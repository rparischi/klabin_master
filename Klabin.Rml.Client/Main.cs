using Klabin.Rml.ClientLogic;
using Klabin.Rml.ClientLogic.HistoryMeasure;
using Klabin.Rml.ClientLogic.MachineObservers;
using Klabin.Rml.ClientLogic.MachineReaders;
using Klabin.Rml.ClientLogic.Sync;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Klabin.Rml.Client.Resources;
using System.Net.Http.Headers;

namespace Klabin.Rml.Client
{
    public enum ExecutionState
    {
        STOPED,
        RUNNING
    }

    public partial class Main : Form
    {
        private readonly ILogger _logger;
        private readonly LogLevel _currentLogLevel;
        private readonly string _currentDirectory;
        private Thread threadLeitura;
        private Thread threadSync;
        private ReaderConfig readerConfig;
        private object syncObject = new object();
        private MachineData lastMachineDataRead;
        private ExecutionState state;
        private CancellationTokenSource cancellationTokenSource;
        private DateTime lastMeasureReadTime;
        private DateTime lastSyncDate;
        private DebugParameterForm debugForm;
        private bool locked;
        private string admCode;
        private const string workDir = "Dados";
        private readonly SyncLocalService _syncLocalService;
        private readonly HistorySearchService _historyService;
        private bool savedValue;

    public Main(LogLevel appLogLevel)
        {
            InitializeComponent();

            _currentDirectory = Directory.GetCurrentDirectory();
            _currentLogLevel = appLogLevel;
            state = ExecutionState.STOPED;
            cancellationTokenSource = new CancellationTokenSource();
            
            LoadConfig(new ConfigForm(Path.Combine(_currentDirectory, "Config")));
            locked = true;
            savedValue = false;

            _logger = Program.ServiceProvider.GetService<ILogger<Main>>();

            _syncLocalService = new(workDir, _logger);
            _historyService = new HistorySearchService(workDir, readerConfig.BaseApiUrl, readerConfig.BaseApiSafariUrl, readerConfig.NumberOfDays, _currentLogLevel, _logger);
            FormLocked();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Visible = false;

            var splashScreen = new SplashScreenForm(InitMainScreen); 
           splashScreen.ShowDialog();
           this.Text += " - " +readerConfig.MachineNumber;
    }


        /// <summary>
        /// called when the splash screen have been closeds
        /// </summary>
        public void InitMainScreen()
        {
            Visible = true;
            if (state == ExecutionState.STOPED)
            {
                InitRead();
            }
        }

        #region Button Events
        private void ButtonShowDebugParametersMode_Click(object sender, EventArgs e)
        {
            debugForm = new DebugParameterForm();
            debugForm.FormClosed += DebugForm_FormClosed;
            debugForm.Show();
        }

        private void IniciarLeituraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == ExecutionState.STOPED)
            {
                InitRead();
            }
            else
            {
                StopRead();
            }
        }

        private void HistoricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var historyService = new HistorySearchService(workDir, readerConfig.BaseApiUrl, readerConfig.BaseApiSafariUrl, readerConfig.NumberOfDays, _currentLogLevel, _logger);
            var historyForm = new MeasureHistoryForm(readerConfig, historyService);

            historyForm.Show();
        }

        private void LancamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var machineData = new MachineData(readerConfig.MachineNumber, readerConfig.MachineType, readerConfig.CapturedDataConfigList)
            {
                ReadTime = DateTime.Now
            };

            foreach (var item in machineData.CapturedDataList)
            {
                item.TrySetValue("100");
            }

            lastMachineDataRead = machineData;
            ShowMessureForm();
        }

        /// <summary>
        /// Triggered when the Config form closes.
        /// This is very important because we need to reload the config and restart the machine reading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadConfig(sender as ConfigForm);
            this.Text = "Klabin - SRP - Automação - " + readerConfig.MachineNumber;
            InitMainScreen();
        }

        private void DebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            debugForm = null;
        }

        private void FecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopRead();
            this.Close();
        }

        private void DesbloquearSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (locked)
            {
                UnlockForm unlockForm = new(admCode, FormUnlocked);
                unlockForm.ShowDialog(this);
            }
            else
            {
                FormLocked();
            }
        }

        public void FormLocked()
        {
            locked = true;
            fecharToolStripMenuItem.Enabled = false;
            iniciarLeituraToolStripMenuItem.Enabled = false;
            configuraçõesToolStripMenuItem.Enabled = false;
            desbloquearSistemaToolStripMenuItem.Text = "Desbloquar Sistema";

            _logger.LogInformation("Sistema bloqueado para alterações");
        }

        public void FormUnlocked()
        {
            locked = false;
            fecharToolStripMenuItem.Enabled = true;
            iniciarLeituraToolStripMenuItem.Enabled = true;
            configuraçõesToolStripMenuItem.Enabled = true;
            desbloquearSistemaToolStripMenuItem.Text = "Bloquear Sistema";

            _logger.LogInformation("Sistema desbloqueado para alterações");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (locked)
            {
                MessageBox.Show("Não é possível fechar o sistema sem desbloqua-lo. Primeiro desbloqueie o modo adm.", "Sistema Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            this.Text += "Esperando finalizar.....";

            StopRead();
        }
        #endregion

        #region Set Init/Stop Read Process 
        private void InitRead()
        {
            if (!IsMachineConfigureted())
            {
                //show error
                MessageBox.Show("Erro ao iniciar a leitura, não há nenhuma configuração válida para a máquina. Configure a máquina primeiro!", "Erro ao inicializar a leitura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ResourceHelper.SetCurrentResource(readerConfig);
            ResourceHelper.SetControlsText(this);

            Task.Run(() => LoadLastMachineData());
            //buttons and interface state
            StartRead_UpdateInterface();

            //Start a new Thread
            _logger.LogInformation("Iniciando sistema de leitura de parametros de maquina...");
            cancellationTokenSource = new CancellationTokenSource();

            threadLeitura = new Thread(ThreadLeitura_DoWork);
            threadLeitura.Start();

            threadSync = new Thread(ThreadSync_DoWork);
            threadSync.Start();
        }

        private void StartRead_UpdateInterface()
        {
            //menu
            iniciarLeituraToolStripMenuItem.Text = "Parar leitura";
            iniciarLeituraToolStripMenuItem.BackColor = Color.Tomato;

            //control labels
            labelStatus.Text = "EXECUTANDO";
            labelStatus.BackColor = Color.ForestGreen;

            //timer
            if (readerConfig == null || !readerConfig.DebugMode)
            {
                return;
            }

            timerUpdateDebugMessage.Enabled = true;
            timerUpdateDebugMessage.Start();
            textBoxDebugLog.Text = string.Empty;

        }

        private void StopRead_UpdateInterface()
        {
            //menu
            iniciarLeituraToolStripMenuItem.Text = "Iniciar leitura";
            iniciarLeituraToolStripMenuItem.BackColor = Color.LightGreen;

            //control labels
            labelStatus.Text = "PARADO";
            labelStatus.BackColor = Color.Tomato;

            //timer
            if (readerConfig == null || !readerConfig.DebugMode)
            {
                return;
            }

            timerUpdateDebugMessage.Stop();
            timerUpdateDebugMessage.Enabled = false;
        }

        private void StopRead()
        {
            _logger.LogInformation("Parando sistema de leitura de parametros de maquina...");

            if (state == ExecutionState.STOPED)
            {
                //buttons and interface state
                StopRead_UpdateInterface();
            }

            cancellationTokenSource.Cancel();
            int countStopTries = 0;

            //wait for busy threads
            do
            {
                if (threadLeitura.Join(500) && threadSync.Join(200))
                {
                    break;
                }

                countStopTries++;

            } while (countStopTries <= 30);

            //wait form stop confirm
            countStopTries = 0;
            do
            {
                //wait main thread for stop confirm
                Thread.Sleep(1000);

                countStopTries++;

            } while (state != ExecutionState.STOPED && countStopTries <= 10);

            //verify if have stoped
            if (state == ExecutionState.STOPED)
            {
                //buttons and interface state
                StopRead_UpdateInterface();
                return;
            }

            //if not stoped notify the user
            _logger.LogInformation("ERRO ao obter confirmação de parada das Threads de Leitura de Maquina e Sincronismo de dados! Possivelmente seja necessário reiniciar o sistema ");
            MessageBox.Show("Não foi possível confirmar a parada do mecanismo de leitura, verifique os logs ou encerre o processo. Possivelmente seja necessário reiniciar o sistema", "Erro ao parar o mecanismo de leitura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Thread.Sleep(2000);
        }
        #endregion

        public async void ThreadLeitura_DoWork()
        {
            //Set main state
            state = ExecutionState.RUNNING;
            ReaderBase machineReader = default;
            MachineObserver observer = default;

            //Get the cancellation Token to be notified if cancel is needed
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            try
            {
                //Create a Machine reader using the Factory
                machineReader = MachineReaderFactory.CreateMachineReader(readerConfig, _currentLogLevel, _logger, cancellationToken);

                if (!machineReader.HasSuccessfullyInitialized())
                {
                    _logger.LogError("O Reader não foi inicializado");
                    return;
                }

                //Setups an observer to the reader
                observer = MachineObserverFactory.Create(readerConfig, machineReader, _logger, _historyService);

                //willl loop until is requested to stop by the cancellationSource
                while (!cancellationToken.IsCancellationRequested)
                {
                    //Read a single interation with machine
                    var returnMachineData = await machineReader.GetMachineData();                    
                    
                    //get last data from the observer cache
                    var machineData = observer.GetLastData(); //TODO Verificar se o valor é o mesmo.
                    
                    if (returnMachineData != null && returnMachineData.RmlRawData == "0")
                    {
                        savedValue = false;
                        observer.ResetObserver();
                    }
                    else
                    {
                        //lock to write 
                        lock (syncObject)
                        {
                            lastMachineDataRead = machineData;
                        }
                    }
                    //anoter check to cancel wich
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    //Has completed the machine read parameters necessary to the measure?
                    if (observer.HasCompletedMachineRead())
                    {
                        if (!savedValue)
                        {
                            SaveMessureReader(observer, readerConfig);
                            if (readerConfig.MachineType == MachineType.ReelWeigth)
                                savedValue = true;
                        }

                        Thread.Sleep(3000);
                    }

                    Thread.Sleep(readerConfig.LookpUpWaitTime);
                }
            }
            finally
            {
                if (observer != default)
                {
                    observer.Unsubscribe();
                }
                if (machineReader != default)
                {
                    machineReader.Dispose();
                }

                state = ExecutionState.STOPED;
            }
        }

        private void SaveMessureReader(MachineObserver observer, ReaderConfig config)
        {
            observer.ResetObserver();
            lastMeasureReadTime = DateTime.Now;
            UpdateLastValidRead();

            if (config.MachineType == MachineType.ReelLength)
            {
                SaveMessureReaderFromMeassureForm();
                return;
            }

            _syncLocalService.SaveMachineMeasure(lastMachineDataRead);
        }

        private void SaveMessureReaderFromMeassureForm()
        {
            //check the "best way" to lauch the MessureForm and wait for user input
            //the form will take the "lastMachineDataRead" variable
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => ShowMessureForm() ));
                return;
            }

            ShowMessureForm();
        }

        public async void ThreadSync_DoWork()
        {
            var syncService = new SyncService(workDir, readerConfig.BaseApiUrl, _currentLogLevel, _logger);
            var cancellationToken = cancellationTokenSource.Token;

            //wait to start the process
            Thread.Sleep(3000);

            try
            {
                //while cancel have not been requested
                while (!cancellationToken.IsCancellationRequested && IsMachineConfigureted())
                {
                    // sync data to the API
                    await syncService.SyncAllMeasureData();

                    lastSyncDate = syncService.LastSyncDate;
                    BeginInvoke((Action)(() => UpdateLastSyncDate()));

                    // wait 
                    Thread.Sleep(2000);
                }
            }
            finally
            {
                syncService.Dispose();
            }
        }

        private void LoadConfig(ConfigForm configForm)
        {
            try
            {
                if (configForm.LoadConfig(false))
                {
                    readerConfig = configForm.GetReaderConfig();

                    if (readerConfig != null)
                    {
                        groupBoxDebugMode.Visible = readerConfig.DebugMode;
                    }
                }

                admCode = Program.Configuration.GetSection("admcode").Value;

                //try to create work dir
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), workDir)))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), workDir));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível carregar a configuração do sistema. Erro: {ex.Message}", "Erro ao carregar o sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsMachineConfigureted()
        {
            return readerConfig != null && !string.IsNullOrWhiteSpace(readerConfig.MachineNumber);
        }

        public void ShowMessureForm()
        {
            var historyService = new HistorySearchService(workDir, readerConfig.BaseApiUrl, readerConfig.BaseApiSafariUrl, readerConfig.NumberOfDays, _currentLogLevel, _logger);
            MessureForm lancamentoForm = new(lastMachineDataRead, historyService, _logger, workDir);
            lancamentoForm.FormClosing += LancamentoForm_FormClosing;
            lancamentoForm.Show();
        }

        private async void LancamentoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var messureForm = sender as MessureForm;
            await LoadLastMachineData(messureForm.GetCompletedMachineData());
        }

        private void UpdateLastSyncDate()
        {
            if (lastSyncDate != default)
            {
                labelLastSyncDate.Text = lastSyncDate.ToString("dd/MM/yy HH:mm");
            }
        }

        private void UpdateLastValidRead()
        {
            if (lastMeasureReadTime != default)
            {
                if (InvokeRequired)
                {
                    BeginInvoke((Action)(() => labelLastRead.Text = lastMeasureReadTime.ToString("dd/MM/yy HH:mm")));
                }
                else
                {
                    labelLastRead.Text = lastMeasureReadTime.ToString("dd/MM/yy HH:mm");
                }
            }
        }

        private void TimerUpdateDebugMessage_Tick(object sender, EventArgs e)
        {
            if (lastMachineDataRead != null)
            {
                lock (syncObject)
                {
                    textBoxDebugLog.Text = lastMachineDataRead.OperationLog;
                    if (debugForm != null)
                    {
                        debugForm.Render(lastMachineDataRead);
                    }
                }
            }

            if (threadLeitura != null)
            {
                labelStatusThread.Text = threadLeitura.ThreadState.ToString();
            }

            if (threadSync != null)
            {
                labelSyncState.Text = threadSync.ThreadState.ToString();
            }
        }

        private async Task LoadLastMachineData(MachineData machineData = null)
        {
            if (!IsMachineConfigureted() || readerConfig?.MachineType == MachineType.ReelWeigth || readerConfig?.MachineType == MachineType.ReelWeightRinnert || readerConfig?.MachineType == MachineType.ReelWeightToledo || readerConfig?.MachineType == MachineType.ReelWeigthP08)
            {
                return;
            }

            string rollNumber;
            string cutNumber;

            //check if model exists, if it not exists, get from History
            if (machineData == null)
            {
                //search history from API
                var (model, success) = await _historyService.SearchLastMachineDataAsync(new()
                {
                    MachineNumber = readerConfig.MachineNumber,
                });

                if (!success || model == null)
                {
                    return;
                }

                //set data from history
                rollNumber = string.IsNullOrWhiteSpace(model?.RollNumber) ? "--" : model?.RollNumber;
                cutNumber = string.IsNullOrWhiteSpace(model?.CutNumber) ? "--" : model?.CutNumber;
            }
            else
            {
                //set data from history
                rollNumber = string.IsNullOrWhiteSpace(machineData?.RollNumber) ? "--" : machineData?.RollNumber;
                cutNumber = string.IsNullOrWhiteSpace(machineData?.CutNumber) ? "--" : machineData?.CutNumber;
            }


            BeginInvoke((Action)(() =>
            {
                labelLastRollNumber.Text = rollNumber;
                labelLastCutNumber.Text = cutNumber;
            }));
        }

        private void ConfiguracoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new(Path.Combine(_currentDirectory, "Config"));
            configForm.Show();
            configForm.FormClosed += ConfigForm_FormClosed;

            StopRead();
        }
    }
}

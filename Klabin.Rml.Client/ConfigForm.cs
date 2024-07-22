using Klabin.Rml.ClientLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klabin.Rml.Client
{
    public partial class ConfigForm : Form
    {
        public const string CONFIG_FILE_NAME = "machineConfig.json";
        private readonly string _baseDirectory;
        private ReaderConfig readerConfig;
        private Dictionary<Control, Label> dictionaryControlsWithError = new Dictionary<Control, Label>();

        public ConfigForm(string dirConfig)
        {
            InitializeComponent();

            _baseDirectory = dirConfig;

            //try to load known config
            LoadConfig(true);
        }

        #region Buttons event
        private void ComboBoxDriverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDriverType.SelectedItem != null &&
               comboBoxDriverType.SelectedItem.ToString() == ReaderType.SERIAL.ToString())
            {
                groupBoxSerialConfig.Visible = true;
                groupBoxTcpConfig.Visible = false;
            }
            else if (comboBoxDriverType.SelectedItem != null &&
                     comboBoxDriverType.SelectedItem.ToString() == ReaderType.TCP.ToString())
            {
                groupBoxSerialConfig.Visible = false;
                groupBoxTcpConfig.Visible = true;
            }

            Refresh();
        }

        private void ComboBoxMachineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMachineType.SelectedItem != null && comboBoxMachineType.SelectedItem.ToString() == MachineType.ReelWeigth.ToString())
            {
                comboBoxDriverType.SelectedItem = ReaderType.TCP.ToString();
            }

            Refresh();
        }

        private void ButtonFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void buttonSalvar_ClickAsync(object sender, EventArgs e)
        {
            if (!SaveConfigGeneral())
                return;

            if (readerConfig.ReaderType == ReaderType.SERIAL)
            {
                if (!SaveSerialConfigParameter())
                    return;
            }
            if (readerConfig.ReaderType == ReaderType.TCP)
            {
                if (!SaveTCPConfigParameter())
                    return;
            }
            if (!SaveReaderDataParameters())
                return;

            //save to file
            await SerializeAndSaveConfigAsync();

            MessageBox.Show("Configuração salva com sucesso!", "Configuração salva", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //closes the form
            this.Close();
        }
        #endregion

        #region Load Config
        public bool LoadConfig(bool loadScreen)
        {
            try
            {
                //check for config file
                if (!Directory.Exists(_baseDirectory) ||
                     Directory.GetFiles(_baseDirectory).Length == 0)
                {
                    //config does not exists yet
                    return true;
                }

                //Get the first file
                var configFile = Directory.GetFiles(_baseDirectory, "*.json").FirstOrDefault();
                if (configFile == null)
                {
                    return false;
                }

                //try read config file
                var configValueInJson = File.ReadAllText(configFile);
                if (string.IsNullOrWhiteSpace(configValueInJson))
                {
                    throw new Exception($"Erro ao ler o conteúdo do arquivo de configuração: {Path.Combine(_baseDirectory, CONFIG_FILE_NAME)} . O arquivo esta vazio");
                }

                var fileInfo = new FileInfo(configFile);
                var readerTypeName = fileInfo.Name.Split('_')[0];

                //Check if the config file has the correct format in its name:
                //ReaderType_CONFIG_FILE_NAME
                //ex: Serial_MachineConfig.json
                if (string.IsNullOrWhiteSpace(readerTypeName))
                {
                    return true;
                }

                //deserialize to the correct implementation type
                switch (Enum.Parse<ReaderType>(readerTypeName))
                {
                    case ReaderType.SERIAL:
                        readerConfig = JsonSerializer.Deserialize<SerialReaderConfig>(configValueInJson, options: GetConfigSerializationOptions());
                        break;
                    case ReaderType.TCP:
                        readerConfig = JsonSerializer.Deserialize<TcpReaderConfig>(configValueInJson, options: GetConfigSerializationOptions());
                        break;
                }

                //if not intented to load the screen we should exit 
                if (!loadScreen)
                {
                    return true;
                }
        
                //Load general config
                LoadConfigGeneral();

                //load specific driver config
                if (readerConfig.ReaderType == ReaderType.SERIAL)
                    LoadSerialConfig();
                if (readerConfig.ReaderType == ReaderType.TCP)
                    LoadTcpConfig();

                //Load data parameters
                LoadDataReadParameters();
                LoadMachineType(readerConfig);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao carregar a configuração. Erro: {ex.Message}", "Erro ao CARREGAR o formulário", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void LoadMachineType(ReaderConfig readerConfig)
        {
            comboBoxMachineType.SelectedItem = readerConfig.MachineType.ToString();
        }

        public void LoadConfigGeneral()
        {
            textBoxMachine.Text = readerConfig.MachineNumber;
            textBoxLookupTime.Text = readerConfig.LookpUpWaitTime.ToString();
            comboBoxDriverType.SelectedItem = readerConfig.ReaderType.ToString();
            comboBoxDebugMode.SelectedItem = readerConfig.DebugMode.ToString().ToUpper();
            textBoxUrlApi.Text = readerConfig.BaseApiUrl;
            textBoxUrlSafariApi.Text = readerConfig.BaseApiSafariUrl;
            textBoxQtdDiasSafari.Text = readerConfig.NumberOfDays.ToString();
        }

        public void LoadSerialConfig()
        {
            var serialReaderConfig = readerConfig as SerialReaderConfig;

            textBoxSerialPortComName.Text = serialReaderConfig.PortName;
            //comboBoxDataType.Text = serialReaderConfig.DataType;
            textBoxSerialBoundRate.Text = serialReaderConfig.BaudRate.ToString();
            comboBoxSerialParity.SelectedItem = serialReaderConfig.Parity.ToString();
            comboBoxSerialStopBits.SelectedItem = serialReaderConfig.StopBits.ToString();
            textBoxSerialDataBits.Text = serialReaderConfig.DataBits.ToString();
            textBoxSerialReadTimeout.Text = serialReaderConfig.ReadTimeout.ToString();
            textBoxSerialWriteTimeout.Text = serialReaderConfig.WriteTimeout.ToString();
        }

        public void LoadTcpConfig()
        {
            var tcpReaderConfig = readerConfig as TcpReaderConfig;

            textBoxTCPAddress.Text = tcpReaderConfig.Address;
            textBoxTCPPort.Text = tcpReaderConfig.Port.ToString();
            textBoxTCPReadTimeout.Text = tcpReaderConfig.ReadTimeout.ToString();
            textBoxTCPWriteTimeout.Text = tcpReaderConfig.WriteTimeout.ToString();
        }

        public void LoadDataReadParameters()
        {
            foreach (var item in readerConfig.CapturedDataConfigList)
            {
                dataGridViewReadParameters.Rows.Add(item.Position.ToString(), item.Name, item.DescriptionName, item.DataType.ToString());
            }
        }

        #endregion

        #region Save Config
        public async Task SerializeAndSaveConfigAsync()
        {
            try
            {
                var readerType = readerConfig.ReaderType;
                object readerConfigToSerialize = null;

                //box the implementation type into object
                //this will allow us to correct serialize the the child type
                switch (readerType)
                {
                    case ReaderType.SERIAL:
                        readerConfigToSerialize = (readerConfig as SerialReaderConfig); 
                        break;
                    case ReaderType.TCP:
                        readerConfigToSerialize = (readerConfig as TcpReaderConfig);
                        break;
                }

                //serialize config to Json
                var configValueInJson = JsonSerializer.Serialize(readerConfigToSerialize, options: GetConfigSerializationOptions());

                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }

                //Clear dir json files
                var oldConfigFiles = Directory.GetFiles(_baseDirectory, "*.json");
                foreach (var file in oldConfigFiles)
                {
                    File.Delete(file);
                }

                //Write to file
                await File.WriteAllTextAsync(Path.Combine(_baseDirectory, $"{readerType}_{CONFIG_FILE_NAME}"), configValueInJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao salvar a configuração. Erro: {ex.Message}", "Erro ao SALVAR o formulário", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool SaveConfigGeneral()
        {
            try
            {
                //Validation
                if (string.IsNullOrWhiteSpace(textBoxMachine.Text) ||
                    textBoxMachine.Text.Length > 10)
                {
                    ShowMessageValidationError("O campo número da máquina não pode ser vazio, e teve ter no máximo 10 caracteres", controlWithError: textBoxMachine, controlWithErrorLabel: labelMachine);
                    return false;
                }
                if (comboBoxDriverType.SelectedItem == null)
                {
                    ShowMessageValidationError("É obrigatório selecionar o tipo de Driver desta máquina para salvar a configuração", controlWithError: comboBoxDriverType, controlWithErrorLabel: labelDriverType);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(textBoxLookupTime.Text) ||
                    !int.TryParse(textBoxLookupTime.Text, out int lookupTime) ||
                    lookupTime <= 0)
                {
                    ShowMessageValidationError("O campo 'Tempo entre leituras' deve conter apenas números e deve ser maior que zero", controlWithError: textBoxLookupTime, controlWithErrorLabel: labelLookupTime);
                    return false;
                }             
                if (string.IsNullOrWhiteSpace(textBoxQtdDiasSafari.Text) ||
                    !int.TryParse(textBoxQtdDiasSafari.Text, out int numberOfDays) ||
                    numberOfDays <= 0)
                {
                    ShowMessageValidationError("O campo 'Quantidade de dias' deve conter apenas números e deve ser maior que zero", controlWithError: textBoxQtdDiasSafari, controlWithErrorLabel: labelQtdDiasSafari);
                    return false;
                }
                if (comboBoxDebugMode.SelectedItem == null)
                {
                    comboBoxDebugMode.SelectedItem = "FALSE";
                }
                if (string.IsNullOrWhiteSpace(textBoxUrlApi.Text))
                {
                    ShowMessageValidationError("O Url API deve possuir um endereço (url) válido", controlWithError: textBoxUrlApi, controlWithErrorLabel: labelUrlApi);
                    return false;
                }                
                if (string.IsNullOrWhiteSpace(textBoxUrlSafariApi.Text))
                {
                    ShowMessageValidationError("O Url API Safari deve possuir um endereço (url) válido", controlWithError: textBoxUrlSafariApi, controlWithErrorLabel: labelUrlSafariApi);
                    return false;
                }


                //Create Reader
                if (comboBoxDriverType.SelectedItem.ToString() == "SERIAL")
                {
                    readerConfig = new SerialReaderConfig();
                }
                else if (comboBoxDriverType.SelectedItem.ToString() == "TCP")
                {
                    readerConfig = new TcpReaderConfig();
                }

                //Field load
                readerConfig.MachineNumber = textBoxMachine.Text;
                readerConfig.ReaderType = Enum.Parse<ReaderType>(comboBoxDriverType.SelectedItem.ToString());
                readerConfig.LookpUpWaitTime = lookupTime;
                readerConfig.DebugMode = bool.Parse(comboBoxDebugMode.SelectedItem.ToString().ToLower());
                readerConfig.BaseApiUrl = textBoxUrlApi.Text;
                readerConfig.BaseApiSafariUrl = textBoxUrlSafariApi.Text;
                readerConfig.NumberOfDays = numberOfDays;
                readerConfig.MachineType = Enum.Parse<MachineType>(comboBoxMachineType.SelectedItem.ToString());

                return true;
            }
            catch (Exception ex)
            {
                ShowMessageValidationError($"Ocorreu um erro ao carregar as configurações gerais. Erro: {ex.Message}");
                return false;
            }
        }

        public bool SaveSerialConfigParameter()
        {
            try
            {
                if (readerConfig == null)
                    return false;

                //Validation
                if (string.IsNullOrWhiteSpace(textBoxSerialPortComName.Text))
                {
                    ShowMessageValidationError($"O campo '{labelSerialComPort.Text}' deve ser preenchido com uma COM válida", controlWithError: textBoxSerialPortComName, controlWithErrorLabel: labelSerialComPort);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(textBoxSerialBoundRate.Text) ||
                    !int.TryParse(textBoxSerialBoundRate.Text, out int boundRate) ||
                    boundRate <= 0)
                {
                    ShowMessageValidationError($"O campo '{labelSerialBoundRate.Text}' deve possuir um valor inteiro maior que zero", controlWithError: textBoxSerialBoundRate, controlWithErrorLabel: labelSerialBoundRate);
                    return false;
                }
                if (comboBoxSerialParity.SelectedItem == null)
                {
                    ShowMessageValidationError($"É obrigatório selecionar uma opção para o campo: '{labelSerialParity.Text}'", controlWithError: comboBoxSerialParity, controlWithErrorLabel: labelSerialParity);
                    return false;
                }
                if (comboBoxSerialStopBits.SelectedItem == null)
                {
                    ShowMessageValidationError($"É obrigatório selecionar uma opção para o campo: '{labelSerialStopBits.Text}'", controlWithError: comboBoxSerialStopBits, controlWithErrorLabel: labelSerialStopBits);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(textBoxSerialDataBits.Text) ||
                    !int.TryParse(textBoxSerialDataBits.Text, out int dataBits) ||
                    dataBits <= 0)
                {
                    ShowMessageValidationError($"O campo '{labelSerialDataBits.Text}' deve possuir um valor inteiro maior que zero", controlWithError: textBoxSerialDataBits, controlWithErrorLabel: labelSerialDataBits);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(textBoxSerialReadTimeout.Text) ||
                    !int.TryParse(textBoxSerialReadTimeout.Text, out int serialReadTimeout) ||
                    serialReadTimeout <= 0)
                {
                    ShowMessageValidationError($"O campo '{labelSerialReadtimeout.Text}' deve possuir um valor inteiro maior que zero", controlWithError: textBoxSerialReadTimeout, controlWithErrorLabel: labelSerialReadtimeout);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(textBoxSerialWriteTimeout.Text) ||
                    !int.TryParse(textBoxSerialWriteTimeout.Text, out int serialWriteTimeout) ||
                    serialWriteTimeout <= 0)
                {
                    ShowMessageValidationError($"O campo '{labelSerialWriteTimeout.Text}' deve possuir um valor inteiro maior que zero", controlWithError: textBoxSerialWriteTimeout, controlWithErrorLabel: labelSerialWriteTimeout);
                    return false;
                }

                //Field load
                var serialRaderConfig = readerConfig as SerialReaderConfig;
                serialRaderConfig.PortName = textBoxSerialPortComName.Text;
                //serialRaderConfig.DataType = Enum<Parse>
                serialRaderConfig.BaudRate = boundRate;
                serialRaderConfig.Parity = Enum.Parse<System.IO.Ports.Parity>(comboBoxSerialParity.SelectedItem.ToString());
                serialRaderConfig.StopBits = Enum.Parse<System.IO.Ports.StopBits>(comboBoxSerialStopBits.SelectedItem.ToString());
                serialRaderConfig.DataBits = dataBits;
                serialRaderConfig.ReadTimeout = serialReadTimeout;
                serialRaderConfig.WriteTimeout = serialWriteTimeout;

                return true;
            }
            catch (Exception ex)
            {
                ShowMessageValidationError($"Ocorreu um erro ao carregar as configurações para driver Serial. Erro: {ex.Message}");
                return false;
            }
        }



        public bool SaveTCPConfigParameter()
        {
            try
            {
                if (readerConfig == null)
                    return false;

                //Validation
                if (string.IsNullOrWhiteSpace(textBoxTCPAddress.Text) ||
                    !ValidateIPv4(textBoxTCPAddress.Text))
                {
                    ShowMessageValidationError($"O campo '{labelTCPAdress.Text}' deve ser preenchido com um endereço IP válido", controlWithError: textBoxTCPAddress, controlWithErrorLabel: labelTCPAdress);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(textBoxTCPPort.Text) ||
                    !int.TryParse(textBoxTCPPort.Text, out int port) ||
                    port <= 0)
                {
                    ShowMessageValidationError($"O campo '{labelTCPPort.Text}' deve possuir um valor inteiro maior que zero", controlWithError: textBoxTCPPort, controlWithErrorLabel: labelTCPPort);
                    return false;
                }

                //Field load
                var tcpReaderConfig = readerConfig as TcpReaderConfig;

                tcpReaderConfig.Address = textBoxTCPAddress.Text;
                tcpReaderConfig.Port = port;
                tcpReaderConfig.ReadTimeout = int.Parse(textBoxTCPReadTimeout.Text);
                tcpReaderConfig.WriteTimeout = int.Parse(textBoxTCPWriteTimeout.Text);

                return true;
            }
            catch (Exception ex)
            {
                ShowMessageValidationError($"Ocorreu um erro ao carregar as configurações para driver TCP. Erro: {ex.Message}");
                return false;
            }
        }

        public bool SaveReaderDataParameters()
        {
            try
            {
                if (readerConfig == null)
                    return false;

                //Validation
                if (dataGridViewReadParameters.Rows.Count == 0)
                {
                    ShowMessageValidationError($"É preciso configurar pelo menos um parâmetro de leitura!", controlWithError: dataGridViewReadParameters);
                    return false;
                }

                //Field load
                readerConfig.CapturedDataConfigList = new();
                foreach (DataGridViewRow row in dataGridViewReadParameters.Rows)
                {
                    //dot not save rows not commited
                    if (row.IsNewRow)
                        continue;

                    int.TryParse(row.Cells[0].Value?.ToString(), out int position);
                    var name = row.Cells[1].Value?.ToString();
                    var description = row.Cells[2].Value?.ToString();
                    var machineCapturedDataType = Enum.Parse<MachineCapturedDataType>(row.Cells[3].Value?.ToString());

                    readerConfig.CapturedDataConfigList.Add(new()
                    {
                        Position = position,
                        Name = name,
                        DescriptionName = description,
                        DataType = machineCapturedDataType
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowMessageValidationError($"Ocorreu um erro ao carregar as configurações dos parâmetros de leitura. Erro: {ex.Message}");
                return false;
            }
        }

        private void ShowMessageValidationError(string message, Control controlWithError = null, Label controlWithErrorLabel = null)
        {
            MessageBox.Show(message, "Erro de validação do formulário", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (controlWithError != null)
            {
                controlWithError.ForeColor = Color.Tomato;
                controlWithError.LostFocus += ControlWithError_LostFocus;

                if (controlWithErrorLabel != null)
                {
                    controlWithErrorLabel.ForeColor = Color.Tomato;
                    dictionaryControlsWithError.Add(controlWithError, controlWithErrorLabel);
                }
            }
          
        }

        private void ControlWithError_LostFocus(object sender, EventArgs e)
        {
            if (sender != null &&
               sender is Control)
            {
                var senderControl = (sender as Control);
                senderControl.ForeColor = Color.Black;
                senderControl.LostFocus -= ControlWithError_LostFocus;

                //lookup on the dictionary
                if (dictionaryControlsWithError.ContainsKey(senderControl))
                {
                    dictionaryControlsWithError[senderControl].ForeColor = Color.Black;
                }
            }
        }

        #endregion

        #region Grid events

        private void dataGridViewReadParameters_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var row = dataGridViewReadParameters.Rows[e.RowIndex];

            int.TryParse(row.Cells[0].Value?.ToString(), out int position);
            var name = row.Cells[1].Value?.ToString();
            var description = row.Cells[2].Value?.ToString();
            var valueType = row.Cells[3].Value?.ToString();

            if (position < 0 || string.IsNullOrWhiteSpace(row.Cells[0].Value?.ToString()))
            {
                ShowMessageValidationError($"A Posição do parâmetro de leitura deve ser '0' ou superior");
                row.Cells[0].ErrorText = "A Posição do parâmetro de leitura deve ser '0' ou superior";
                row.ErrorText = "Corrija os erros de validação para salvar o parâmetro de leitura";
                e.Cancel = true;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                ShowMessageValidationError($"O Nome do parâmetro de leitura não pode ser vazio");
                row.Cells[1].ErrorText = "O Nome do parâmetro de leitura não pode ser vazio";
                row.ErrorText = "Corrija os erros de validação para salvar o parâmetro de leitura";
                e.Cancel = true;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                ShowMessageValidationError($"A Descrição do parâmetro de leitura não pode ser vazia");
                row.Cells[2].ErrorText = "A Descrição do parâmetro de leitura não pode ser vazia";
                row.ErrorText = "Corrija os erros de validação para salvar o parâmetro de leitura";
                e.Cancel = true;
            }

            if (string.IsNullOrWhiteSpace(valueType) ||
                !Enum.TryParse(valueType, out MachineCapturedDataType machineCapturedDataType))
            {
                ShowMessageValidationError($"O Tipo do parâmetro de leitura não pode ser vazio, selecione um valor");
                row.Cells[3].ErrorText = "O Tipo do parâmetro de leitura não pode ser vazio, selecione um valor";
                row.ErrorText = "Corrija os erros de validação para salvar o parâmetro de leitura";
                e.Cancel = true;
            }

            //if has validation error, exits here
            if (e.Cancel)
                return;

            //check repeated values
            if (GridHasRepeteadValues(e.RowIndex, 0, position.ToString()))
            {
                ShowMessageValidationError($"Já existe um parâmetro de leitura com esse valor de Posição");
                row.Cells[0].ErrorText = "Já existe um parâmetro de leitura com esse valor de Posição";
                row.ErrorText = "Corrija os erros de validação para salvar o parâmetro de leitura";
                e.Cancel = true;
            }
            if (GridHasRepeteadValues(e.RowIndex, 1, name))
            {
                ShowMessageValidationError($"Já existe um parâmetro de leitura com esse Nome");
                row.Cells[1].ErrorText = "Já existe um parâmetro de leitura com esse Nome";
                row.ErrorText = "Corrija os erros de validação para salvar o parâmetro de leitura";
                e.Cancel = true;
            }

            //if not to cancel, then clear error texts
            if (!e.Cancel)
            {
                row.ErrorText = string.Empty;
                row.Cells[0].ErrorText = row.Cells[1].ErrorText = row.Cells[2].ErrorText = string.Empty;
            }
        }

        private bool GridHasRepeteadValues(int currentRowIndex, int cellIndex, string value)
        {
            foreach (DataGridViewRow row in dataGridViewReadParameters.Rows)
            {
                //do not validate current row
                if (row.Index == currentRowIndex)
                    continue;

                //check equals value
                if (row.Cells[cellIndex].Value?.ToString() == value)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        public static JsonSerializerOptions GetConfigSerializationOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public ReaderConfig GetReaderConfig()
        {
            return readerConfig;
        }

        public bool ValidateIPv4(string ipString)
        {
            if (string.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
  }
}

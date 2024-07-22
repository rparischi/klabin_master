using Klabin.Rml.ClientLogic;
using Klabin.Rml.ClientLogic.HistoryMeasure;
using Klabin.Rml.ClientLogic.Sync;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Klabin.Rml.Client
{
    public partial class MessureForm : Form
    {
        private readonly MachineData _machineData;
        private readonly HistorySearchService _searchService;
        private readonly SyncLocalService _syncLocalService;

        public MessureForm(MachineData machineData, HistorySearchService historySearchService, ILogger logger, string baseDirectory)
        {
            InitializeComponent();

            _machineData = machineData;

            _searchService = historySearchService;

            _syncLocalService = new(baseDirectory, logger);

            buttonEvniarDados.Click += async (s, e) => await ButtonEvniarDados_Click(s, e);
        }

        private void LancamentoMetragemForm_Load(object sender, EventArgs e)
        {
            LoadForm(_machineData);

            LoadLastMachineData(_machineData.MachineNumber).GetAwaiter();
        }

        private void LoadForm(MachineData machineData)
        {
            labelMachine.Text = machineData.MachineNumber;
            dateTimePickerCreationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            labelReadTime.Text = machineData.ReadTime.ToString("dd/MM/yyyy HH:mm");

            GenerateReadParametersFields(machineData);
        }

        private void GenerateReadParametersFields(MachineData machineData)
        {
            var graphics = this.CreateGraphics();
            panelMachineReadParameters.Controls.Clear();
            int initialY = 17;
            int yAux = initialY;
            int yConstantSparse = 5;

            foreach (var parameter in machineData.CapturedDataList.OrderBy(x => x.Position))
            {
                var labelParam = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                    Location = new Point(8, yAux + yConstantSparse),
                    Name = $"label_{parameter.Name}",
                    Size = new Size(173, 25),
                    Text = parameter.DescriptionName,
                };
                panelMachineReadParameters.Controls.Add(labelParam);

                var sz = graphics.MeasureString(labelParam.Text, labelParam.Font);
                yAux = yAux + (int)sz.Height + yConstantSparse;

                var textBoxParam = new TextBox
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                    Location = new Point(8, yAux),
                    Name = $"label_{parameter.Name}",
                    Size = new Size(174, 29),
                    Text = parameter.Value?.ToString(),
                };

                sz = graphics.MeasureString(textBoxParam.Text, labelParam.Font);
                yAux = yAux + (int)sz.Height + yConstantSparse;

                panelMachineReadParameters.Controls.Add(textBoxParam);
            }

            panelMachineReadParameters.Refresh();
        }

        private async Task ButtonEvniarDados_Click(object sender, EventArgs e)
        {
            if (!EnrichMachineData())
            {
                return;
            }

            if (!await MachineMeasureIsValid(_machineData.MachineNumber, _machineData.RollNumber, _machineData.CutNumber, _machineData.RollDate))
            {
                return;
            }

            SaveMachineMeasure();
            Close();
        }

        private bool EnrichMachineData()
        {
            int rollNumber;
            int cutNumber;

            if (!int.TryParse(maskedTextBoxRollNumber.Text, out rollNumber))
            {
                ShowMessaBox("Erro ao converter o valor do Número do Rolo para um valor númerico. Revise o campo", "Nº Rolo com erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(maskedTextBoxCutNumber.Text, out cutNumber))
            {
                ShowMessaBox("Erro ao converter o valor do Número do Tirada para um valor númerico. Revise o campo", "Nº TIRADA com erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rollNumber <= 0)
            {
                ShowMessaBox("O Número do Rolo deve ser maior que zero. Revise o campo", "Nº Rolo com erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cutNumber <= 0)
            {
                ShowMessaBox("O Número do Tirada deve ser maior que zero. Revise o campo", "Nº TIRADA com erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            _machineData.RollDate = LoadRoolDate();
            _machineData.RollNumber = rollNumber.ToString();
            _machineData.CutNumber = cutNumber.ToString();

            return true;
        }

        private DateTime LoadRoolDate()
        {
            return new DateTime(year: dateTimePickerCreationDate.Value.Year,
                                month: dateTimePickerCreationDate.Value.Month,
                                day: dateTimePickerCreationDate.Value.Day,
                                hour: DateTime.Now.Hour,
                                minute: DateTime.Now.Minute,
                                second: DateTime.Now.Second,
                                kind: DateTimeKind.Local);
        }

        private void SaveMachineMeasure()
        {
            var (success, errorMessage) = _syncLocalService.SaveMachineMeasure(_machineData);
            if (success)
            {
                return;
            }

            ShowMessaBox(errorMessage, "Erro ao salvar a leitura", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async Task<bool> MachineMeasureIsValid(string machineNumber, string rollNumber, string cutNumber, DateTime creationDate)
        {
            var initialDate = DateTime.Parse(dateTimePickerCreationDate.Text);

            var history = await GetHistory(machineNumber, rollNumber, cutNumber, creationDate);

            var historyLinearMeasure = await GetLinearMeasureHistory(machineNumber, rollNumber, cutNumber, initialDate);

            if (history == null || historyLinearMeasure == null)
            {
                return false;
            }

            if (history.Any())
            {
                ShowMessaBox($"A tirada {cutNumber}, do rolo {rollNumber} já foi informada .", "Tirada já informada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!historyLinearMeasure.Where(x => x.RollDate == initialDate).Any())
            {
                ShowMessaBox($"Rolo jumbo {rollNumber} não existe no SRP para a data {initialDate:dd/MM/yyyy}.", "Rolo jumbo inexistente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var resultValidation = historyLinearMeasure.Where(x => x.RollDate != initialDate && x.CutNumber == null).ToList();

            if (resultValidation.Any())
            {
                var dates = String.Join(", ", resultValidation.Select(x => x.RollDate.Date.ToString("dd/MM/yyyy")));

                var yesNo = ShowMessaBoxYesNo($"Nos últimos dias foram encontrados mais de um rolo {rollNumber} onde existe a tirada {cutNumber} em aberto.\n" +
                                              $"{dates}, {initialDate:dd/MM/yyyy} \n \n" +
                                              $"Você confirma inserir metragem para o rolo {rollNumber} tirada {cutNumber} para data {initialDate:dd/MM/yyyy}?", 
                                              "Tirada não informada", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                return yesNo == DialogResult.Yes;
            }

            return true;
        }

        private void ShowMessaBox(string erroMessage, string titleMessage, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            TopMost = false;
            MessageBox.Show(new Form { TopMost = true }, erroMessage, titleMessage, messageBoxButtons, messageBoxIcon);
            TopMost = true;
        }

        private DialogResult ShowMessaBoxYesNo(string erroMessage, string titleMessage, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            TopMost = false;
            DialogResult result = MessageBox.Show(new Form { TopMost = true }, erroMessage, titleMessage, messageBoxButtons, messageBoxIcon);
            TopMost = true;
            return result;
        }

        private async Task LoadLastMachineData(string machineNumber)
        {
            var (model, success) = await _searchService.SearchLastMachineDataAsync(new()
            {
                MachineNumber = machineNumber,
            });

            if (!success || model == null)
            {
                return;
            }

            labelLastCutNumber.Text = model.CutNumber;
            labelLastRollNumber.Text = model.RollNumber;
            labelLastRollDate.Text = model.RollDate?.ToString("dd/MM/yyyy");
        }

        private async Task<List<HistoryMachineDataResponseDto>> GetHistory(string machineNumber, string rollNumber, string cutNumber, DateTime creationDate)
        {
            try
            {
                var initialDate = DateTime.Parse(dateTimePickerCreationDate.Text);

                return await _searchService.SearchHistoryAsync(new HistoryMachineDataRequest()
                {
                    MachineNumber = machineNumber,
                    RollNumber = rollNumber,
                    CutNumber = cutNumber,
                    InitialRollDate = initialDate.Date,
                    FinalRollDate = creationDate.Date.AddDays(1).AddMilliseconds(-1.0)
                });
            }
            catch (Exception ex)
            {
                ShowMessaBox($"Erro ao buscar as últimas leituras informadas. Erro: {ex.Message}", "Erro na operação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private async Task<List<HistoryLinearMeasureDataResponseDto>> GetLinearMeasureHistory(string machineNumber, string rollNumber, string cutNumber, DateTime initialDate)
        {
            try
            {
                return await _searchService.GetLinearMeasureHistory(new HistoryLinearMeasureDataRequest()
                {
                    MachineNumber = machineNumber,
                    RollNumber = rollNumber,
                    CutNumber = cutNumber,
                    RollDate = initialDate
                });
            }
            catch (Exception ex)
            {
                ShowMessaBox($"Erro ao buscar as últimas leituras informadas na tabela ROLAO/METRAGEM LINEAR. Erro: {ex.Message}", "Erro na operação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void MessureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maskedTextBoxRollNumber.Text))
            {
                ShowMessaBox($"Informe o valor do número do rolo primeiro", "Campos obriatórios não atendidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(maskedTextBoxCutNumber.Text))
            {
                ShowMessaBox($"Informe o valor do número da Tirada primeiro", "Campos obriatórios não atendidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
        }

        public MachineData GetCompletedMachineData()
        {
            return _machineData;
        }
    }
}

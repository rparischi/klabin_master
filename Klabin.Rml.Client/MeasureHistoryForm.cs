using Klabin.Rml.ClientLogic;
using Klabin.Rml.ClientLogic.HistoryMeasure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Klabin.Rml.Client
{
    public partial class MeasureHistoryForm : Form
    {
        private readonly ReaderConfig _readerConfig;
        private readonly HistorySearchService _searchService;
        private Dictionary<Control, Label> dictionaryControlsWithError = new Dictionary<Control, Label>();

        public MeasureHistoryForm(ReaderConfig readerConfig, HistorySearchService historySearchService)
        {
            InitializeComponent();

            _searchService = historySearchService;
            _readerConfig = readerConfig;
        }

        private void MeasureHistoryForm_Load(object sender, EventArgs e)
        {
            //load driver type
            foreach (var item in Enum.GetNames(typeof(ReaderType)))
            {
                comboBoxDriverType.Items.Add(item);
            }

            //load sync option
            comboBoxSyncOption.Items.Add("TODAS");
            comboBoxSyncOption.Items.Add("Não enviadas");
            comboBoxSyncOption.Items.Add("Enviadas");


            //load dates
            dateTimePickerDateInitital.Value = DateTime.Now;
            dateTimePickerDateFinal.Value = DateTime.Now.AddDays(1);

            //setup machine config
            textBoxMachine.Text = _readerConfig.MachineNumber;
            textBoxMachine.Enabled = false;
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var request = GetRequestModel();
                if (request == null)
                {
                    return;
                }


                var historyValues = await _searchService.SearchHistoryAsync(request);

                dataGridViewResult.AutoGenerateColumns = false;
                dataGridViewResult.DataSource = historyValues;
                dataGridViewResult.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar a consulta do histórico. Erro: {ex.Message}", "Erro na operação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public HistoryMachineDataRequest GetRequestModel()
        {
            HistoryMachineDataRequest request = new();

            request.MachineNumber = textBoxMachine.Text;
            request.MeasureType = textBoxMeasureType.Text;
            request.InitialDate = dateTimePickerDateInitital.Value.Date;
            request.FinalDate = dateTimePickerDateFinal.Value.Date;
            request.RollNumber = textBoxRoll.Text;
            request.CutNumber = textBoxCut.Text;

            //validate final < initial
            if (request.FinalDate.Value.CompareTo(request.InitialDate) < 0)
            {
                ShowMessageValidationError("A data final não pode ser menor que a data final", dateTimePickerDateFinal, labelDateFinal);
                return null;
            }

            //check 30 days max
            if ((request.FinalDate - request.InitialDate).Value.TotalDays > 30)
            {
                ShowMessageValidationError("A faixa de datas pesquisada não pode ser maior que 30 dias", dateTimePickerDateFinal, labelDateFinal);
                return null;
            }

            if (comboBoxDriverType.SelectedItem != null)
            {
                request.DriverType = comboBoxDriverType.SelectedItem.ToString();
            }
            if (comboBoxSyncOption.SelectedItem != null)
            {
                if (comboBoxSyncOption.SelectedItem.ToString() == "Não enviadas")
                {
                    request.Synchronized = false;
                }
                else if (comboBoxSyncOption.SelectedItem.ToString() == "Enviadas")
                {
                    request.Synchronized = true;
                }
            }

            return request;
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
                var senderControl = sender as Control;
                senderControl.ForeColor = Color.Black;
                senderControl.LostFocus -= ControlWithError_LostFocus;

                //lookup on the dictionary
                if (dictionaryControlsWithError.ContainsKey(senderControl))
                {
                    dictionaryControlsWithError[senderControl].ForeColor = Color.Black;
                    dictionaryControlsWithError.Remove(senderControl);
                }
            }
        }
    }
}

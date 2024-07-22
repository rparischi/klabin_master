using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klabin.Rml.Client
{
    public partial class UnlockForm : Form
    {
        private string _admCode;
        private int quantityTry;
        private Dictionary<Control, Label> dictionaryControlsWithError = new Dictionary<Control, Label>();
        private readonly Action _notifyUnblock;

        public UnlockForm(string admCode, Action notifyUnblock)
        {
            InitializeComponent();

            _admCode = admCode;
            _notifyUnblock = notifyUnblock;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (quantityTry > 3)
            {
                this.Close();
            }

            // increment trys
            quantityTry++;


            // checkk form erros
            if (string.IsNullOrWhiteSpace(textBoxAdmCode.Text))
            {
                ShowMessageValidationError("O código do ADM não pode ser vazio", textBoxAdmCode, labelAdmCode);
                return;
            }

            // check diff
            if (textBoxAdmCode.Text != _admCode)
            {
                ShowMessageValidationError("Código inválido", textBoxAdmCode, labelAdmCode);
                return;
            }

            // if here, code is equals
            _notifyUnblock();
            this.Close();
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
                    dictionaryControlsWithError.TryAdd(controlWithError, controlWithErrorLabel);
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
                    dictionaryControlsWithError.Remove(senderControl);
                }
            }
        }

        private void textBoxAdmCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(sender, e);
            }
        }

        private void UnlockForm_Shown(object sender, EventArgs e)
        {
            textBoxAdmCode.Focus();
        }
    }
}

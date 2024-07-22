
namespace Klabin.Rml.Client
{
    partial class MessureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessureForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxRollNumber = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxCutNumber = new System.Windows.Forms.MaskedTextBox();
            this.dateTimePickerCreationDate = new System.Windows.Forms.DateTimePicker();
            this.labelReadTime = new System.Windows.Forms.Label();
            this.labelTituloDataLeitura = new System.Windows.Forms.Label();
            this.labelTituloDataCriacao = new System.Windows.Forms.Label();
            this.labelMachine = new System.Windows.Forms.Label();
            this.labelTituloMaquina = new System.Windows.Forms.Label();
            this.labelTituloNumeroTirada = new System.Windows.Forms.Label();
            this.labelTituloNumeroRolo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelMachineReadParameters = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonEvniarDados = new System.Windows.Forms.Button();
            this.groupBoxHistorico = new System.Windows.Forms.GroupBox();
            this.labelLastRollDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLastCutNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLastRollNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelMachineReadParameters.SuspendLayout();
            this.groupBoxHistorico.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.maskedTextBoxRollNumber);
            this.groupBox1.Controls.Add(this.maskedTextBoxCutNumber);
            this.groupBox1.Controls.Add(this.dateTimePickerCreationDate);
            this.groupBox1.Controls.Add(this.labelReadTime);
            this.groupBox1.Controls.Add(this.labelTituloDataLeitura);
            this.groupBox1.Controls.Add(this.labelTituloDataCriacao);
            this.groupBox1.Controls.Add(this.labelMachine);
            this.groupBox1.Controls.Add(this.labelTituloMaquina);
            this.groupBox1.Controls.Add(this.labelTituloNumeroTirada);
            this.groupBox1.Controls.Add(this.labelTituloNumeroRolo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do rolo/ bobina mãe";
            // 
            // maskedTextBoxRollNumber
            // 
            this.maskedTextBoxRollNumber.AllowPromptAsInput = false;
            this.maskedTextBoxRollNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maskedTextBoxRollNumber.HidePromptOnLeave = true;
            this.maskedTextBoxRollNumber.Location = new System.Drawing.Point(14, 119);
            this.maskedTextBoxRollNumber.Mask = "00";
            this.maskedTextBoxRollNumber.Name = "maskedTextBoxRollNumber";
            this.maskedTextBoxRollNumber.Size = new System.Drawing.Size(174, 29);
            this.maskedTextBoxRollNumber.TabIndex = 4;
            this.maskedTextBoxRollNumber.ValidatingType = typeof(int);
            // 
            // maskedTextBoxCutNumber
            // 
            this.maskedTextBoxCutNumber.AllowPromptAsInput = false;
            this.maskedTextBoxCutNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maskedTextBoxCutNumber.HidePromptOnLeave = true;
            this.maskedTextBoxCutNumber.Location = new System.Drawing.Point(219, 119);
            this.maskedTextBoxCutNumber.Mask = "00";
            this.maskedTextBoxCutNumber.Name = "maskedTextBoxCutNumber";
            this.maskedTextBoxCutNumber.Size = new System.Drawing.Size(174, 29);
            this.maskedTextBoxCutNumber.TabIndex = 5;
            this.maskedTextBoxCutNumber.ValidatingType = typeof(int);
            // 
            // dateTimePickerCreationDate
            // 
            this.dateTimePickerCreationDate.CalendarFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerCreationDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerCreationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerCreationDate.Location = new System.Drawing.Point(219, 48);
            this.dateTimePickerCreationDate.Name = "dateTimePickerCreationDate";
            this.dateTimePickerCreationDate.Size = new System.Drawing.Size(231, 29);
            this.dateTimePickerCreationDate.TabIndex = 2;
            // 
            // labelReadTime
            // 
            this.labelReadTime.AutoSize = true;
            this.labelReadTime.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelReadTime.ForeColor = System.Drawing.Color.Green;
            this.labelReadTime.Location = new System.Drawing.Point(522, 48);
            this.labelReadTime.Name = "labelReadTime";
            this.labelReadTime.Size = new System.Drawing.Size(170, 25);
            this.labelReadTime.TabIndex = 3;
            this.labelReadTime.Text = "10/11/2021 10:20";
            // 
            // labelTituloDataLeitura
            // 
            this.labelTituloDataLeitura.AutoSize = true;
            this.labelTituloDataLeitura.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTituloDataLeitura.Location = new System.Drawing.Point(522, 19);
            this.labelTituloDataLeitura.Name = "labelTituloDataLeitura";
            this.labelTituloDataLeitura.Size = new System.Drawing.Size(152, 25);
            this.labelTituloDataLeitura.TabIndex = 9;
            this.labelTituloDataLeitura.Text = "Data da Leitura:";
            // 
            // labelTituloDataCriacao
            // 
            this.labelTituloDataCriacao.AutoSize = true;
            this.labelTituloDataCriacao.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTituloDataCriacao.Location = new System.Drawing.Point(213, 20);
            this.labelTituloDataCriacao.Name = "labelTituloDataCriacao";
            this.labelTituloDataCriacao.Size = new System.Drawing.Size(231, 25);
            this.labelTituloDataCriacao.TabIndex = 6;
            this.labelTituloDataCriacao.Text = "Data de Criação do Rolo:";
            // 
            // labelMachine
            // 
            this.labelMachine.AutoSize = true;
            this.labelMachine.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMachine.ForeColor = System.Drawing.Color.Green;
            this.labelMachine.Location = new System.Drawing.Point(9, 48);
            this.labelMachine.Name = "labelMachine";
            this.labelMachine.Size = new System.Drawing.Size(34, 25);
            this.labelMachine.TabIndex = 1;
            this.labelMachine.Text = "07";
            // 
            // labelTituloMaquina
            // 
            this.labelTituloMaquina.AutoSize = true;
            this.labelTituloMaquina.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTituloMaquina.Location = new System.Drawing.Point(9, 19);
            this.labelTituloMaquina.Name = "labelTituloMaquina";
            this.labelTituloMaquina.Size = new System.Drawing.Size(96, 25);
            this.labelTituloMaquina.TabIndex = 4;
            this.labelTituloMaquina.Text = "Máquina:";
            // 
            // labelTituloNumeroTirada
            // 
            this.labelTituloNumeroTirada.AutoSize = true;
            this.labelTituloNumeroTirada.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTituloNumeroTirada.Location = new System.Drawing.Point(214, 91);
            this.labelTituloNumeroTirada.Name = "labelTituloNumeroTirada";
            this.labelTituloNumeroTirada.Size = new System.Drawing.Size(179, 25);
            this.labelTituloNumeroTirada.TabIndex = 0;
            this.labelTituloNumeroTirada.Text = "Número da Tirada:";
            // 
            // labelTituloNumeroRolo
            // 
            this.labelTituloNumeroRolo.AutoSize = true;
            this.labelTituloNumeroRolo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTituloNumeroRolo.Location = new System.Drawing.Point(10, 90);
            this.labelTituloNumeroRolo.Name = "labelTituloNumeroRolo";
            this.labelTituloNumeroRolo.Size = new System.Drawing.Size(166, 25);
            this.labelTituloNumeroRolo.TabIndex = 2;
            this.labelTituloNumeroRolo.Text = "Número do Rolo:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.panelMachineReadParameters);
            this.groupBox2.Location = new System.Drawing.Point(12, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 339);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Leituras";
            // 
            // panelMachineReadParameters
            // 
            this.panelMachineReadParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMachineReadParameters.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelMachineReadParameters.Controls.Add(this.textBox1);
            this.panelMachineReadParameters.Controls.Add(this.label1);
            this.panelMachineReadParameters.Controls.Add(this.textBox4);
            this.panelMachineReadParameters.Controls.Add(this.label4);
            this.panelMachineReadParameters.Location = new System.Drawing.Point(6, 16);
            this.panelMachineReadParameters.Name = "panelMachineReadParameters";
            this.panelMachineReadParameters.Size = new System.Drawing.Size(720, 290);
            this.panelMachineReadParameters.TabIndex = 100;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(224, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 29);
            this.textBox1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(224, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Diâmetro medido:";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox4.Location = new System.Drawing.Point(8, 47);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(174, 29);
            this.textBox4.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(8, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Metragem lida:";
            // 
            // buttonEvniarDados
            // 
            this.buttonEvniarDados.BackColor = System.Drawing.Color.OliveDrab;
            this.buttonEvniarDados.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonEvniarDados.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonEvniarDados.Location = new System.Drawing.Point(596, 630);
            this.buttonEvniarDados.Name = "buttonEvniarDados";
            this.buttonEvniarDados.Size = new System.Drawing.Size(142, 38);
            this.buttonEvniarDados.TabIndex = 500;
            this.buttonEvniarDados.Text = "Enviar";
            this.buttonEvniarDados.UseVisualStyleBackColor = false;
            // 
            // groupBoxHistorico
            // 
            this.groupBoxHistorico.Controls.Add(this.labelLastRollDate);
            this.groupBoxHistorico.Controls.Add(this.label3);
            this.groupBoxHistorico.Controls.Add(this.labelLastCutNumber);
            this.groupBoxHistorico.Controls.Add(this.label5);
            this.groupBoxHistorico.Controls.Add(this.labelLastRollNumber);
            this.groupBoxHistorico.Controls.Add(this.label2);
            this.groupBoxHistorico.Location = new System.Drawing.Point(12, 186);
            this.groupBoxHistorico.Name = "groupBoxHistorico";
            this.groupBoxHistorico.Size = new System.Drawing.Size(725, 93);
            this.groupBoxHistorico.TabIndex = 501;
            this.groupBoxHistorico.TabStop = false;
            this.groupBoxHistorico.Text = "Última leitura";
            // 
            // labelLastRollDate
            // 
            this.labelLastRollDate.AutoSize = true;
            this.labelLastRollDate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelLastRollDate.ForeColor = System.Drawing.Color.Black;
            this.labelLastRollDate.Location = new System.Drawing.Point(461, 44);
            this.labelLastRollDate.Name = "labelLastRollDate";
            this.labelLastRollDate.Size = new System.Drawing.Size(92, 25);
            this.labelLastRollDate.TabIndex = 14;
            this.labelLastRollDate.Text = "__/__/____";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(461, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Data de Criação do Rolo:";
            // 
            // labelLastCutNumber
            // 
            this.labelLastCutNumber.AutoSize = true;
            this.labelLastCutNumber.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelLastCutNumber.ForeColor = System.Drawing.Color.Black;
            this.labelLastCutNumber.Location = new System.Drawing.Point(219, 44);
            this.labelLastCutNumber.Name = "labelLastCutNumber";
            this.labelLastCutNumber.Size = new System.Drawing.Size(28, 25);
            this.labelLastCutNumber.TabIndex = 12;
            this.labelLastCutNumber.Text = "__";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(219, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Número da Tirada:";
            // 
            // labelLastRollNumber
            // 
            this.labelLastRollNumber.AutoSize = true;
            this.labelLastRollNumber.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelLastRollNumber.ForeColor = System.Drawing.Color.Black;
            this.labelLastRollNumber.Location = new System.Drawing.Point(10, 44);
            this.labelLastRollNumber.Name = "labelLastRollNumber";
            this.labelLastRollNumber.Size = new System.Drawing.Size(28, 25);
            this.labelLastRollNumber.TabIndex = 10;
            this.labelLastRollNumber.Text = "__";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Número Rolo:";
            // 
            // MessureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(749, 678);
            this.Controls.Add(this.groupBoxHistorico);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonEvniarDados);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lançamento metragem linear";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessureForm_FormClosing);
            this.Load += new System.EventHandler(this.LancamentoMetragemForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panelMachineReadParameters.ResumeLayout(false);
            this.panelMachineReadParameters.PerformLayout();
            this.groupBoxHistorico.ResumeLayout(false);
            this.groupBoxHistorico.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelTituloNumeroRolo;
        private System.Windows.Forms.Label labelTituloNumeroTirada;
        private System.Windows.Forms.Button buttonEvniarDados;
        private System.Windows.Forms.Label labelTituloMaquina;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTituloDataCriacao;
        private System.Windows.Forms.Label labelMachine;
        private System.Windows.Forms.DateTimePicker dateTimePickerCreationDate;
        private System.Windows.Forms.Label labelReadTime;
        private System.Windows.Forms.Label labelTituloDataLeitura;
        private System.Windows.Forms.Panel panelMachineReadParameters;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxRollNumber;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCutNumber;
        private System.Windows.Forms.GroupBox groupBoxHistorico;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLastRollNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLastCutNumber;
        private System.Windows.Forms.Label labelLastRollDate;
        private System.Windows.Forms.Label label3;
    }
}
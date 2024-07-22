
namespace Klabin.Rml.Client
{
    partial class MeasureHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasureHistoryForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelCut = new System.Windows.Forms.Label();
            this.textBoxCut = new System.Windows.Forms.TextBox();
            this.labelRoll = new System.Windows.Forms.Label();
            this.textBoxRoll = new System.Windows.Forms.TextBox();
            this.dateTimePickerDateFinal = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDateInitital = new System.Windows.Forms.DateTimePicker();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxSyncOption = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxDriverType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelDateFinal = new System.Windows.Forms.Label();
            this.labelDateInitial = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMeasureType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMachine = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.ColumnSync = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMachineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRoll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDateSync = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMeasureType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMeasureValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelCut);
            this.groupBox1.Controls.Add(this.textBoxCut);
            this.groupBox1.Controls.Add(this.labelRoll);
            this.groupBox1.Controls.Add(this.textBoxRoll);
            this.groupBox1.Controls.Add(this.dateTimePickerDateFinal);
            this.groupBox1.Controls.Add(this.dateTimePickerDateInitital);
            this.groupBox1.Controls.Add(this.buttonSearch);
            this.groupBox1.Controls.Add(this.comboBoxSyncOption);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxDriverType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelDateFinal);
            this.groupBox1.Controls.Add(this.labelDateInitial);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxMeasureType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxMachine);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1001, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // labelCut
            // 
            this.labelCut.AutoSize = true;
            this.labelCut.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCut.Location = new System.Drawing.Point(474, 19);
            this.labelCut.Name = "labelCut";
            this.labelCut.Size = new System.Drawing.Size(54, 20);
            this.labelCut.TabIndex = 15;
            this.labelCut.Text = "Tirada:";
            // 
            // textBoxCut
            // 
            this.textBoxCut.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxCut.Location = new System.Drawing.Point(476, 42);
            this.textBoxCut.MaxLength = 3;
            this.textBoxCut.Name = "textBoxCut";
            this.textBoxCut.Size = new System.Drawing.Size(156, 25);
            this.textBoxCut.TabIndex = 14;
            // 
            // labelRoll
            // 
            this.labelRoll.AutoSize = true;
            this.labelRoll.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelRoll.Location = new System.Drawing.Point(312, 19);
            this.labelRoll.Name = "labelRoll";
            this.labelRoll.Size = new System.Drawing.Size(86, 20);
            this.labelRoll.TabIndex = 13;
            this.labelRoll.Text = "Nº do Rolo:";
            // 
            // textBoxRoll
            // 
            this.textBoxRoll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxRoll.Location = new System.Drawing.Point(314, 42);
            this.textBoxRoll.MaxLength = 3;
            this.textBoxRoll.Name = "textBoxRoll";
            this.textBoxRoll.Size = new System.Drawing.Size(156, 25);
            this.textBoxRoll.TabIndex = 12;
            // 
            // dateTimePickerDateFinal
            // 
            this.dateTimePickerDateFinal.CalendarFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerDateFinal.CustomFormat = "";
            this.dateTimePickerDateFinal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerDateFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateFinal.Location = new System.Drawing.Point(508, 101);
            this.dateTimePickerDateFinal.Name = "dateTimePickerDateFinal";
            this.dateTimePickerDateFinal.Size = new System.Drawing.Size(142, 25);
            this.dateTimePickerDateFinal.TabIndex = 3;
            this.dateTimePickerDateFinal.Value = new System.DateTime(2021, 12, 6, 0, 0, 0, 0);
            // 
            // dateTimePickerDateInitital
            // 
            this.dateTimePickerDateInitital.CalendarFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerDateInitital.CustomFormat = "";
            this.dateTimePickerDateInitital.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTimePickerDateInitital.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateInitital.Location = new System.Drawing.Point(342, 100);
            this.dateTimePickerDateInitital.Name = "dateTimePickerDateInitital";
            this.dateTimePickerDateInitital.Size = new System.Drawing.Size(142, 25);
            this.dateTimePickerDateInitital.TabIndex = 2;
            this.dateTimePickerDateInitital.Value = new System.DateTime(2021, 12, 6, 0, 0, 0, 0);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.LightBlue;
            this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSearch.Location = new System.Drawing.Point(682, 88);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(138, 38);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Pesquisar";
            this.buttonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxSyncOption
            // 
            this.comboBoxSyncOption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxSyncOption.FormattingEnabled = true;
            this.comboBoxSyncOption.Location = new System.Drawing.Point(149, 101);
            this.comboBoxSyncOption.Name = "comboBoxSyncOption";
            this.comboBoxSyncOption.Size = new System.Drawing.Size(175, 25);
            this.comboBoxSyncOption.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(145, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Sincronizadas (enviadas):";
            // 
            // comboBoxDriverType
            // 
            this.comboBoxDriverType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxDriverType.FormattingEnabled = true;
            this.comboBoxDriverType.Location = new System.Drawing.Point(6, 101);
            this.comboBoxDriverType.Name = "comboBoxDriverType";
            this.comboBoxDriverType.Size = new System.Drawing.Size(121, 25);
            this.comboBoxDriverType.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(4, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tipo Driver:";
            // 
            // labelDateFinal
            // 
            this.labelDateFinal.AutoSize = true;
            this.labelDateFinal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDateFinal.Location = new System.Drawing.Point(504, 78);
            this.labelDateFinal.Name = "labelDateFinal";
            this.labelDateFinal.Size = new System.Drawing.Size(77, 20);
            this.labelDateFinal.TabIndex = 7;
            this.labelDateFinal.Text = "Data final:";
            // 
            // labelDateInitial
            // 
            this.labelDateInitial.AutoSize = true;
            this.labelDateInitial.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDateInitial.Location = new System.Drawing.Point(342, 78);
            this.labelDateInitial.Name = "labelDateInitial";
            this.labelDateInitial.Size = new System.Drawing.Size(87, 20);
            this.labelDateInitial.TabIndex = 5;
            this.labelDateInitial.Text = "Data inicial:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(126, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo de Medida:";
            // 
            // textBoxMeasureType
            // 
            this.textBoxMeasureType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMeasureType.Location = new System.Drawing.Point(132, 42);
            this.textBoxMeasureType.Name = "textBoxMeasureType";
            this.textBoxMeasureType.Size = new System.Drawing.Size(156, 25);
            this.textBoxMeasureType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Máquina:";
            // 
            // textBoxMachine
            // 
            this.textBoxMachine.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMachine.Location = new System.Drawing.Point(6, 42);
            this.textBoxMachine.MaxLength = 3;
            this.textBoxMachine.Name = "textBoxMachine";
            this.textBoxMachine.Size = new System.Drawing.Size(111, 25);
            this.textBoxMachine.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridViewResult);
            this.groupBox2.Location = new System.Drawing.Point(13, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1000, 552);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultado";
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.AllowUserToAddRows = false;
            this.dataGridViewResult.AllowUserToDeleteRows = false;
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSync,
            this.ColumnMachineNumber,
            this.ColumnRoll,
            this.ColumnCut,
            this.ColumnDate,
            this.ColumnDateSync,
            this.ColumnMeasureType,
            this.ColumnMeasureValue});
            this.dataGridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResult.Location = new System.Drawing.Point(3, 19);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.ReadOnly = true;
            this.dataGridViewResult.RowTemplate.Height = 25;
            this.dataGridViewResult.Size = new System.Drawing.Size(994, 530);
            this.dataGridViewResult.TabIndex = 10;
            // 
            // ColumnSync
            // 
            this.ColumnSync.DataPropertyName = "SynchronizedString";
            this.ColumnSync.HeaderText = "Sincronizada?";
            this.ColumnSync.MaxInputLength = 10;
            this.ColumnSync.Name = "ColumnSync";
            this.ColumnSync.ReadOnly = true;
            this.ColumnSync.ToolTipText = "Indica se o valor já foi enviado ao SRP ou não";
            // 
            // ColumnMachineNumber
            // 
            this.ColumnMachineNumber.DataPropertyName = "MachineNumber";
            this.ColumnMachineNumber.HeaderText = "Nº Máquina";
            this.ColumnMachineNumber.MaxInputLength = 3;
            this.ColumnMachineNumber.Name = "ColumnMachineNumber";
            this.ColumnMachineNumber.ReadOnly = true;
            // 
            // ColumnRoll
            // 
            this.ColumnRoll.DataPropertyName = "RollNumber";
            this.ColumnRoll.HeaderText = "Nº Rolo";
            this.ColumnRoll.MaxInputLength = 3;
            this.ColumnRoll.Name = "ColumnRoll";
            this.ColumnRoll.ReadOnly = true;
            // 
            // ColumnCut
            // 
            this.ColumnCut.DataPropertyName = "CutNumber";
            this.ColumnCut.HeaderText = "Tirada";
            this.ColumnCut.MaxInputLength = 2;
            this.ColumnCut.Name = "ColumnCut";
            this.ColumnCut.ReadOnly = true;
            // 
            // ColumnDate
            // 
            this.ColumnDate.DataPropertyName = "RollDate";
            this.ColumnDate.HeaderText = "Data Rolo";
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.ReadOnly = true;
            this.ColumnDate.ToolTipText = "Data da criação do rolo";
            // 
            // ColumnDateSync
            // 
            this.ColumnDateSync.DataPropertyName = "SyncTime";
            this.ColumnDateSync.HeaderText = "Data Envio";
            this.ColumnDateSync.Name = "ColumnDateSync";
            this.ColumnDateSync.ReadOnly = true;
            this.ColumnDateSync.ToolTipText = "Data do envio para o SRP";
            // 
            // ColumnMeasureType
            // 
            this.ColumnMeasureType.DataPropertyName = "MeasureType";
            this.ColumnMeasureType.HeaderText = "Tipo Medida";
            this.ColumnMeasureType.Name = "ColumnMeasureType";
            this.ColumnMeasureType.ReadOnly = true;
            this.ColumnMeasureType.ToolTipText = "Tipo da medida capturada da automação";
            // 
            // ColumnMeasureValue
            // 
            this.ColumnMeasureValue.DataPropertyName = "MeasureValue";
            this.ColumnMeasureValue.HeaderText = "Valor Medido";
            this.ColumnMeasureValue.Name = "ColumnMeasureValue";
            this.ColumnMeasureValue.ReadOnly = true;
            // 
            // MeasureHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1025, 714);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MeasureHistoryForm";
            this.Text = "Histórico de medidas";
            this.Load += new System.EventHandler(this.MeasureHistoryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.Label labelDateFinal;
        private System.Windows.Forms.Label labelDateInitial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMeasureType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMachine;
        private System.Windows.Forms.ComboBox comboBoxDriverType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ComboBox comboBoxSyncOption;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateFinal;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateInitital;
        private System.Windows.Forms.Label labelCut;
        private System.Windows.Forms.TextBox textBoxCut;
        private System.Windows.Forms.Label labelRoll;
        private System.Windows.Forms.TextBox textBoxRoll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSync;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMachineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRoll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCut;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateSync;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeasureType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeasureValue;
    }
}
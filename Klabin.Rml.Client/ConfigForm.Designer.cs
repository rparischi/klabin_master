
namespace Klabin.Rml.Client
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            buttonSalvar = new System.Windows.Forms.Button();
            buttonFechar = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            dataGridViewReadParameters = new System.Windows.Forms.DataGridView();
            ColumnPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            groupBoxTcpConfig = new System.Windows.Forms.GroupBox();
            textBoxTCPWriteTimeout = new System.Windows.Forms.TextBox();
            textBoxTCPPort = new System.Windows.Forms.TextBox();
            labelTCPWriteTimeout = new System.Windows.Forms.Label();
            labelTCPPort = new System.Windows.Forms.Label();
            textBoxTCPReadTimeout = new System.Windows.Forms.TextBox();
            labelTCPReadTimeout = new System.Windows.Forms.Label();
            textBoxTCPAddress = new System.Windows.Forms.TextBox();
            labelTCPAdress = new System.Windows.Forms.Label();
            groupBoxSerialConfig = new System.Windows.Forms.GroupBox();
            comboBoxSerialStopBits = new System.Windows.Forms.ComboBox();
            comboBoxSerialParity = new System.Windows.Forms.ComboBox();
            textBoxSerialWriteTimeout = new System.Windows.Forms.TextBox();
            labelSerialWriteTimeout = new System.Windows.Forms.Label();
            textBoxSerialReadTimeout = new System.Windows.Forms.TextBox();
            labelSerialReadtimeout = new System.Windows.Forms.Label();
            textBoxSerialDataBits = new System.Windows.Forms.TextBox();
            labelSerialDataBits = new System.Windows.Forms.Label();
            labelSerialStopBits = new System.Windows.Forms.Label();
            labelSerialParity = new System.Windows.Forms.Label();
            textBoxSerialBoundRate = new System.Windows.Forms.TextBox();
            labelSerialBoundRate = new System.Windows.Forms.Label();
            textBoxSerialPortComName = new System.Windows.Forms.TextBox();
            labelSerialComPort = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label16 = new System.Windows.Forms.Label();
            comboBoxMachineType = new System.Windows.Forms.ComboBox();
            textBoxUrlApi = new System.Windows.Forms.TextBox();
            labelUrlApi = new System.Windows.Forms.Label();
            textBoxUrlSafariApi = new System.Windows.Forms.TextBox();
            labelUrlSafariApi = new System.Windows.Forms.Label();
            textBoxQtdDiasSafari = new System.Windows.Forms.TextBox();
            labelQtdDiasSafari = new System.Windows.Forms.Label();
            comboBoxDebugMode = new System.Windows.Forms.ComboBox();
            labelDebugMode = new System.Windows.Forms.Label();
            textBoxLookupTime = new System.Windows.Forms.TextBox();
            labelLookupTime = new System.Windows.Forms.Label();
            comboBoxDriverType = new System.Windows.Forms.ComboBox();
            labelDriverType = new System.Windows.Forms.Label();
            textBoxMachine = new System.Windows.Forms.TextBox();
            labelMachine = new System.Windows.Forms.Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReadParameters).BeginInit();
            groupBoxTcpConfig.SuspendLayout();
            groupBoxSerialConfig.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonSalvar
            // 
            buttonSalvar.BackColor = System.Drawing.Color.PaleGreen;
            buttonSalvar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonSalvar.Location = new System.Drawing.Point(662, 727);
            buttonSalvar.Name = "buttonSalvar";
            buttonSalvar.Size = new System.Drawing.Size(130, 33);
            buttonSalvar.TabIndex = 71;
            buttonSalvar.Text = "Salvar";
            buttonSalvar.UseVisualStyleBackColor = false;
            buttonSalvar.Click += buttonSalvar_ClickAsync;
            // 
            // buttonFechar
            // 
            buttonFechar.BackColor = System.Drawing.Color.PaleTurquoise;
            buttonFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonFechar.Location = new System.Drawing.Point(12, 727);
            buttonFechar.Name = "buttonFechar";
            buttonFechar.Size = new System.Drawing.Size(130, 33);
            buttonFechar.TabIndex = 70;
            buttonFechar.Text = "Fechar";
            buttonFechar.UseVisualStyleBackColor = false;
            buttonFechar.Click += ButtonFechar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridViewReadParameters);
            groupBox2.Location = new System.Drawing.Point(16, 443);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(776, 278);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Parâmetros de leitura";
            // 
            // dataGridViewReadParameters
            // 
            dataGridViewReadParameters.AllowUserToResizeColumns = false;
            dataGridViewReadParameters.AllowUserToResizeRows = false;
            dataGridViewReadParameters.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridViewReadParameters.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewReadParameters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewReadParameters.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewReadParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReadParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColumnPosition, ColumnName, ColumnDescription, ColumnDataType });
            dataGridViewReadParameters.Location = new System.Drawing.Point(0, 22);
            dataGridViewReadParameters.MultiSelect = false;
            dataGridViewReadParameters.Name = "dataGridViewReadParameters";
            dataGridViewReadParameters.RowTemplate.Height = 25;
            dataGridViewReadParameters.Size = new System.Drawing.Size(760, 289);
            dataGridViewReadParameters.TabIndex = 60;
            dataGridViewReadParameters.RowValidating += dataGridViewReadParameters_RowValidating;
            // 
            // ColumnPosition
            // 
            ColumnPosition.HeaderText = "Posição";
            ColumnPosition.MaxInputLength = 2;
            ColumnPosition.Name = "ColumnPosition";
            ColumnPosition.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            ColumnPosition.ToolTipText = "Posição do parâmetro de leitura na lista";
            // 
            // ColumnName
            // 
            ColumnName.HeaderText = "Nome";
            ColumnName.Name = "ColumnName";
            ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            ColumnName.ToolTipText = "Nome do parâmetro de leitura esperado pelo driver";
            ColumnName.Width = 150;
            // 
            // ColumnDescription
            // 
            ColumnDescription.HeaderText = "Descrição";
            ColumnDescription.Name = "ColumnDescription";
            ColumnDescription.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            ColumnDescription.ToolTipText = "Nome em português do parâmetro de leitura";
            ColumnDescription.Width = 200;
            // 
            // ColumnDataType
            // 
            ColumnDataType.HeaderText = "Tipo Valor";
            ColumnDataType.Items.AddRange(new object[] { "Int", "Decimal" });
            ColumnDataType.Name = "ColumnDataType";
            ColumnDataType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            ColumnDataType.Width = 150;
            // 
            // groupBoxTcpConfig
            // 
            groupBoxTcpConfig.Controls.Add(textBoxTCPWriteTimeout);
            groupBoxTcpConfig.Controls.Add(textBoxTCPPort);
            groupBoxTcpConfig.Controls.Add(labelTCPWriteTimeout);
            groupBoxTcpConfig.Controls.Add(labelTCPPort);
            groupBoxTcpConfig.Controls.Add(textBoxTCPReadTimeout);
            groupBoxTcpConfig.Controls.Add(labelTCPReadTimeout);
            groupBoxTcpConfig.Controls.Add(textBoxTCPAddress);
            groupBoxTcpConfig.Controls.Add(labelTCPAdress);
            groupBoxTcpConfig.Location = new System.Drawing.Point(12, 257);
            groupBoxTcpConfig.Name = "groupBoxTcpConfig";
            groupBoxTcpConfig.Size = new System.Drawing.Size(783, 98);
            groupBoxTcpConfig.TabIndex = 4;
            groupBoxTcpConfig.TabStop = false;
            groupBoxTcpConfig.Text = "Parâmetros driver - TCP/IP";
            groupBoxTcpConfig.Visible = false;
            // 
            // textBoxTCPWriteTimeout
            // 
            textBoxTCPWriteTimeout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxTCPWriteTimeout.Location = new System.Drawing.Point(507, 60);
            textBoxTCPWriteTimeout.Name = "textBoxTCPWriteTimeout";
            textBoxTCPWriteTimeout.Size = new System.Drawing.Size(127, 29);
            textBoxTCPWriteTimeout.TabIndex = 23;
            // 
            // textBoxTCPPort
            // 
            textBoxTCPPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxTCPPort.Location = new System.Drawing.Point(183, 60);
            textBoxTCPPort.Name = "textBoxTCPPort";
            textBoxTCPPort.Size = new System.Drawing.Size(80, 29);
            textBoxTCPPort.TabIndex = 21;
            // 
            // labelTCPWriteTimeout
            // 
            labelTCPWriteTimeout.AutoSize = true;
            labelTCPWriteTimeout.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelTCPWriteTimeout.Location = new System.Drawing.Point(503, 32);
            labelTCPWriteTimeout.Name = "labelTCPWriteTimeout";
            labelTCPWriteTimeout.Size = new System.Drawing.Size(136, 25);
            labelTCPWriteTimeout.TabIndex = 24;
            labelTCPWriteTimeout.Text = "Write Timeout:";
            // 
            // labelTCPPort
            // 
            labelTCPPort.AutoSize = true;
            labelTCPPort.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelTCPPort.Location = new System.Drawing.Point(179, 32);
            labelTCPPort.Name = "labelTCPPort";
            labelTCPPort.Size = new System.Drawing.Size(60, 25);
            labelTCPPort.TabIndex = 24;
            labelTCPPort.Text = "Porta:";
            // 
            // textBoxTCPReadTimeout
            // 
            textBoxTCPReadTimeout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxTCPReadTimeout.Location = new System.Drawing.Point(363, 60);
            textBoxTCPReadTimeout.Name = "textBoxTCPReadTimeout";
            textBoxTCPReadTimeout.Size = new System.Drawing.Size(127, 29);
            textBoxTCPReadTimeout.TabIndex = 22;
            // 
            // labelTCPReadTimeout
            // 
            labelTCPReadTimeout.AutoSize = true;
            labelTCPReadTimeout.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelTCPReadTimeout.Location = new System.Drawing.Point(359, 32);
            labelTCPReadTimeout.Name = "labelTCPReadTimeout";
            labelTCPReadTimeout.Size = new System.Drawing.Size(131, 25);
            labelTCPReadTimeout.TabIndex = 22;
            labelTCPReadTimeout.Text = "Read Timeout:";
            // 
            // textBoxTCPAddress
            // 
            textBoxTCPAddress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxTCPAddress.Location = new System.Drawing.Point(15, 60);
            textBoxTCPAddress.Name = "textBoxTCPAddress";
            textBoxTCPAddress.Size = new System.Drawing.Size(154, 29);
            textBoxTCPAddress.TabIndex = 20;
            // 
            // labelTCPAdress
            // 
            labelTCPAdress.AutoSize = true;
            labelTCPAdress.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelTCPAdress.Location = new System.Drawing.Point(11, 32);
            labelTCPAdress.Name = "labelTCPAdress";
            labelTCPAdress.Size = new System.Drawing.Size(116, 25);
            labelTCPAdress.TabIndex = 22;
            labelTCPAdress.Text = "Endereço IP:";
            // 
            // groupBoxSerialConfig
            // 
            groupBoxSerialConfig.Controls.Add(comboBoxSerialStopBits);
            groupBoxSerialConfig.Controls.Add(comboBoxSerialParity);
            groupBoxSerialConfig.Controls.Add(textBoxSerialWriteTimeout);
            groupBoxSerialConfig.Controls.Add(labelSerialWriteTimeout);
            groupBoxSerialConfig.Controls.Add(textBoxSerialReadTimeout);
            groupBoxSerialConfig.Controls.Add(labelSerialReadtimeout);
            groupBoxSerialConfig.Controls.Add(textBoxSerialDataBits);
            groupBoxSerialConfig.Controls.Add(labelSerialDataBits);
            groupBoxSerialConfig.Controls.Add(labelSerialStopBits);
            groupBoxSerialConfig.Controls.Add(labelSerialParity);
            groupBoxSerialConfig.Controls.Add(textBoxSerialBoundRate);
            groupBoxSerialConfig.Controls.Add(labelSerialBoundRate);
            groupBoxSerialConfig.Controls.Add(textBoxSerialPortComName);
            groupBoxSerialConfig.Controls.Add(labelSerialComPort);
            groupBoxSerialConfig.Location = new System.Drawing.Point(16, 257);
            groupBoxSerialConfig.Name = "groupBoxSerialConfig";
            groupBoxSerialConfig.Size = new System.Drawing.Size(779, 180);
            groupBoxSerialConfig.TabIndex = 3;
            groupBoxSerialConfig.TabStop = false;
            groupBoxSerialConfig.Text = "Parâmetros driver - SERIAL/COM";
            groupBoxSerialConfig.Visible = false;
            // 
            // comboBoxSerialStopBits
            // 
            comboBoxSerialStopBits.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxSerialStopBits.FormattingEnabled = true;
            comboBoxSerialStopBits.Items.AddRange(new object[] { "None", "One", "Two", "OnePointFive" });
            comboBoxSerialStopBits.Location = new System.Drawing.Point(437, 61);
            comboBoxSerialStopBits.Name = "comboBoxSerialStopBits";
            comboBoxSerialStopBits.Size = new System.Drawing.Size(121, 29);
            comboBoxSerialStopBits.TabIndex = 13;
            // 
            // comboBoxSerialParity
            // 
            comboBoxSerialParity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxSerialParity.FormattingEnabled = true;
            comboBoxSerialParity.Items.AddRange(new object[] { "None", "Odd", "Even", "Mark", "Space" });
            comboBoxSerialParity.Location = new System.Drawing.Point(287, 61);
            comboBoxSerialParity.Name = "comboBoxSerialParity";
            comboBoxSerialParity.Size = new System.Drawing.Size(121, 29);
            comboBoxSerialParity.TabIndex = 12;
            // 
            // textBoxSerialWriteTimeout
            // 
            textBoxSerialWriteTimeout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSerialWriteTimeout.Location = new System.Drawing.Point(192, 139);
            textBoxSerialWriteTimeout.MaxLength = 5;
            textBoxSerialWriteTimeout.Name = "textBoxSerialWriteTimeout";
            textBoxSerialWriteTimeout.Size = new System.Drawing.Size(127, 29);
            textBoxSerialWriteTimeout.TabIndex = 16;
            // 
            // labelSerialWriteTimeout
            // 
            labelSerialWriteTimeout.AutoSize = true;
            labelSerialWriteTimeout.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSerialWriteTimeout.Location = new System.Drawing.Point(183, 111);
            labelSerialWriteTimeout.Name = "labelSerialWriteTimeout";
            labelSerialWriteTimeout.Size = new System.Drawing.Size(136, 25);
            labelSerialWriteTimeout.TabIndex = 18;
            labelSerialWriteTimeout.Text = "Write Timeout:";
            // 
            // textBoxSerialReadTimeout
            // 
            textBoxSerialReadTimeout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSerialReadTimeout.Location = new System.Drawing.Point(15, 139);
            textBoxSerialReadTimeout.MaxLength = 5;
            textBoxSerialReadTimeout.Name = "textBoxSerialReadTimeout";
            textBoxSerialReadTimeout.Size = new System.Drawing.Size(127, 29);
            textBoxSerialReadTimeout.TabIndex = 15;
            // 
            // labelSerialReadtimeout
            // 
            labelSerialReadtimeout.AutoSize = true;
            labelSerialReadtimeout.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSerialReadtimeout.Location = new System.Drawing.Point(11, 111);
            labelSerialReadtimeout.Name = "labelSerialReadtimeout";
            labelSerialReadtimeout.Size = new System.Drawing.Size(131, 25);
            labelSerialReadtimeout.TabIndex = 16;
            labelSerialReadtimeout.Text = "Read Timeout:";
            // 
            // textBoxSerialDataBits
            // 
            textBoxSerialDataBits.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSerialDataBits.Location = new System.Drawing.Point(583, 61);
            textBoxSerialDataBits.MaxLength = 2;
            textBoxSerialDataBits.Name = "textBoxSerialDataBits";
            textBoxSerialDataBits.Size = new System.Drawing.Size(100, 29);
            textBoxSerialDataBits.TabIndex = 14;
            // 
            // labelSerialDataBits
            // 
            labelSerialDataBits.AutoSize = true;
            labelSerialDataBits.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSerialDataBits.Location = new System.Drawing.Point(579, 33);
            labelSerialDataBits.Name = "labelSerialDataBits";
            labelSerialDataBits.Size = new System.Drawing.Size(85, 25);
            labelSerialDataBits.TabIndex = 14;
            labelSerialDataBits.Text = "DataBits:";
            // 
            // labelSerialStopBits
            // 
            labelSerialStopBits.AutoSize = true;
            labelSerialStopBits.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSerialStopBits.Location = new System.Drawing.Point(437, 33);
            labelSerialStopBits.Name = "labelSerialStopBits";
            labelSerialStopBits.Size = new System.Drawing.Size(83, 25);
            labelSerialStopBits.TabIndex = 12;
            labelSerialStopBits.Text = "StopBits:";
            // 
            // labelSerialParity
            // 
            labelSerialParity.AutoSize = true;
            labelSerialParity.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSerialParity.Location = new System.Drawing.Point(287, 33);
            labelSerialParity.Name = "labelSerialParity";
            labelSerialParity.Size = new System.Drawing.Size(90, 25);
            labelSerialParity.TabIndex = 10;
            labelSerialParity.Text = "Paridade:";
            // 
            // textBoxSerialBoundRate
            // 
            textBoxSerialBoundRate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSerialBoundRate.Location = new System.Drawing.Point(154, 61);
            textBoxSerialBoundRate.MaxLength = 4;
            textBoxSerialBoundRate.Name = "textBoxSerialBoundRate";
            textBoxSerialBoundRate.Size = new System.Drawing.Size(100, 29);
            textBoxSerialBoundRate.TabIndex = 11;
            // 
            // labelSerialBoundRate
            // 
            labelSerialBoundRate.AutoSize = true;
            labelSerialBoundRate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSerialBoundRate.Location = new System.Drawing.Point(150, 33);
            labelSerialBoundRate.Name = "labelSerialBoundRate";
            labelSerialBoundRate.Size = new System.Drawing.Size(113, 25);
            labelSerialBoundRate.TabIndex = 8;
            labelSerialBoundRate.Text = "Bound Rate:";
            // 
            // textBoxSerialPortComName
            // 
            textBoxSerialPortComName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textBoxSerialPortComName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSerialPortComName.Location = new System.Drawing.Point(15, 61);
            textBoxSerialPortComName.MaxLength = 6;
            textBoxSerialPortComName.Name = "textBoxSerialPortComName";
            textBoxSerialPortComName.Size = new System.Drawing.Size(100, 29);
            textBoxSerialPortComName.TabIndex = 10;
            // 
            // labelSerialComPort
            // 
            labelSerialComPort.AutoSize = true;
            labelSerialComPort.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelSerialComPort.Location = new System.Drawing.Point(11, 33);
            labelSerialComPort.Name = "labelSerialComPort";
            labelSerialComPort.Size = new System.Drawing.Size(107, 25);
            labelSerialComPort.TabIndex = 6;
            labelSerialComPort.Text = "Porta COM:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(comboBoxMachineType);
            groupBox1.Controls.Add(textBoxUrlApi);
            groupBox1.Controls.Add(labelUrlApi);
            groupBox1.Controls.Add(textBoxUrlSafariApi);
            groupBox1.Controls.Add(labelUrlSafariApi);
            groupBox1.Controls.Add(textBoxQtdDiasSafari);
            groupBox1.Controls.Add(labelQtdDiasSafari);
            groupBox1.Controls.Add(comboBoxDebugMode);
            groupBox1.Controls.Add(labelDebugMode);
            groupBox1.Controls.Add(textBoxLookupTime);
            groupBox1.Controls.Add(labelLookupTime);
            groupBox1.Controls.Add(comboBoxDriverType);
            groupBox1.Controls.Add(labelDriverType);
            groupBox1.Controls.Add(textBoxMachine);
            groupBox1.Controls.Add(labelMachine);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(783, 239);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Parâmetros gerais";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label16.Location = new System.Drawing.Point(437, 19);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(158, 25);
            label16.TabIndex = 10;
            label16.Text = "Tipo de maquina:";
            // 
            // comboBoxMachineType
            // 
            comboBoxMachineType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxMachineType.FormattingEnabled = true;
            comboBoxMachineType.Items.AddRange(new object[] { "ReelLength", "ReelWeigth", "ReelWeightRinnert", "ReelWeightToledo", "ReelWeigthP08" });
            comboBoxMachineType.Location = new System.Drawing.Point(232, 48);
            comboBoxMachineType.Name = "comboBoxMachineType";
            comboBoxMachineType.Size = new System.Drawing.Size(176, 29);
            comboBoxMachineType.TabIndex = 9;
            comboBoxMachineType.SelectedIndexChanged += ComboBoxMachineType_SelectedIndexChanged;
            // 
            // textBoxUrlApi
            // 
            textBoxUrlApi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxUrlApi.Location = new System.Drawing.Point(431, 124);
            textBoxUrlApi.MaxLength = 9999;
            textBoxUrlApi.Name = "textBoxUrlApi";
            textBoxUrlApi.Size = new System.Drawing.Size(323, 29);
            textBoxUrlApi.TabIndex = 5;
            // 
            // labelUrlApi
            // 
            labelUrlApi.AutoSize = true;
            labelUrlApi.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelUrlApi.Location = new System.Drawing.Point(431, 96);
            labelUrlApi.Name = "labelUrlApi";
            labelUrlApi.Size = new System.Drawing.Size(74, 25);
            labelUrlApi.TabIndex = 8;
            labelUrlApi.Text = "Url API:";
            // 
            // textBoxUrlSafariApi
            // 
            textBoxUrlSafariApi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxUrlSafariApi.Location = new System.Drawing.Point(15, 194);
            textBoxUrlSafariApi.MaxLength = 9999;
            textBoxUrlSafariApi.Name = "textBoxUrlSafariApi";
            textBoxUrlSafariApi.Size = new System.Drawing.Size(323, 29);
            textBoxUrlSafariApi.TabIndex = 5;
            // 
            // labelUrlSafariApi
            // 
            labelUrlSafariApi.AutoSize = true;
            labelUrlSafariApi.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelUrlSafariApi.Location = new System.Drawing.Point(15, 166);
            labelUrlSafariApi.Name = "labelUrlSafariApi";
            labelUrlSafariApi.Size = new System.Drawing.Size(127, 25);
            labelUrlSafariApi.TabIndex = 6;
            labelUrlSafariApi.Text = "Url API Safari:";
            // 
            // textBoxQtdDiasSafari
            // 
            textBoxQtdDiasSafari.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxQtdDiasSafari.Location = new System.Drawing.Point(431, 194);
            textBoxQtdDiasSafari.MaxLength = 1;
            textBoxQtdDiasSafari.Name = "textBoxQtdDiasSafari";
            textBoxQtdDiasSafari.Size = new System.Drawing.Size(114, 29);
            textBoxQtdDiasSafari.TabIndex = 5;
            // 
            // labelQtdDiasSafari
            // 
            labelQtdDiasSafari.AutoSize = true;
            labelQtdDiasSafari.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelQtdDiasSafari.Location = new System.Drawing.Point(431, 166);
            labelQtdDiasSafari.Name = "labelQtdDiasSafari";
            labelQtdDiasSafari.Size = new System.Drawing.Size(293, 25);
            labelQtdDiasSafari.TabIndex = 6;
            labelQtdDiasSafari.Text = "Qtde dias p/ buscar tirada aberta:";
            // 
            // comboBoxDebugMode
            // 
            comboBoxDebugMode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxDebugMode.FormattingEnabled = true;
            comboBoxDebugMode.Items.AddRange(new object[] { "FALSE", "TRUE" });
            comboBoxDebugMode.Location = new System.Drawing.Point(232, 124);
            comboBoxDebugMode.Name = "comboBoxDebugMode";
            comboBoxDebugMode.Size = new System.Drawing.Size(176, 29);
            comboBoxDebugMode.TabIndex = 4;
            // 
            // labelDebugMode
            // 
            labelDebugMode.AutoSize = true;
            labelDebugMode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelDebugMode.Location = new System.Drawing.Point(224, 96);
            labelDebugMode.Name = "labelDebugMode";
            labelDebugMode.Size = new System.Drawing.Size(126, 25);
            labelDebugMode.TabIndex = 6;
            labelDebugMode.Text = "Debug Mode:";
            // 
            // textBoxLookupTime
            // 
            textBoxLookupTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxLookupTime.Location = new System.Drawing.Point(15, 124);
            textBoxLookupTime.MaxLength = 5;
            textBoxLookupTime.Name = "textBoxLookupTime";
            textBoxLookupTime.Size = new System.Drawing.Size(182, 29);
            textBoxLookupTime.TabIndex = 3;
            // 
            // labelLookupTime
            // 
            labelLookupTime.AutoSize = true;
            labelLookupTime.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelLookupTime.Location = new System.Drawing.Point(9, 96);
            labelLookupTime.Name = "labelLookupTime";
            labelLookupTime.Size = new System.Drawing.Size(188, 25);
            labelLookupTime.TabIndex = 4;
            labelLookupTime.Text = "Tempo entre leituras:";
            // 
            // comboBoxDriverType
            // 
            comboBoxDriverType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxDriverType.FormattingEnabled = true;
            comboBoxDriverType.Items.AddRange(new object[] { "SERIAL", "TCP" });
            comboBoxDriverType.Location = new System.Drawing.Point(232, 48);
            comboBoxDriverType.Name = "comboBoxDriverType";
            comboBoxDriverType.Size = new System.Drawing.Size(176, 29);
            comboBoxDriverType.TabIndex = 2;
            comboBoxDriverType.SelectedIndexChanged += ComboBoxDriverType_SelectedIndexChanged;
            // 
            // labelDriverType
            // 
            labelDriverType.AutoSize = true;
            labelDriverType.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelDriverType.Location = new System.Drawing.Point(232, 20);
            labelDriverType.Name = "labelDriverType";
            labelDriverType.Size = new System.Drawing.Size(185, 25);
            labelDriverType.TabIndex = 2;
            labelDriverType.Text = "Tipo conexão/driver:";
            // 
            // textBoxMachine
            // 
            textBoxMachine.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxMachine.Location = new System.Drawing.Point(15, 48);
            textBoxMachine.MaxLength = 3;
            textBoxMachine.Name = "textBoxMachine";
            textBoxMachine.Size = new System.Drawing.Size(100, 29);
            textBoxMachine.TabIndex = 1;
            // 
            // labelMachine
            // 
            labelMachine.AutoSize = true;
            labelMachine.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelMachine.Location = new System.Drawing.Point(15, 20);
            labelMachine.Name = "labelMachine";
            labelMachine.Size = new System.Drawing.Size(91, 25);
            labelMachine.TabIndex = 0;
            labelMachine.Text = "Máquina:";
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(807, 785);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxSerialConfig);
            Controls.Add(groupBoxTcpConfig);
            Controls.Add(buttonFechar);
            Controls.Add(buttonSalvar);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigForm";
            Text = "ConfigForm";
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewReadParameters).EndInit();
            groupBoxTcpConfig.ResumeLayout(false);
            groupBoxTcpConfig.PerformLayout();
            groupBoxSerialConfig.ResumeLayout(false);
            groupBoxSerialConfig.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button buttonSalvar;
        private System.Windows.Forms.Button buttonFechar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewReadParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnDataType;
        private System.Windows.Forms.GroupBox groupBoxTcpConfig;
        private System.Windows.Forms.TextBox textBoxTCPWriteTimeout;
        private System.Windows.Forms.TextBox textBoxTCPPort;
        private System.Windows.Forms.Label labelTCPWriteTimeout;
        private System.Windows.Forms.Label labelTCPPort;
        private System.Windows.Forms.TextBox textBoxTCPReadTimeout;
        private System.Windows.Forms.Label labelTCPReadTimeout;
        private System.Windows.Forms.TextBox textBoxTCPAddress;
        private System.Windows.Forms.Label labelTCPAdress;
        private System.Windows.Forms.GroupBox groupBoxSerialConfig;
        private System.Windows.Forms.ComboBox comboBoxSerialStopBits;
        private System.Windows.Forms.ComboBox comboBoxSerialParity;
        private System.Windows.Forms.TextBox textBoxSerialWriteTimeout;
        private System.Windows.Forms.Label labelSerialWriteTimeout;
        private System.Windows.Forms.TextBox textBoxSerialReadTimeout;
        private System.Windows.Forms.Label labelSerialReadtimeout;
        private System.Windows.Forms.TextBox textBoxSerialDataBits;
        private System.Windows.Forms.Label labelSerialDataBits;
        private System.Windows.Forms.Label labelSerialStopBits;
        private System.Windows.Forms.Label labelSerialParity;
        private System.Windows.Forms.TextBox textBoxSerialBoundRate;
        private System.Windows.Forms.Label labelSerialBoundRate;
        private System.Windows.Forms.TextBox textBoxSerialPortComName;
        private System.Windows.Forms.Label labelSerialComPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxMachineType;
        private System.Windows.Forms.TextBox textBoxUrlApi;
        private System.Windows.Forms.Label labelUrlApi;        
        private System.Windows.Forms.TextBox textBoxUrlSafariApi;
        private System.Windows.Forms.Label labelUrlSafariApi;
        private System.Windows.Forms.TextBox textBoxQtdDiasSafari;
        private System.Windows.Forms.Label labelQtdDiasSafari;
        private System.Windows.Forms.ComboBox comboBoxDebugMode;
        private System.Windows.Forms.Label labelDebugMode;
        private System.Windows.Forms.TextBox textBoxLookupTime;
        private System.Windows.Forms.Label labelLookupTime;
        private System.Windows.Forms.ComboBox comboBoxDriverType;
        private System.Windows.Forms.Label labelDriverType;
        private System.Windows.Forms.TextBox textBoxMachine;
        private System.Windows.Forms.Label labelMachine;
    }
}


namespace Klabin.Rml.Client
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.acoesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarLeituraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desbloquearSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lançamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.históricoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelLastCutNumber = new System.Windows.Forms.Label();
            this.labelUltimaTirada = new System.Windows.Forms.Label();
            this.labelLastRollNumber = new System.Windows.Forms.Label();
            this.labelUltimoRolo = new System.Windows.Forms.Label();
            this.labelLastSyncDate = new System.Windows.Forms.Label();
            this.labelUltimoEnvio = new System.Windows.Forms.Label();
            this.groupBoxDebugMode = new System.Windows.Forms.GroupBox();
            this.labelSyncState = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonShowDebugParametersMode = new System.Windows.Forms.Button();
            this.labelStatusThread = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDebugLog = new System.Windows.Forms.TextBox();
            this.labelLastRead = new System.Windows.Forms.Label();
            this.labelUltimaLeitura = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerUpdateDebugMessage = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxDebugMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acoesToolStripMenuItem,
            this.lançamentoToolStripMenuItem,
            this.históricoToolStripMenuItem,
            this.configuraçõesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(804, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // acoesToolStripMenuItem
            // 
            this.acoesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarLeituraToolStripMenuItem,
            this.desbloquearSistemaToolStripMenuItem,
            this.fecharToolStripMenuItem});
            this.acoesToolStripMenuItem.Name = "acoesToolStripMenuItem";
            this.acoesToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.acoesToolStripMenuItem.Text = "Ações";
            // 
            // iniciarLeituraToolStripMenuItem
            // 
            this.iniciarLeituraToolStripMenuItem.BackColor = System.Drawing.Color.LightGreen;
            this.iniciarLeituraToolStripMenuItem.Name = "iniciarLeituraToolStripMenuItem";
            this.iniciarLeituraToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.iniciarLeituraToolStripMenuItem.Text = "Iniciar leitura";
            this.iniciarLeituraToolStripMenuItem.Click += new System.EventHandler(this.IniciarLeituraToolStripMenuItem_Click);
            // 
            // desbloquearSistemaToolStripMenuItem
            // 
            this.desbloquearSistemaToolStripMenuItem.Name = "desbloquearSistemaToolStripMenuItem";
            this.desbloquearSistemaToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.desbloquearSistemaToolStripMenuItem.Text = "Desbloquear sistema";
            this.desbloquearSistemaToolStripMenuItem.Click += new System.EventHandler(this.DesbloquearSistemaToolStripMenuItem_Click);
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.fecharToolStripMenuItem.Text = "Fechar";
            this.fecharToolStripMenuItem.Click += new System.EventHandler(this.FecharToolStripMenuItem_Click);
            // 
            // lançamentoToolStripMenuItem
            // 
            this.lançamentoToolStripMenuItem.Name = "lançamentoToolStripMenuItem";
            this.lançamentoToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.lançamentoToolStripMenuItem.Text = "Lançamento";
            this.lançamentoToolStripMenuItem.Visible = false;
            this.lançamentoToolStripMenuItem.Click += new System.EventHandler(this.LancamentoToolStripMenuItem_Click);
            // 
            // históricoToolStripMenuItem
            // 
            this.históricoToolStripMenuItem.Name = "históricoToolStripMenuItem";
            this.históricoToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.históricoToolStripMenuItem.Text = "Histórico";
            this.históricoToolStripMenuItem.Click += new System.EventHandler(this.HistoricoToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            this.configuraçõesToolStripMenuItem.Click += new System.EventHandler(this.ConfiguracoesToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelLastCutNumber);
            this.panel1.Controls.Add(this.labelUltimaTirada);
            this.panel1.Controls.Add(this.labelLastRollNumber);
            this.panel1.Controls.Add(this.labelUltimoRolo);
            this.panel1.Controls.Add(this.labelLastSyncDate);
            this.panel1.Controls.Add(this.labelUltimoEnvio);
            this.panel1.Controls.Add(this.groupBoxDebugMode);
            this.panel1.Controls.Add(this.labelLastRead);
            this.panel1.Controls.Add(this.labelUltimaLeitura);
            this.panel1.Controls.Add(this.labelStatus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 477);
            this.panel1.TabIndex = 5;
            // 
            // labelLastCutNumber
            // 
            this.labelLastCutNumber.AutoSize = true;
            this.labelLastCutNumber.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLastCutNumber.Location = new System.Drawing.Point(560, 123);
            this.labelLastCutNumber.Name = "labelLastCutNumber";
            this.labelLastCutNumber.Size = new System.Drawing.Size(31, 30);
            this.labelLastCutNumber.TabIndex = 10;
            this.labelLastCutNumber.Text = "--";
            // 
            // labelUltimaTirada
            // 
            this.labelUltimaTirada.AutoSize = true;
            this.labelUltimaTirada.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelUltimaTirada.Location = new System.Drawing.Point(328, 123);
            this.labelUltimaTirada.Name = "labelUltimaTirada";
            this.labelUltimaTirada.Size = new System.Drawing.Size(226, 30);
            this.labelUltimaTirada.TabIndex = 9;
            this.labelUltimaTirada.Text = "Última Tirada Enviada:";
            // 
            // labelLastRollNumber
            // 
            this.labelLastRollNumber.AutoSize = true;
            this.labelLastRollNumber.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLastRollNumber.Location = new System.Drawing.Point(231, 123);
            this.labelLastRollNumber.Name = "labelLastRollNumber";
            this.labelLastRollNumber.Size = new System.Drawing.Size(31, 30);
            this.labelLastRollNumber.TabIndex = 8;
            this.labelLastRollNumber.Text = "--";
            // 
            // labelUltimoRolo
            // 
            this.labelUltimoRolo.AutoSize = true;
            this.labelUltimoRolo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelUltimoRolo.Location = new System.Drawing.Point(12, 123);
            this.labelUltimoRolo.Name = "labelUltimoRolo";
            this.labelUltimoRolo.Size = new System.Drawing.Size(213, 30);
            this.labelUltimoRolo.TabIndex = 7;
            this.labelUltimoRolo.Text = "Último Rolo Enviado:";
            // 
            // labelLastSyncDate
            // 
            this.labelLastSyncDate.AutoSize = true;
            this.labelLastSyncDate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLastSyncDate.Location = new System.Drawing.Point(474, 72);
            this.labelLastSyncDate.Name = "labelLastSyncDate";
            this.labelLastSyncDate.Size = new System.Drawing.Size(31, 30);
            this.labelLastSyncDate.TabIndex = 6;
            this.labelLastSyncDate.Text = "--";
            // 
            // labelUltimoEnvio
            // 
            this.labelUltimoEnvio.AutoSize = true;
            this.labelUltimoEnvio.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelUltimoEnvio.Location = new System.Drawing.Point(328, 72);
            this.labelUltimoEnvio.Name = "labelUltimoEnvio";
            this.labelUltimoEnvio.Size = new System.Drawing.Size(140, 30);
            this.labelUltimoEnvio.TabIndex = 5;
            this.labelUltimoEnvio.Text = "Último Envio:";
            // 
            // groupBoxDebugMode
            // 
            this.groupBoxDebugMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDebugMode.Controls.Add(this.labelSyncState);
            this.groupBoxDebugMode.Controls.Add(this.label5);
            this.groupBoxDebugMode.Controls.Add(this.buttonShowDebugParametersMode);
            this.groupBoxDebugMode.Controls.Add(this.labelStatusThread);
            this.groupBoxDebugMode.Controls.Add(this.label2);
            this.groupBoxDebugMode.Controls.Add(this.textBoxDebugLog);
            this.groupBoxDebugMode.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxDebugMode.Location = new System.Drawing.Point(12, 169);
            this.groupBoxDebugMode.Name = "groupBoxDebugMode";
            this.groupBoxDebugMode.Size = new System.Drawing.Size(780, 296);
            this.groupBoxDebugMode.TabIndex = 4;
            this.groupBoxDebugMode.TabStop = false;
            this.groupBoxDebugMode.Text = "Debug Panel";
            // 
            // labelSyncState
            // 
            this.labelSyncState.AutoSize = true;
            this.labelSyncState.BackColor = System.Drawing.Color.White;
            this.labelSyncState.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSyncState.ForeColor = System.Drawing.Color.Black;
            this.labelSyncState.Location = new System.Drawing.Point(441, 26);
            this.labelSyncState.Name = "labelSyncState";
            this.labelSyncState.Size = new System.Drawing.Size(67, 20);
            this.labelSyncState.TabIndex = 5;
            this.labelSyncState.Text = "PARADO";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Thread de sincronismo:";
            // 
            // buttonShowDebugParametersMode
            // 
            this.buttonShowDebugParametersMode.Location = new System.Drawing.Point(606, 21);
            this.buttonShowDebugParametersMode.Name = "buttonShowDebugParametersMode";
            this.buttonShowDebugParametersMode.Size = new System.Drawing.Size(168, 23);
            this.buttonShowDebugParametersMode.TabIndex = 3;
            this.buttonShowDebugParametersMode.Text = "Parâmetros de leitura";
            this.buttonShowDebugParametersMode.UseVisualStyleBackColor = true;
            this.buttonShowDebugParametersMode.Click += new System.EventHandler(this.ButtonShowDebugParametersMode_Click);
            // 
            // labelStatusThread
            // 
            this.labelStatusThread.AutoSize = true;
            this.labelStatusThread.BackColor = System.Drawing.Color.White;
            this.labelStatusThread.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelStatusThread.ForeColor = System.Drawing.Color.Black;
            this.labelStatusThread.Location = new System.Drawing.Point(146, 26);
            this.labelStatusThread.Name = "labelStatusThread";
            this.labelStatusThread.Size = new System.Drawing.Size(67, 20);
            this.labelStatusThread.TabIndex = 2;
            this.labelStatusThread.Text = "PARADO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thread de leitura:";
            // 
            // textBoxDebugLog
            // 
            this.textBoxDebugLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDebugLog.BackColor = System.Drawing.Color.DimGray;
            this.textBoxDebugLog.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDebugLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.textBoxDebugLog.Location = new System.Drawing.Point(6, 61);
            this.textBoxDebugLog.Multiline = true;
            this.textBoxDebugLog.Name = "textBoxDebugLog";
            this.textBoxDebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDebugLog.Size = new System.Drawing.Size(768, 229);
            this.textBoxDebugLog.TabIndex = 0;
            // 
            // labelLastRead
            // 
            this.labelLastRead.AutoSize = true;
            this.labelLastRead.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLastRead.Location = new System.Drawing.Point(169, 72);
            this.labelLastRead.Name = "labelLastRead";
            this.labelLastRead.Size = new System.Drawing.Size(31, 30);
            this.labelLastRead.TabIndex = 3;
            this.labelLastRead.Text = "--";
            // 
            // labelUltimaLeitura
            // 
            this.labelUltimaLeitura.AutoSize = true;
            this.labelUltimaLeitura.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelUltimaLeitura.Location = new System.Drawing.Point(12, 72);
            this.labelUltimaLeitura.Name = "labelUltimaLeitura";
            this.labelUltimaLeitura.Size = new System.Drawing.Size(151, 30);
            this.labelUltimaLeitura.TabIndex = 2;
            this.labelUltimaLeitura.Text = "Última Leitura:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.Tomato;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.ForeColor = System.Drawing.Color.White;
            this.labelStatus.Location = new System.Drawing.Point(203, 26);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(96, 30);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "PARADO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status do sistema:";
            // 
            // timerUpdateDebugMessage
            // 
            this.timerUpdateDebugMessage.Interval = 300;
            this.timerUpdateDebugMessage.Tick += new System.EventHandler(this.TimerUpdateDebugMessage_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(804, 501);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Klabin - SRP - Automação";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxDebugMode.ResumeLayout(false);
            this.groupBoxDebugMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acoesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lançamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem históricoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarLeituraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desbloquearSistemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLastRead;
        private System.Windows.Forms.Label labelUltimaLeitura;
        private System.Windows.Forms.GroupBox groupBoxDebugMode;
        private System.Windows.Forms.TextBox textBoxDebugLog;
        private System.Windows.Forms.Timer timerUpdateDebugMessage;
        private System.Windows.Forms.Label labelStatusThread;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonShowDebugParametersMode;
        private System.Windows.Forms.Label labelSyncState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLastSyncDate;
        private System.Windows.Forms.Label labelUltimoEnvio;
        private System.Windows.Forms.Label labelUltimoRolo;
        private System.Windows.Forms.Label labelLastRollNumber;
        private System.Windows.Forms.Label labelUltimaTirada;
        private System.Windows.Forms.Label labelLastCutNumber;
    }
}


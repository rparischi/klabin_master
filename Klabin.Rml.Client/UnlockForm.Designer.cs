
namespace Klabin.Rml.Client
{
    partial class UnlockForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnlockForm));
            this.labelAdmCode = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxAdmCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelAdmCode
            // 
            this.labelAdmCode.AutoSize = true;
            this.labelAdmCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAdmCode.Location = new System.Drawing.Point(33, 71);
            this.labelAdmCode.Name = "labelAdmCode";
            this.labelAdmCode.Size = new System.Drawing.Size(200, 21);
            this.labelAdmCode.TabIndex = 0;
            this.labelAdmCode.Text = "Código de administrador";
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.LightGreen;
            this.buttonLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonLogin.Location = new System.Drawing.Point(171, 157);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(127, 41);
            this.buttonLogin.TabIndex = 1;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxAdmCode
            // 
            this.textBoxAdmCode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAdmCode.Location = new System.Drawing.Point(33, 95);
            this.textBoxAdmCode.Name = "textBoxAdmCode";
            this.textBoxAdmCode.Size = new System.Drawing.Size(401, 32);
            this.textBoxAdmCode.TabIndex = 2;
            this.textBoxAdmCode.UseSystemPasswordChar = true;
            this.textBoxAdmCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAdmCode_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Para desbloquear o sistema, digite o código de administrador:";
            // 
            // UnlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 209);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAdmCode);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelAdmCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnlockForm";
            this.Text = "UnlockForm";
            this.Shown += new System.EventHandler(this.UnlockForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAdmCode;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxAdmCode;
        private System.Windows.Forms.Label label1;
    }
}
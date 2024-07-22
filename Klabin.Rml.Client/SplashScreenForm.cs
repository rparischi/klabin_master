using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klabin.Rml.Client
{
    public partial class SplashScreenForm : Form
    {
        private int timeToOpen = 5;
        private int timeCount = 1;
        private bool finishInit = false;
        private readonly Action _splashScreenClosed;

        public SplashScreenForm(Action splashClose)
        {
            InitializeComponent();
            _splashScreenClosed = splashClose;

            FormClosed += async (s, e) => await SplashScreenForm_FormClosed(s, e);
        }

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            var buildNumber = Program.Configuration.GetSection("buildNumber").Value;
            var buildDate = Program.Configuration.GetSection("buildDate").Value;

            labelVersion.Text = $"Versão: {buildNumber}-{buildDate}";
            timerSplash.Start();
            finishInit = true;

        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            timeCount = timeCount + 1;
            if (timeCount >= timeToOpen && finishInit)
            {
                timerSplash.Enabled = false;

                this.Close();
            }
        }

        private async Task SplashScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            await Task.Delay(3000);
            _splashScreenClosed();
        }
    }
}

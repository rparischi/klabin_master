using Klabin.Rml.ClientLogic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Klabin.Rml.Client
{
    public partial class DebugParameterForm : Form
    {
        public DebugParameterForm()
        {
            InitializeComponent();
        }

        public void Render(MachineData machineData)
        {
            textBoxRawData.Text = machineData.RmlRawData;

            var graphics = this.CreateGraphics();
            panelParamControls.Controls.Clear();
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
                panelParamControls.Controls.Add(labelParam);

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
                yAux = yAux  + yConstantSparse + ((int)sz.Height == 0? 28 : (int)sz.Height);

                panelParamControls.Controls.Add(textBoxParam);
            }

            panelParamControls.Refresh();
        }
    }
}

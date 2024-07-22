using Klabin.Rml.ClientLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace Klabin.Rml.Client.Resources
{
    public static class ResourceHelper
    {
        private static List<Tuple<ResourceManager, string>> resources;
        private static ResourceManager currentResource;


        public static void AddResource(ResourceManager resourceManager, string machineType)
        {
            if (resources == null)
            {
                resources = new List<Tuple<ResourceManager, string>>(5);
            }

            resources.Add(Tuple.Create(resourceManager, machineType));
        }

        public static void SetCurrentResource(ReaderConfig readerConfig)
        {
            if (resources == null)
            {
                return;
            }

            currentResource = resources.First(r => r.Item2.ToLower() == readerConfig.MachineType.ToString().ToLower()).Item1;
        }

        public static ResourceManager GetCurrentResource()
        {
            return currentResource;
        }

        public static void SetControlsText(Control control)
        {
            SetControlsText(control, GetCurrentResource());
        }

        public static void SetControlsText(Control control, ResourceManager resourceManager)
        {
            foreach (var childControl in control.Controls)
            {
                if (childControl is Control)
                {
                    var resourceText = resourceManager.GetString(((Control)childControl).Name);
                    if (resourceText != null)
                    {
                        ((Control)childControl).Text = resourceText;
                    }
                }

                if (childControl is DataGridView)
                {
                    SetColumnsText(childControl as DataGridView, resourceManager);
                }

                if ((childControl as Control).Controls != null)
                {
                    SetControlsText((Control)childControl, resourceManager);
                }
            }
        }

        private static void SetColumnsText(DataGridView dataGrid, ResourceManager resourceManager)
        {
            foreach (DataGridViewTextBoxColumn column in dataGrid.Columns)
            {
                var resourceText = resourceManager.GetString($"{dataGrid.Name}{column.Name}");

                if (!string.IsNullOrWhiteSpace(resourceText))
                {
                    column.HeaderText = resourceText;
                }
            }
        }
    }
}

using System;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    internal class PersonalDashboardListViewBuilder : ListViewBuilder
    {
        public override ListViewItem BuildItem(object artefact)
        {
            var dashboard = (PersonalDashboard) artefact;
            var dashboardEntity = dashboard.Entity;
            var item = new ListViewItem(new string[]
            {
                String.Empty,
                dashboardEntity.GetAttributeValue<string>("name"),
                dashboardEntity.GetAttributeValue<string>("objecttypecode")
            });
            item.Tag = artefact;
            return item;
        }

        public override void BuildColumns(ListView target)
        {
            // Clear existing
            target.Columns.Clear();

            // And rebuild
            var colSelector = new ColumnHeader()
            {
                Text = String.Empty,
                Width = 30,
            };
            var colArtefactName = new ColumnHeader()
            {
                Text = "Name",
                Width = 120,
            };
            var colEntityName = new ColumnHeader()
            {
                Text = "Entity",
                Width = 160,
            };
            
            target.Columns.AddRange(
                new System.Windows.Forms.ColumnHeader[] {
                    colSelector,
                    colArtefactName,
                    colEntityName
                });
        }
    }
}
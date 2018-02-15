using System;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    internal class PersonalDiagramListViewBuilder : ListViewBuilder
    {
        public override ListViewItem BuildItem(object artefact)
        {
            var view = (PersonalDiagram) artefact;
            var viewEntity = view.Entity;
            var item = new ListViewItem(new string[]
            {
                String.Empty,
                viewEntity.GetAttributeValue<string>("name"),
                viewEntity.GetAttributeValue<string>("primaryentitytypecode")
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
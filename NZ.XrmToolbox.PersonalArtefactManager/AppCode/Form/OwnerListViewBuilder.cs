using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    internal class OwnerListViewBuilder : ListViewBuilder
    {
        public bool HasCheckboxes { get; set; }

        public OwnerListViewBuilder(bool withCheckboxes = true)
        {
            HasCheckboxes = withCheckboxes;
        }

        public override ListViewItem BuildItem(object artefact)
        {
            var owner = (Owner)artefact;
            var cells = new List<string>();

            if (HasCheckboxes) cells.Add(String.Empty);

            cells.AddRange(new string[]
            {
                owner.Name,
                owner.LogicalName,
            });

            var item = new ListViewItem(cells.ToArray());
            item.Tag = artefact;
            return item;
        }

        public override void BuildColumns(ListView target)
        {
            // Clear existing
            target.Columns.Clear();

            // And rebuild

            var columns = new List<ColumnHeader>();

            if (HasCheckboxes)
            {
                columns.Add(new ColumnHeader()
                {
                    Text = String.Empty,
                    Width = 20,
                });
            }
            target.CheckBoxes = HasCheckboxes;

            // Primary Field
            columns.Add(new ColumnHeader()
            {
                Text = "Name",
                Width = 160,
            });

            // Primary Field
            columns.Add(new ColumnHeader()
            {
                Text = "LogicalName",
                Width = 160,
            });
                            
            target.Columns.AddRange(columns.ToArray());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    internal class UserListViewBuilder : ListViewBuilder
    {
        public bool HasCheckboxes { get; set; }

        public UserListViewBuilder(bool withCheckboxes = true)
        {
            HasCheckboxes = withCheckboxes;
        }

        public override ListViewItem BuildItem(object artefact)
        {
            var user = (User)artefact;
            var cells = new List<string>();

            if (HasCheckboxes) cells.Add(String.Empty);

            cells.AddRange(new string[]
            {
                user.Email,
                user.Fullname,
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

            // Email
            columns.Add(new ColumnHeader()
            {
                Text = "Email",
                Width = 120,
            });

            // Fullname
            columns.Add(new ColumnHeader()
            {
                Text = "Fullname",
                Width = 160,
            });
                            
            target.Columns.AddRange(columns.ToArray());
        }
    }
}
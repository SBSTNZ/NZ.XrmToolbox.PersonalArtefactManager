﻿using System;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    internal class PersonalViewListViewBuilder : ListViewBuilder
    {
        public override ListViewItem BuildItem(object artefact)
        {
            var view = (PersonalView) artefact;
            var viewEntity = view.Entity;
            var item = new ListViewItem(new string[]
            {
                String.Empty,
                viewEntity.GetAttributeValue<string>("name"),
                viewEntity.GetAttributeValue<string>("returnedtypecode"),
                viewEntity.FormattedValues["modifiedon"]
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
                Width = 160,
            };
            var colEntityName = new ColumnHeader()
            {
                Text = "Entity",
                Width = 100,
            };
            var colLastMod = new ColumnHeader()
            {
                Text = "Modified on",
                Width = 60,
            };

            target.Columns.AddRange(
                new System.Windows.Forms.ColumnHeader[] {
                    colSelector,
                    colArtefactName,
                    colEntityName,
                    colLastMod,
                });
        }
    }
}
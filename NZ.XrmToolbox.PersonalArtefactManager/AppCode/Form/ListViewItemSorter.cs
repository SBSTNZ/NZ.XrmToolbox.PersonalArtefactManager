using System;
using System.Collections;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    internal class ListViewItemSorter : IComparer
    {
        /// <summary>
        /// Column used for comparison
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Order of sorting
        /// </summary>
        public SortOrder Order { get; set; }

        public ListViewItemSorter(int colIndex)
        {
            Column = colIndex;
            Order = SortOrder.None;
        }

        public static void OnColumnClick(object sender, ColumnClickEventArgs evt)
        {
            var listView = ((ListView)sender);
            ListViewItemSorter sorter = listView.ListViewItemSorter as ListViewItemSorter;
            if (sorter == null)
            {
                listView.ListViewItemSorter =
                    new ListViewItemSorter(evt.Column) { Order = SortOrder.Ascending};
            }
            // If clicked column is already the column that is being sorted
            if (evt.Column == sorter.Column)
            {
                // Reverse the current sort direction
                sorter.Order = (sorter.Order == SortOrder.Ascending)
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = evt.Column;
                sorter.Order = SortOrder.Ascending;
            }
            listView.Sort();
        }

        public int Compare(object a, object b)
        {
            int result;
            var itemA = a as ListViewItem;
            var itemB = b as ListViewItem;
            if (itemA == null && itemB == null)
                result = 0;
            else if (itemA == null)
                result = -1;
            else if (itemB == null)
                result = 1;
            if (itemA == itemB)
                result = 0;
            //alphabetic comparison
            result = string.Compare(itemA.SubItems[Column].Text, itemB.SubItems[Column].Text);
            // if sort order is descending.
            if (Order == SortOrder.Descending)
                // Invert the value returned by Compare.
                result *= -1;
            return result;
        }
    }
}
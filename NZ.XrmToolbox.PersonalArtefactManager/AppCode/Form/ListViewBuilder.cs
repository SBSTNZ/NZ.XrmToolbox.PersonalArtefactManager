using System.Linq;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    internal abstract class ListViewBuilder
    {
        public ListViewItem[] BuildItems(object[] artefacts)
        {
            return artefacts.Select(BuildItem).ToArray<ListViewItem>();
        }

        public void BuildList(ListView target, object[] artefacts)
        {
            target.Clear();
            BuildColumns(target);
            target.Items.AddRange(BuildItems(artefacts));
        }

        abstract public void BuildColumns(ListView target);

        abstract public ListViewItem BuildItem(object artefact);
    }
}
namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Form
{
    public class ComboBoxItem
    {
        public string LabelText { get; set; }
        public object Tag { get; set; }

        public ComboBoxItem(string label, object value)
        {
            LabelText = label;
            Tag = value;
        }

        public override string ToString()
        {
            return LabelText;
        }
    }
}
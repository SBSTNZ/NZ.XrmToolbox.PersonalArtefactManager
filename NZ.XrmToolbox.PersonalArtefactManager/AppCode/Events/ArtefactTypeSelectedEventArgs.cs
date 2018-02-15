namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Events
{
    internal class ArtefactTypeSelectedEventArgs
    {
        public string EntityType { get; set; }

        internal ArtefactTypeSelectedEventArgs(string entityType)
        {
            EntityType = entityType;
        }
    }
}
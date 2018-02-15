using System;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Events
{
    internal class ArtefactListUpdatedEventArgs : EventArgs
    {
        public IPersonalArtefact[] Artefacts { get; private set; }
        
        public ArtefactListUpdatedEventArgs(IPersonalArtefact[] artefactsList)
        {
            Artefacts = artefactsList;
        }
    }
}
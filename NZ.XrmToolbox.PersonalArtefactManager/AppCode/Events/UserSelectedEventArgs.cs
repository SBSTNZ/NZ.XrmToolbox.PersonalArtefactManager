using System;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Events
{
    internal class OwnerSelectedEventArgs : EventArgs
    {
        public Owner[] OwnerSelection { get; private set; }

        internal OwnerSelectedEventArgs(Owner[] owners)
        {
            OwnerSelection = owners;
        }        
    }
}
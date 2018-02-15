using System;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Events
{
    internal class UserSelectedEventArgs : EventArgs
    {
        public User[] UserSelection { get; private set; }

        internal UserSelectedEventArgs(User[] users)
        {
            UserSelection = users;
        }        
    }
}
using System;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    public class UnknownArtefactTypeError : ArgumentOutOfRangeException
    {
        public UnknownArtefactTypeError(string typeName, string message) : base(typeName, message)
        {
            
        }
    }
}
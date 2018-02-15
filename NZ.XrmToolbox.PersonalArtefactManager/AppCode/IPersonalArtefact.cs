using Microsoft.Xrm.Sdk;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal interface IPersonalArtefact
    {
        IPersonalArtefactManager Container { get; }

        string TypeName { get; }

        Entity Entity { get; }
    }
}
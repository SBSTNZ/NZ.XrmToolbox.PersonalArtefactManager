using System;
using Microsoft.Xrm.Sdk;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class Owner
    {
        public EntityReference entityReference { get; private set; }

        public Guid Id
        {
            get => entityReference.Id;
        }

        public String Name
        {
            get => entityReference.Name ?? String.Empty;
        }
         public String LogicalName
        {
            get => entityReference.LogicalName ?? String.Empty;
        }

        public Owner(EntityReference entityRef)
        {
            entityReference = entityRef;
        }

    }
}
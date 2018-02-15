using System;
using Microsoft.Xrm.Sdk;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class User
    {
        public Entity Entity { get; private set; }

        public Guid Id
        {
            get => Entity.Id;
        }

        public String Email
        {
            get => Entity.GetAttributeValue<string>("internalemailaddress") ?? String.Empty;
        }
         public String Fullname
        {
            get => Entity.GetAttributeValue<string>("fullname") ?? String.Empty;
        }

        public User(Entity entity)
        {
            Entity = entity;
        }

    }
}
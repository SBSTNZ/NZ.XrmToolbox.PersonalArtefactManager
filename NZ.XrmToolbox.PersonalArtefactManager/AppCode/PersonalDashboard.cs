using System;
using Microsoft.Xrm.Sdk;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class PersonalDashboard : IPersonalArtefact
    {
        public IPersonalArtefactManager Container { get; private set; }

        public string TypeName => PersonalArtefactType.UserForm;

        public Entity Entity { get; private set; }

        public string Name
        {
            get => Entity.GetAttributeValue<string>("name");
        }

        public PersonalDashboard(IPersonalArtefactManager parentContainer, Entity entity)
        {
            if (parentContainer == null)
                throw new ArgumentNullException("parentContainer", "Artefact must be instantiated with reference to its original manager instance");

            Container = parentContainer;
            Entity = entity;
        }
    }
}
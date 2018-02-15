using System;
using Microsoft.Xrm.Sdk;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal class PersonalView : IPersonalArtefact
    {
        public IPersonalArtefactManager Container { get; private set; }

        public string TypeName => PersonalArtefactType.UserQuery;

        public Entity Entity { get; private set; }

        public PersonalView(IPersonalArtefactManager parentContainer, Entity entity)
        {
            if (parentContainer == null)
                throw new ArgumentNullException("parentContainer", "Artefact must be instantiated with reference to its original manager instance");

            Container = parentContainer;
            Entity = entity;
        }
    }
}
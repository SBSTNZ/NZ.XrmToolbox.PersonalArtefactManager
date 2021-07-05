namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal interface IPersonalArtefactManager
    {

        void Duplicate(IPersonalArtefact artefact, Owner owner);

        void Assign(IPersonalArtefact artefact, Owner newOwner);

        void Delete(IPersonalArtefact artefact);

        void QueryByOwner(Owner owner);
    }

    
}
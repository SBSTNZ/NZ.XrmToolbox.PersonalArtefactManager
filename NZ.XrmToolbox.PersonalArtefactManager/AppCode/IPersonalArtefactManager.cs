namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode
{
    internal interface IPersonalArtefactManager
    {

        void Duplicate(IPersonalArtefact artefact, User owner);

        void Assign(IPersonalArtefact artefact, User newOwner);

        void Delete(IPersonalArtefact artefact);

        void QueryByUser(User user);
    }

    
}
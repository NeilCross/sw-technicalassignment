namespace SW.TechnicalAssignment.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IPersonModelLoader
    {
        IList<PersonModel> GetPeople();
    }
}
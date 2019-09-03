namespace SW.TechnicalAssignment.Interfaces
{
    using System.Collections.Generic;

    using Models;

    public interface IUserDataSource
    {
        IList<UserModel> GetUsers();
    }
}
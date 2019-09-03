namespace SW.TechnicalAssignment.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;
    using Models;

    public class UserRepository
    {
        private readonly IUserDataSource dataSource;

        public UserRepository(IUserDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        /// <summary>
        /// Returns the user with a specified Id.
        /// </summary>
        /// <param name="id">The Id of the user.</param>
        public UserModel GetById(int id)
        {
            return this.dataSource.GetUsers().FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Returns the users who are the specified age.
        /// </summary>
        /// <param name="age">The age of users to return.</param>
        public IList<UserModel> GetUsersByAge(int age)
        {
            return this.dataSource.GetUsers().Where(u => u.Age == age).ToList();
        }

        /// <summary>
        /// Returns the number of genders per age, displayed from youngest to oldest.
        /// </summary>
        public IEnumerable<AgeSummary> GenerateAgeSummaries()
        {
            return 
                this.dataSource.GetUsers()
                    .GroupBy(u => u.Age)
                    .OrderBy(group => group.Key)
                    .Select(group => 
                        new AgeSummary(
                            group.Key, 
                            group
                                .GroupBy(p => p.Gender)
                                .ToDictionary(p => p.Key, p => p.Count())
                        )
                    )
                    .ToList();
        }
    }
}
namespace SW.TechnicalAssignment
{
    using System.Collections.Generic;
    using System.Linq;

    using DataAccess;
    using Interfaces;
    using Models;

    public class AnswerMachine
    {
        private readonly IList<PersonModel> model;

        public AnswerMachine(IPersonModelLoader loader)
        {
            this.model = loader.GetPeople();
        }

        /// <summary>
        /// Returns the name of the user with a specified Id.
        /// </summary>
        /// <param name="id">The Id of the user.</param>
        public string GetUserFullNameById(int id)
        {
            return model.FirstOrDefault(p => p.Id == id)?.FullName;
        }

        /// <summary>
        /// Retrns the names of users who are the specified age.
        /// </summary>
        /// <param name="age">The age of users to return.</param>
        public string GetNamesByAge(int age)
        {
            var users = model.Where(p => p.Age == age).Select(p => p.First);
            return string.Join(", ", users);
        }

        /// <summary>
        /// Returns the number of genders per Age, displayed from youngest to oldest.
        /// </summary>
        public IEnumerable<string> GenerateAgeSummaries()
        {
            var ageGroups =
                model
                    .GroupBy(p => p.Age)
                    .Select(g =>
                    new
                    {
                        Age = g.Key,
                        Genders = g.GroupBy(p => p.Gender)
                            .ToDictionary(p => p.Key, p => p.Count())
                    })
                    .OrderBy(g => g.Age);

            return ageGroups
                .Select(group =>
                    $"Age: {group.Age} " +
                    string.Join(
                        " ",
                        group.Genders.Select(g => $"{g.Key}: {g.Value}")
                    )
                );
        }
    }
}
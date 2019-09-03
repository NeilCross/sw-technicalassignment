namespace SW.TechnicalAssignment.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class AgeSummary
    {
        public int Age { get; }

        public Dictionary<Gender, int> GenderCounts { get; }

        public AgeSummary(int age, Dictionary<Gender, int> genderCounts)
        {
            this.Age = age;
            this.GenderCounts = genderCounts;
        }

        public override string ToString()
        {
            return $"Age: {this.Age} " +
                string.Join(" ", this.GenderCounts.Select(g => $"{g.Key}: {g.Value}"));
        }
    }
}
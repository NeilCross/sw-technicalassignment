using Newtonsoft.Json;

namespace SW.TechnicalAssignment.Models
{
    using DataAccess;

    public class UserModel
    {
        public int Id { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public int Age { get; set; }

        [JsonConverter(typeof(EnumerationDefaultConverter<Gender>), Gender.Other)]
        public Gender Gender { get; set; }

        public string FullName
        {
            get
            {
                return $"{this.First} {this.Last}";
            }
        }
    }
}
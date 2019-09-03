namespace SW.TechnicalAssignment.DataAccess
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    using Interfaces;
    using Models;

    public class JsonUserDataSource : IUserDataSource
    {
        private readonly IList<UserModel> model;

        public JsonUserDataSource(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            this.model = JsonConvert.DeserializeObject<IList<UserModel>>(jsonData);
        }

        public IList<UserModel> GetUsers()
        {
            return this.model;
        }
    }
}

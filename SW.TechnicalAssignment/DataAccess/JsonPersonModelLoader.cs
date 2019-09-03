namespace SW.TechnicalAssignment.DataAccess
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    using Interfaces;
    using Models;

    public class JsonPersonModelLoader : IPersonModelLoader
    {
        private IStringDataSource datasource;

        public JsonPersonModelLoader(IStringDataSource datasource)
        {
            this.datasource = datasource;
        }

        public IList<PersonModel> GetPeople()
        {
            var jsonData = this.datasource.GetString();
            return JsonConvert.DeserializeObject<IList<PersonModel>>(jsonData);
        }
    }
}

namespace SW.TechnicalAssignment.DataAccess
{
    using System.IO;

    using Interfaces;

    public class JsonFileDataSource : IStringDataSource
    {
        private readonly string filePath;

        public JsonFileDataSource(string filePath)
        {
            this.filePath = filePath;
        }

        public string GetString()
        {
            return File.ReadAllText(filePath);
        }
    }
}
namespace SW.TechnicalAssignment
{
    using DataAccess;

    public class Program
    {
        static void Main(string[] args)
        {
            // setup dependencies
            var logger = new ConsoleLogger();
            var datasource = new JsonFileDataSource("example_data.json");
            var loader = new JsonPersonModelLoader(datasource);

            var answerMachine = new AnswerMachine(loader);

            // perform tasks

            // The users full name for id=42
            var userId = 42;
            var result1 = answerMachine.GetUserFullNameById(userId);
            if (result1 != null)
            {
                logger.Log(result1);
            }
            else
            {
                logger.Log($"No user for id: {userId}");
            }

            //	All the users first names (comma separated) who are 23
            var result2 = answerMachine.GetNamesByAge(23);
            logger.Log(result2);

            // Display the number of genders per Age, displayed from youngest to oldest.
            var result3 = answerMachine.GenerateAgeSummaries();
            foreach (var result in result3)
            {
                logger.Log(result);
            }
        }
    }
}
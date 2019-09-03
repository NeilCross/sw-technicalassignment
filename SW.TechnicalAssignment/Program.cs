namespace SW.TechnicalAssignment
{
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    using DataAccess;
    using Interfaces;

    public class Program
    {
        static void Main(string[] args)
        {
            // setup dependencies
            var serviceProvider = new ServiceCollection()
                .AddScoped<ILogger>(_ => new ConsoleLogger())
                .AddScoped<IUserDataSource>(_ => new JsonUserDataSource("example_data.json"))
                .AddScoped<UserRepository>()
                .BuildServiceProvider();

            // retrieve dependencies.
            var logger = serviceProvider.GetRequiredService<ILogger>();
            var userRepository = serviceProvider.GetRequiredService<UserRepository>();

            // perform tasks

            // Output the users full name for id=42
            var userId = 42;
            var result1 = userRepository.GetById(userId);
            if (result1 != null)
            {
                logger.Log(result1.FullName);
            }
            else
            {
                logger.Log($"No user for id: {userId}");
            }

            // Output all the users first names (comma separated) who are 23
            var result2 = userRepository.GetUsersByAge(23);
            logger.Log(string.Join(", ", result2.Select(u=>u.First)));

            // Display the number of genders per Age, displayed from youngest to oldest.
            var result3 = userRepository.GenerateAgeSummaries();
            foreach (var result in result3)
            {
                logger.Log(result);
            }
        }
    }
}
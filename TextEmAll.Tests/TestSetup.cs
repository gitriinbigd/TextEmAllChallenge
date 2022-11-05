namespace TextEmAll.Tests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using TextEmAll.DataAccess;

    public class TestSetup
    {
        public TestSetup()
        {
            //Find our root path 
            string testPath = Directory.GetCurrentDirectory();
            string rootPath = testPath.Substring(0, testPath.IndexOf("TextEmAll.Tests"));

            // create our service collection to emulate how it would work in the app 
            IServiceCollection serviceCollection = new ServiceCollection();

            // let's build our configuration as it's found in our original config file
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(rootPath)
                .AddJsonFile(
                     path: "TextEmAll\\appsettings.json",
                     optional: false,
                     reloadOnChange: true)
               .Build();

            // add that to our service collection 
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddDbContext<Challenge1DbContext>();
            serviceCollection.AddScoped<IChallenge1Service, Challenge1Service>();

            // assign our service provider 
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}

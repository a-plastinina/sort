using Microsoft.Extensions.Configuration;
using Sort.App;
using Sort.Infrastructure;

const int LENGTH = 50;
const int MAX_VALUE = 200;

RegisterDependency();

IConfiguration config = new ConfigurationBuilder() 
    .AddJsonFile("appsettings.json")
    .Build();

var sourceArrayPath = config["sourceArray"] ?? "";
var resFolder = config["resFolder"] ?? ".\\";

using (var file = IoC.Resolve<FileHelper>("FileHelper", sourceArrayPath))
{
    if (!File.Exists(sourceArrayPath))
    {
        file.WriteArray("", ArrayHelper.CreateRandom(LENGTH, MAX_VALUE));
    }
}

IoC.Resolve<Application>("Application", "Sort.Inserted", sourceArrayPath, resFolder + "result1.txt")
    .Run();

IoC.Resolve<Application>("Application", "Sort.Merged", sourceArrayPath, resFolder + "result2.txt")
    .Run();

IoC.Resolve<Application>("Application", "Sort.Selected", sourceArrayPath, resFolder + "result3.txt")
    .Run();

void RegisterDependency()
{
    IoC.Resolve<ICommand>("IoC.Register", "FileHelper"
            , (object[] args) => new FileHelper(args[0].ToString()))
        .Execute();
    
    IoC.Resolve<ICommand>("IoC.Register", "Sort.Inserted"
            , (object[] args) => new SortInsertedCommand((int[])args[0]))
        .Execute();
    IoC.Resolve<ICommand>("IoC.Register", "Sort.Selected"
            , (object[] args) => new SortSelectedCommand((int[])args[0]))
        .Execute();
    IoC.Resolve<ICommand>("IoC.Register", "Sort.Merged"
            , (object[] args) => new SortMergedCommand((int[])args[0]))
        .Execute();

    IoC.Resolve<ICommand>("IoC.Register", "Factory.Sort.Inserted"
            , (object[] args) => new SortInserted())
        .Execute();
    IoC.Resolve<ICommand>("IoC.Register", "Factory.Sort.Merged"
            , (object[] args) => new SortMerged())
        .Execute();
    IoC.Resolve<ICommand>("IoC.Register", "Factory.Sort.Selected"
            , (object[] args) => new SortSelected())
        .Execute();

    IoC.Resolve<ICommand>(
            "IoC.Register"
            , "Application"
            , (object[] args) => new Application(args[0].ToString(), args[1].ToString(), args[2].ToString()))
        .Execute();
}
    



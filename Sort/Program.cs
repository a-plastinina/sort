using Microsoft.Extensions.Configuration;

const int LENGTH = 50;
const int MAX_VALUE = 100;

IConfiguration config = new ConfigurationBuilder() 
    .AddJsonFile("appsettings.json")
    .Build();

var sourceArrayPath = config["sourceArray"] ?? "";
var resultArrayPath = config["resultArray"] ?? "";
int[] unsortedArray = {};

using (var file = new FileHelper(sourceArrayPath))
{
    if (!File.Exists(sourceArrayPath))
    {
        unsortedArray = ArrayHelper.CreateRandom(LENGTH, MAX_VALUE);
        file.WriteArray("", unsortedArray);
    }
    else
    {
        unsortedArray = file.ReadArray();
    }
}

IoC.Resolve<ICommand>("IoC.Register", "Sort.Inserted", (object[] args) => new SortInsertedCommand((int[])args[0])).Execute();
IoC.Resolve<ICommand>("IoC.Register", "Sort.Selected", (object[] args) => new SortSelectedCommand((int[])args[0])).Execute();
IoC.Resolve<ICommand>("IoC.Register", "Sort.Merged", (object[] args) => new SortMergedCommand((int[])args[0])).Execute();

IoC.Resolve<ICommand>("Sort.Inserted", unsortedArray).Execute();
IoC.Resolve<ICommand>("Sort.Merged", unsortedArray).Execute();
IoC.Resolve<ICommand>("Sort.Selected", unsortedArray).Execute();

IoC.Resolve<ICommand>("IoC.Register", "Adapter.Sort", (object[] args) => new SortableAdapter(args[0].ToString(), args[1].ToString())).Execute();

var sortAdapter = IoC.Resolve<SortableAdapter>("Adapter.Sort", sourceArrayPath, resultArrayPath);
IoC.Resolve<ICommand>("IoC.Register", "Sort.Command", (object[] args) => new SortMergedCommand((int[])args[0])).Execute();
sortAdapter.Execute();

IoC.Resolve<ICommand>("IoC.Register", "Sort.Command", (object[] args) => new SortInsertedCommand((int[])args[0])).Execute();
sortAdapter.Execute();

IoC.Resolve<ICommand>("IoC.Register", "Sort.Command", (object[] args) => new SortSelectedCommand((int[])args[0])).Execute();
sortAdapter.Execute();




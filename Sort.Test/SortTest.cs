using Sort.App;
using Sort.Infrastructure;

namespace Sort.Test;

[TestFixture]
public class SortTest
{
    const int LENGTH = 50;
    const int MAX_VALUE = 200;
    
    string sourceArrayPath = "..\\..\\..\\res\\array.txt";
    private string resFolder = "..\\..\\..\\res\\";
    
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

    void PrepareSourceArray()
    {
        using (var file = IoC.Resolve<FileHelper>("FileHelper", sourceArrayPath))
        {
            if (!File.Exists(sourceArrayPath))
            {
                file.WriteArray("", ArrayHelper.CreateRandom(LENGTH, MAX_VALUE));
            }
        }
    }
    
    [SetUp]
    public void Setup()
    {
        RegisterDependency();
        PrepareSourceArray();
    }
    
    [Test]
    public void TestInserted()
    {
        IoC.Resolve<Application>("Application", "Sort.Inserted", sourceArrayPath, resFolder + "result1.txt")
            .Run();
    }
    
    [Test]
    public void TestMerged()
    {
        IoC.Resolve<Application>("Application", "Sort.Merged", sourceArrayPath, resFolder + "result2.txt")
            .Run();
    }
    
    [Test]
    public void TestSelected()
    {
        IoC.Resolve<Application>("Application", "Sort.Selected", sourceArrayPath, resFolder + "result3.txt")
            .Run();
    }
}
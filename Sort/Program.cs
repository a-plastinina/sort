using Microsoft.Extensions.Configuration;
using Sort.App;
using Sort.App.Interface;
using Sort.App.Matrix;
using Sort.Command;
using Sort.Infrastructure;

internal class Program
{
    static IConfiguration config = new ConfigurationBuilder() 
        .AddJsonFile("appsettings.json")
        .Build();
    
    public static void Main(string[] args)
    {
        RegisterDependency();
        
        var resFolder = config["resFolder"] ?? ".\\";

        var fileReader = IoC.Resolve<MatrixReader>("Matrix.Reader", resFolder + "F0.txt");
        var fileWriter = IoC.Resolve<MatrixWriter>("Matrix.Writer", resFolder + "F1.txt");
        var app = IoC.Resolve<IAggregatable>("Matrix.Application", fileReader, fileWriter);
        app.Execute();
        
        
        var fileWriter2 = IoC.Resolve<MatrixWriter>("Matrix.Writer", resFolder + "F2.txt");
        var fileReader2 = IoC.Resolve<MatrixReader>("Matrix.Reader", resFolder + "F2.txt");
        var app1 = IoC.Resolve<IAggregatable>("Matrix.Application", fileReader2, fileWriter);
        var randomAdaptor = IoC.Resolve<IAggregatable>("Matrix.Random.Application", app1, fileWriter2);
        randomAdaptor.Execute();
    }

    static void RegisterDependency()
    {
        IoC.Resolve<ICommand>("IoC.Register",
            "Matrix.Random.Application", (object[] args) =>
                new MatrixRandomAdaptor((IAggregatable)args[0], (IWriter<int[,]>)args[1]))
            .Execute();
        
        IoC.Resolve<ICommand>("IoC.Register",
                "Matrixes",
                (object[] _) => new Matrixes())
            .Execute();
        
        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Application",
                (object[] args) => new MatrixApplication(
                    IoC.Resolve<IMatrixes>("Matrixes")
                    , (IReader<int[,]>)args[0]
                    , (IWriter<int[,]>)args[1]))
            .Execute();
        
        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Aggregate.Command",
                (object[] args) => new AggregateCommand((IMatrixes)args[0]))
            .Execute();
        
        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Reader",
                (object[] args) => new MatrixReader(args[0].ToString()))
            .Execute();
        
        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Writer",
                (object[] args) => new MatrixWriter(args[0].ToString()))
            .Execute();

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
}
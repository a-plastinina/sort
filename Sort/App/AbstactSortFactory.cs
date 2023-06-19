namespace Sort.App;

public abstract class AbstactSortFactory: ICommand
{
    private readonly string _sortName;
    private readonly string _sourceFilepath;
    private readonly string _resultFilePath;

    [Obsolete]
    private static AbstactSortFactory CreateSwitch(string sortName, string sourceFilepath, string resultFilePath)
    {
        switch (sortName)
        {
            case "Sort.Inserted":
                return new SortInserted(sourceFilepath, resultFilePath);
            case "Sort.Merged":
                return new SortMerged(sourceFilepath, resultFilePath);
            case "Sort.Selected":
                return new SortSelected(sourceFilepath, resultFilePath);
            default:
                throw new ArgumentOutOfRangeException(sortName);
        }
    }
    
    public static AbstactSortFactory Create(string sortName, string sourceFilepath, string resultFilePath)
    {
        return IoC.Resolve<AbstactSortFactory>(sortName, sourceFilepath, resultFilePath);
    }
    
    protected AbstactSortFactory(string sortName, string sourceFilepath, string resultFilePath)
    {
        _sortName = sortName;
        _sourceFilepath = sourceFilepath;
        _resultFilePath = resultFilePath;
    }

    public void Execute()
    {
        var data = ReadArray();
        Sort(data);
        WriteArray(data);
    }

    private int[] ReadArray()
    {
        using var file = new FileHelper(_sourceFilepath);
        return file.ReadArray();
    }

    private void WriteArray(int[] data)
    {
        using var file = new FileHelper(_resultFilePath);
        file.WriteArray(_sortName, data);
    }

    protected abstract void Sort(int[] data);
}
public class SortInserted: AbstactSortFactory
{
    protected internal SortInserted(string sourceFilepath, string resultFilePath) 
        : base("Sort.Inserted", sourceFilepath, resultFilePath)
    { }

    protected override void Sort(int[] data)
    {
        IoC.Resolve<ICommand>("Sort.Inserted", data).Execute();
    }
}

public class SortMerged: AbstactSortFactory
{
    protected internal SortMerged(string sourceFilepath, string resultFilePath) 
        : base("Sort.Merged", sourceFilepath, resultFilePath)
    { }

    protected override void Sort(int[] data)
    {
        IoC.Resolve<ICommand>("Sort.Merged", data).Execute();
    }
}


public class SortSelected: AbstactSortFactory
{
    protected internal SortSelected(string sourceFilepath, string resultFilePath) 
        : base("Sort.Selected", sourceFilepath, resultFilePath)
    { }

    protected override void Sort(int[] data)
    {
        IoC.Resolve<ICommand>("Sort.Selected", data).Execute();
    }
}
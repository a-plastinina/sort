using Sort.App;

namespace Sort.Infrastructure;

public class Application
{
    private readonly string _sortName;
    private readonly string _sourceFilepath;
    private readonly string _resultFilePath;

    public Application(string sortName, string sourceFilepath, string resultFilePath)
    {
        _sortName = sortName;
        _sourceFilepath = sourceFilepath;
        _resultFilePath = resultFilePath;
    }
    
    public void Run()
    {
        var data = ReadArray();
        IoC.Resolve<AbstactSortFactory>($"Factory.{_sortName}", _sortName)
            .CreateCommand(data)
            .Execute();
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
}
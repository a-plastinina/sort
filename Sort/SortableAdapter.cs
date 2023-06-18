using System.Reflection;
using System.Runtime.Serialization;

public class SortableAdapter: ICommand
{
    private readonly string _sourceFilepath;
    private readonly string _resultFilePath;

    public SortableAdapter(string sourceFilepath, string resultFilePath)
    {
        _sourceFilepath = sourceFilepath;
        _resultFilePath = resultFilePath;
    }
    public void Execute()
    {
        var data = GetData();

        var cmd = IoC.Resolve<ICommand>("Sort.Command", data);
        cmd.Execute();
        
        SetData(cmd.GetType().ToString(), data);
    }

    private int[] GetData()
    {
        using var file = new FileHelper(_sourceFilepath);
        return file.ReadArray();
    }

    private void SetData(string sortName, int[] data)
    {
        using var file = new FileHelper(_resultFilePath);
        file.WriteArray(sortName, data);
    }
}

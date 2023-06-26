using Sort.App.Interface;

namespace Sort.App.Matrix;

public class MatrixWriter : IWriter<int[,]>
{
    private readonly string _outputFilePath;

    public MatrixWriter(string outputFilePath)
    {
        _outputFilePath = outputFilePath;
    }
    
    public void Write(int[,] data)
    {
        using var file = new StreamWriter(_outputFilePath, new FileStreamOptions() { Mode = FileMode.Append, Access = FileAccess.Write });
        for (int i = 0; i < data.GetLength(0); i++)
        {
            string dimension = "";
            for (int j = 0; j < data.GetLength(1); j++)
            {
                dimension += data[i, j] + ",";
            }
            file.WriteLine(dimension.Trim(','));
        }
        file.WriteLine("***");
        file.Close();
    }
}
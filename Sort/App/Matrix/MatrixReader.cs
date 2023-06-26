using Sort.App.Interface;

namespace Sort.App.Matrix;

public class MatrixReader: IReader<int[,]>
{
    private readonly string _inputFilePath;
    private StreamReader _reader;
    private const string EndOfMatrix = "***";

    public MatrixReader(string inputFilePath)
    {
        _inputFilePath = inputFilePath;
    }

    public int[,] Read()
    {
        if (_reader is null)
            _reader = new StreamReader(_inputFilePath);
        
        string input = "";
        int[,]? result = null;
        int[] dimension;
        int index = 0;
        
        while (_reader.Peek() >= 0)
        {
            input = _reader.ReadLine();
            if (input == EndOfMatrix) break;
            
            dimension = string.IsNullOrWhiteSpace(input)
                ? Array.Empty<int>()
                : input.Split(',').Select(s => int.Parse(s)).ToArray();
            if (result is null)
            {
                int length = dimension.Length;
                result = new int[length, length];
            }
            for(int i=0; i < result.GetLength(1); i++)
            {
                result[index, i] = dimension[i];
            }

            index++;
        }

        return result;
    }

    public void Dispose()
    {
        if (_reader is not null)
            _reader.Dispose();
    }
}
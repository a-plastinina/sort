using Sort.App.Interface;

namespace Sort.App.Matrix;

public class MatrixRandomAdaptor: IAggregatable
{
    private readonly IAggregatable _matrixBaseAggragate;
    private readonly IWriter<int[,]> _sourceWriter;
    private readonly string _sourceFilePath;

    public MatrixRandomAdaptor(IAggregatable matrixBaseAggragate, IWriter<int[,]> sourceWriter)
    {
        if (matrixBaseAggragate is null)
            throw new ArgumentNullException(nameof(matrixBaseAggragate));
        if (sourceWriter is null)
            throw new ArgumentNullException(nameof(sourceWriter));
        _matrixBaseAggragate = matrixBaseAggragate;
        _sourceWriter = sourceWriter;
    }

    private int[,] Generate(int length, int maxValue)
    {
        var result = new int[length, length];
        var random = new Random();

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                result[i,j] = random.Next(maxValue);
            }
        }

        return result;
    }
    
    public void Execute()
    {
        _sourceWriter.Write(Generate(5,15));
        _sourceWriter.Write(Generate(5,15));
        _matrixBaseAggragate.Execute();
    }
}
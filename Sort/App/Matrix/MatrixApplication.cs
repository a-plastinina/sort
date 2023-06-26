using Sort.App.Interface;

namespace Sort.App.Matrix;

public class MatrixApplication : IAggregatable
{
    private readonly IReader<int[,]> _reader;
    private readonly IWriter<int[,]> _writer;
    private readonly IMatrixes _matrixes;

    public MatrixApplication(IMatrixes matrixes, IReader<int[,]> reader, IWriter<int[,]> writer)
    {
        if (reader is null)
            throw new ArgumentNullException(nameof(reader));
        
        if (writer is null)
            throw new ArgumentNullException(nameof(reader));

        if (matrixes is null)
            throw new ArgumentNullException(nameof(matrixes));

        _reader = reader;
        _writer = writer;
        _matrixes = matrixes;
    }

    public void Read()
    {
        _matrixes.Matrix1 = _reader.Read();
        _matrixes.Matrix2 = _reader.Read();
    }
    
    public void Aggregate()
    {
        IoC.Resolve<ICommand>("Matrix.Aggregate.Command", _matrixes).Execute();
    }

    public void Write()
    {
        _writer.Write(_matrixes.Result);
    }

    public void Execute()
    {
        this.Read();
        this.Aggregate();
        this.Write();
    }
}
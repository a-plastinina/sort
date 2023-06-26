using Sort.App.Interface;

namespace Sort.Command;

public class AggregateCommand: ICommand
{
    private readonly IMatrixes _obj;

    public AggregateCommand(IMatrixes obj)
    {
        _obj = obj;
    }
    public void Execute()
    {
        ValidateMatrix(_obj.Matrix1, nameof(_obj.Matrix1));
        ValidateMatrix(_obj.Matrix2, nameof(_obj.Matrix2));
        
        var x = _obj.Matrix1.GetLength(0);
        var y = _obj.Matrix2.GetLength(1);

        _obj.Result = new int[x, y];
        
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                _obj.Result[i, j] = _obj.Matrix1[i, j] + _obj.Matrix2[i, j];
            }
        }
    }

    private static void ValidateMatrix(int[,] matrix, string fieldName)
    {
        if (matrix is null || matrix.GetLength(0) == 0)
            throw new ArgumentNullException(fieldName);

        if (matrix.GetLength(0) != matrix.GetLength(1))
            throw new IndexOutOfRangeException(fieldName);
    }
}

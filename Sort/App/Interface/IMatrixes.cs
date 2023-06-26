using System.Collections.Generic;
namespace Sort.App.Interface;

public interface IMatrixes
{
    public int[,] Matrix1 { get; set; }
    public int[,] Matrix2 { get; set; }
    public int[,] Result { get; set; }
}
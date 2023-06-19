public class FileHelper : IDisposable
{
    public string FilePath { get; }

    public FileHelper(string filePath)
    {
        this.FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    public void WriteArray(string sortType, IEnumerable<int> array)
    {
        using var file = new StreamWriter(FilePath);
        if (!string.IsNullOrWhiteSpace(sortType))
        {
            file.WriteLine(sortType);
        }
        file.WriteLine(array.Aggregate("", (first, next) => $"{first},{next}").Trim(','));
        file.Close();
    }

    public int[] ReadArray()
    {
        using var file = new StreamReader(FilePath);
        var result = file.ReadLine();
        file.Close();

        return string.IsNullOrWhiteSpace(result)
            ? Array.Empty<int>()
            : result.Split(',').Select(s => int.Parse(s)).ToArray();
    }

    public void Dispose()
    {
        //
    }
}

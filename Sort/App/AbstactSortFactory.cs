namespace Sort.App;

public abstract class AbstactSortFactory
{
    public abstract ICommand CreateCommand(int[] data);
    
}
public class SortInserted: AbstactSortFactory
{
    public SortInserted()
    { }

    public override ICommand CreateCommand(int[] data)
    {
        return IoC.Resolve<ICommand>("Sort.Inserted", data);
    }
}

public class SortMerged: AbstactSortFactory
{
    public SortMerged() { }

    public override ICommand CreateCommand(int[] data)
    {
        return IoC.Resolve<ICommand>("Sort.Merged", data);
    }
}


public class SortSelected: AbstactSortFactory
{
    public SortSelected() 
    { }

    public override ICommand CreateCommand(int[] data)
    {
        return IoC.Resolve<ICommand>("Sort.Selected", data);
    }
}
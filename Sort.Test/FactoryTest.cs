using Moq;
using NUnit.Framework;
using Sort.App;

namespace Sort.Test;

[TestFixture]
public class FactoryTest
{
    [Test]
    public void TestInsertedCreateCommand()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Sort.Inserted"
                , (object[] args) => new SortInsertedCommand((int[])args[0]))
            .Execute();
        
        var obj = new SortInserted().CreateCommand(new[] { 11, 5 });
        Assert.IsInstanceOf(typeof(SortInsertedCommand), obj);
    }
    
    [Test]
    public void TestMergedCreateCommand()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Sort.Merged"
                , (object[] args) => new SortMergedCommand((int[])args[0]))
            .Execute();
        
        var obj = new SortMerged().CreateCommand(new[] { 11, 5 });
        Assert.IsInstanceOf(typeof(SortMergedCommand), obj);
    }
    
    [Test]
    public void TestSelectedCreateCommand()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Sort.Selected"
                , (object[] args) => new SortSelectedCommand((int[])args[0]))
            .Execute();
        
        var obj = new SortSelected().CreateCommand(new[] { 11, 5 });
        Assert.IsInstanceOf(typeof(SortSelectedCommand), obj);
    }
}
using Moq;
using NUnit.Framework.Constraints;

namespace Sort.Test;

public class IoCTests
{
    [SetUp]
    public void Setup()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Sort.Inserted", (object[] args) => new SortInsertedCommand((int[])args[0])).Execute();
    }

    [Test]
    public void TestEmptyKey()
    {
        Assert.Throws<ArgumentNullException>(() => { IoC.Resolve<SortInsertedCommand>("", 22); });
    }
    
    [Test]
    public void TestNullKey()
    {
        Assert.Throws<ArgumentNullException>(() => { IoC.Resolve<SortInsertedCommand>(null, 22); });
    }
    
    
    [Test]
    public void TestNotRegisterKey()
    {
        Assert.Throws<NullReferenceException>(() => { IoC.Resolve<SortInsertedCommand>("SortInsertedCommand", 22); });
    }
    
    [Test]
    public void TestRegister()
    {
        var cmd = IoC.Resolve<ICommand>("IoC.Register", "Sort.Selected", (object[] args) => new SortSelectedCommand((int[])args[0]));
        Assert.IsNotNull(cmd);
    }
    
    [Test]
    public void TestResolve()
    {
        var cmd = IoC.Resolve<ICommand>("Sort.Inserted", new[] {2});
        Assert.IsNotNull(cmd);
        Assert.IsInstanceOf<SortInsertedCommand>(cmd);
    }
}
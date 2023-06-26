using Moq;
using Sort.App.Interface;
using Sort.App.Matrix;
using Sort.Command;

namespace Sort.Test;

[TestFixture]
public class MatrixApplicationTest
{
    void RegisterDependency()
    {
        IoC.Resolve<ICommand>("IoC.Register",
                "Matrixes",
                (object[] _) => new Matrixes())
            .Execute();

        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Application",
                (object[] args) => new MatrixApplication(
                    IoC.Resolve<IMatrixes>("Matrixes")
                    , (IReader<int[,]>)args[0]
                    , (IWriter<int[,]>)args[1]))
            .Execute();

        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Aggregate.Command",
                (object[] args) => new AggregateCommand((IMatrixes)args[0]))
            .Execute();

        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Reader",
                (object[] args) => new MatrixReader(args[0].ToString()))
            .Execute();

        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Writer",
                (object[] args) => new MatrixWriter(args[0].ToString()))
            .Execute();
    }
    
    [SetUp]
    public void Setup()
    {
        RegisterDependency();
    }

    [Test]
    public void CreateTest()
    {
        var app = IoC.Resolve<IAggregatable>("Matrix.Application", Mock.Of<IReader<int[,]>>(), Mock.Of<IWriter<int[,]>>());
        Assert.NotNull(app);
    }
    
    [Test]
    public void FailedCreateTest()
    {
        Assert.Throws<ArgumentNullException>(() => { IoC.Resolve<IAggregatable>("Matrix.Application", null, Mock.Of<IWriter<int[,]>>());});
    }
    
    [Test]
    public void FailedCreate2Test()
    {
        Assert.Throws<ArgumentNullException>(() => { IoC.Resolve<IAggregatable>("Matrix.Application", Mock.Of<IReader<int[,]>>(), null);});
    }
    
    [Test]
    public void Execute_CallsWriteMethodTwiceAndExecuteMethod()
    {
        // Arrange
        var mockWriter = new Mock<IWriter<int[,]>>();
        var mockReader = new Mock<IReader<int[,]>>();
        mockReader.Setup(io => io.Read()).Returns(new int[1, 1]);
        
        var app = IoC.Resolve<IAggregatable>("Matrix.Application",mockReader.Object, mockWriter.Object);

        // Act
        app.Execute();

        // Assert
        mockWriter.Verify(w => w.Write(It.IsAny<int[,]>()), Times.Once);
        mockReader.Verify(a => a.Read(), Times.Exactly(2));
    }
}
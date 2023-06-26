using Moq;
using Sort.App.Interface;
using Sort.App.Matrix;
using Sort.Command;

namespace Sort.Test;

[TestFixture]
public class MatrixRandomTest
{
    void RegisterDependency()
    {
        IoC.Resolve<ICommand>("IoC.Register",
                "Matrix.Random.Application", (object[] args) =>
                    new MatrixRandomAdaptor((IAggregatable)args[0], (IWriter<int[,]>)args[1]))
            .Execute();

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
    public void CreateAdaptorTest()
    {
        var mockStrem = new Mock<IWriter<int[,]>>();
        var mockApp = new Mock<IAggregatable>();
        
        var randomAdaptor = IoC.Resolve<IAggregatable>("Matrix.Random.Application", mockApp.Object, mockStrem.Object);
        Assert.NotNull(randomAdaptor);
    }
    
    [Test]
    public void FailedCreateAdaptorTest()
    {
        var mockStrem = new Mock<IWriter<int[,]>>();
        Assert.Throws<ArgumentNullException>(() => { IoC.Resolve<IAggregatable>("Matrix.Random.Application", null, mockStrem.Object);});
    }
    
    [Test]
    public void FailedCreate2AdaptorTest()
    {
        var mockApp = new Mock<IAggregatable>();
        Assert.Throws<ArgumentNullException>(() => { IoC.Resolve<IAggregatable>("Matrix.Random.Application", mockApp.Object, null);});
    }
    
    [Test]
    public void Execute_CallsWriteMethodTwiceAndExecuteMethod()
    {
        // Arrange
        var mockAggregate = new Mock<IAggregatable>();
        var mockWriter = new Mock<IWriter<int[,]>>();
        var adaptor = new MatrixRandomAdaptor(mockAggregate.Object, mockWriter.Object);

        // Act
        adaptor.Execute();

        // Assert
        mockWriter.Verify(w => w.Write(It.IsAny<int[,]>()), Times.Exactly(2));
        mockAggregate.Verify(a => a.Execute(), Times.Once);
    }
}
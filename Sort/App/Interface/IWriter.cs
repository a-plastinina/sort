namespace Sort.App.Interface;

public interface IWriter<T>
{
    void Write(T data);
}
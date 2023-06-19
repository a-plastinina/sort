using System;
using System.Collections;

public class IoC
{
    private static readonly Hashtable Container = new();
    public class RegisterCommand<T> : ICommand
    {
        private readonly string _key;
        private Func<object[], T> _func;
        
        public RegisterCommand(string key, Func<object[], T> func)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new NullReferenceException(nameof(key));
            _key = key;
            _func = func;
        }

        public void Execute()
        {
            Container[_key] = _func;
        }

    }
    
    public static T Resolve<T>(string key, params object[] args)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(key);

        if (key == "IoC.Register")
        {
            object cmd = new RegisterCommand<T>(args[0].ToString(), (Func<object[], T>)args[1]);
            return (T)cmd;
        }

        if (Container[key] == null)
            throw new NullReferenceException("Неизвестная зависимость {key}");

        return ((Func<object[], T>)Container[key])(args);
    }
}

using System;

namespace ApplicationService.Services
{
    public class Base : IDisposable
    {
        public Base()
            => Console.WriteLine($"Instance of {GetType().Name} is created.");
        public void Dispose()
            => Console.WriteLine($"Instance of {GetType().Name} is disposed.");
    }
}

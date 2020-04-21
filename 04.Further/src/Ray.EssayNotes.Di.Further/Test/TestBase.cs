using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.Di.ContainerDemo.IServices;
using Ray.EssayNotes.Di.ContainerDemo.Services;

namespace Ray.EssayNotes.Di.Further.Test
{
    public abstract class TestBase : ITest
    {
        public virtual void InitContainer()
        {
            if (Program.ServiceProviderRoot != null) return;
            Program.ServiceProviderRoot = new ServiceCollection()
                .AddTransient<IMyTransientService, MyTransientService>()
                .AddSingleton<IMySingletonService, MySingletonService>()
                .AddScoped<IMyScopedService, MyScopedOtherService>()
                .AddScoped<IMyScopedService, MyScopedService>()
                .BuildServiceProvider();
        }

        public abstract void Print();

        public virtual void Run()
        {
            InitContainer();
            Print();
        }
    }
}

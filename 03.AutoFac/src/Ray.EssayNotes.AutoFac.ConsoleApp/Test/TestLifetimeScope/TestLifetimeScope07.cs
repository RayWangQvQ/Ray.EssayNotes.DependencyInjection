using System;
using Autofac;
using Autofac.Features.OwnedInstances;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Extensions;
using Ray.EssayNotes.AutoFac.Service.AppService;
using Ray.EssayNotes.AutoFac.Service.Dtos;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestLifetimeScope
{
    public class TestLifetimeScope07 : TestLifetimeScopeBase
    {
        /// <summary>
        /// 每次被拥有单例
        /// 【InstancePerOwned】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public override ContainerBuilder RegisterFunc(ContainerBuilder builder)
        {
            builder.RegisterType<TestServiceOne>();
            builder.RegisterType<TestServiceTwo>();

            builder.RegisterType<DtoToken>()
                .InstancePerOwned<DtoToken>();

            return builder;
        }

        protected override void PrintResult()
        {
            using (var scope = MyContainer.Instance.BeginLifetimeScope())
            {
                var service = scope.Resolve<TestServiceTwo>();
                service.PrintToken();
                service.PrintToken();
            }
        }
    }



    /// <summary>
    /// 模拟一个有依赖的类
    /// </summary>
    public class TestServiceOne
    {
        private readonly DtoToken _dtoToken;

        public TestServiceOne(DtoToken dtoToken)
        {
            _dtoToken = dtoToken;
        }

        public void PrintToken()
        {
            Console.WriteLine(_dtoToken.Guid);
        }
    }

    /// <summary>
    /// 模拟一个有依赖的类
    /// </summary>
    public class TestServiceTwo
    {
        private readonly Owned<DtoToken> _ownedDtoToken;
        public TestServiceTwo(Owned<DtoToken> ownedDtoToken)
        {
            _ownedDtoToken = ownedDtoToken;
        }

        public void PrintToken()
        {
            Console.WriteLine(_ownedDtoToken?.Value?.Guid);
            _ownedDtoToken?.Dispose();
        }
    }
}

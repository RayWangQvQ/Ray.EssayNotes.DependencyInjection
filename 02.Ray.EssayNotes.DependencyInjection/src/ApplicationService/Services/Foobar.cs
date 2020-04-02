using ApplicationService.IServices;

namespace ApplicationService.Services
{
    public class Foobar<T1, T2> : IFoobar<T1, T2>
    {
        public IFoo Foo { get; }
        public IBar Bar { get; }
        public Foobar(IFoo foo, IBar bar)
        {
            Foo = foo;
            Bar = bar;
        }
    }

    public class Foobar : IFoobar
    {
        private Foobar() { }
        public static readonly Foobar Instance = new Foobar();
    }
}

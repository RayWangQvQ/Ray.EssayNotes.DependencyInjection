# Core DI 进阶

## 几个核心接口的职责

### IServiceScope（域）

有2个职责：

* 释放资源
* 封装一个容器对象（IServiceProvider对象）

其继承了IDisposable接口，并且包裹了一个容器对象作为属性。

```
  /// <summary>
  /// The <see cref="M:System.IDisposable.Dispose" /> method ends the scope lifetime. Once Dispose
  /// is called, any scoped services that have been resolved from
  /// <see cref="P:Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProvider" /> will be
  /// disposed.
  /// </summary>
  public interface IServiceScope : IDisposable
  {
    /// <summary>
    /// The <see cref="T:System.IServiceProvider" /> used to resolve dependencies from the scope.
    /// </summary>
    IServiceProvider ServiceProvider { get; }
  }

```

### IServiceProvider（容器）

只有一个职责：作为容器对外提供实例对象。

其未继承任何接口，只有一个GetService()方法。

```
  /// <summary>Defines a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</summary>
  public interface IServiceProvider
  {
    /// <summary>Gets the service object of the specified type.</summary>
    /// <param name="serviceType">An object that specifies the type of service object to get.</param>
    /// <returns>A service object of type <paramref name="serviceType" />.
    /// -or-
    /// <see langword="null" /> if there is no service object of type <paramref name="serviceType" />.</returns>
    object GetService(Type serviceType);
  }

```

### IServiceScopeFactory（域工厂）

只有一个职责：生产子IServiceScope对象（子域）。

其未实现任何接口，只有一个CreateScope()方法。

```
  /// <summary>
  /// A factory for creating instances of <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScope" />, which is used to create
  /// services within a scope.
  /// </summary>
  public interface IServiceScopeFactory
  {
    /// <summary>
    /// Create an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScope" /> which
    /// contains an <see cref="T:System.IServiceProvider" /> used to resolve dependencies from a
    /// newly created scope.
    /// </summary>
    /// <returns>
    /// An <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScope" /> controlling the
    /// lifetime of the scope. Once this is disposed, any scoped services that have been resolved
    /// from the <see cref="P:Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProvider" />
    /// will also be disposed.
    /// </returns>
    IServiceScope CreateScope();
  }

```

### IServiceProviderEngine（容器引擎）

有2个职责：

* 保存一个指向根域的地址（RootScope属性）
* 本身同时作为一个容器（继承了IServiceProvider接口）


比价复杂的一个接口，内部封装了一个指向根域的引用。

~~~
  internal interface IServiceProviderEngine : IServiceProvider, IDisposable, IAsyncDisposable
  {
    IServiceScope RootScope { get; }

    void ValidateService(ServiceDescriptor descriptor);
  }

~~~


## 几个核心实现

### ServiceProvider（根容器）

主要实现容器功能（IServiceProvider）。

该对象非常简单，内部封装了一个IServiceProviderEngine（容器引擎）对象，利用代理模式，让引擎去实现具体的容器功能。

这样做的目的是为了为DI容器进行一层封装隔离。

可以看到GetService()本身什么都没有做，只是直接调用的引擎的GetService()方法：

~~~
  public sealed class ServiceProvider : IServiceProvider, IDisposable, IServiceProviderEngineCallback, IAsyncDisposable
  {
    private readonly IServiceProviderEngine _engine;

    /// <summary>Gets the service object of the specified type.</summary>
    /// <param name="serviceType">The type of the service to get.</param>
    /// <returns>The service that was produced.</returns>
    public object GetService(Type serviceType)
    {
      return this._engine.GetService(serviceType);
    }
  }

~~~

值得指出的是，ServiceProvider是一个特殊的容器，只有由ServiceCollection创建的根容器才会是该实现类型。

当我们创建子容器时，子容器的类型并不是ServiceProvider类型，而是下面会讲的引擎域（ServiceProviderEngineScope）实现类型。

### ServiceProviderEngine（容器引擎）

比价复杂，即实现了容器引擎功能（IServiceProviderEngine），也实现了容器功能（IServiceProvider），还实现了域工厂功能（IServiceScopeFactory）。

* 作为容器引擎职责

    + 引擎职责1：保存指向跟容器的地址

RootScope属性（指向根域的引用）指向的是根引擎域。

RootScope和Root本质是同一个引擎域对象（ServiceProviderEngineScope对象），只是不同的多态表现而已。

~~~
  internal abstract class ServiceProviderEngine : IServiceProviderEngine, IServiceProvider, IDisposable, IAsyncDisposable, IServiceScopeFactory
  {
    protected ServiceProviderEngine(
      IEnumerable<ServiceDescriptor> serviceDescriptors,
      IServiceProviderEngineCallback callback)
    {
      //...
      this.Root = new ServiceProviderEngineScope(this);
      //...
    }

    public ServiceProviderEngineScope Root { get; }

    public IServiceScope RootScope=>(IServiceScope) this.Root;
    {
      get
      {
        return (IServiceScope) this.Root;
      }
    }

    //...
  }

~~~

注意，引擎对象是唯一的，即如果产生了多个域，每个域内都会包裹一个引擎，但是这些所有的引擎其实都是同一个对象。

引擎对象的创建是在创建根容器（ServiceProvider）时发生的：

~~~
    public sealed class ServiceProvider : IServiceProvider, IDisposable, IServiceProviderEngineCallback, IAsyncDisposable
    {
        private readonly IServiceProviderEngine _engine;

        internal ServiceProvider(IEnumerable<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            switch (options.Mode)
            {
                case ServiceProviderMode.Default:
                    if (RuntimeFeature.IsSupported("IsDynamicCodeCompiled"))
                    {
                        _engine = new DynamicServiceProviderEngine(serviceDescriptors, callback);
                    }
                    else
                    {
                        _engine = new RuntimeServiceProviderEngine(serviceDescriptors, callback);
                    }
                    break;
                case ServiceProviderMode.Dynamic:
                    _engine = new DynamicServiceProviderEngine(serviceDescriptors, callback);
                    break;
                case ServiceProviderMode.Runtime:
                    _engine = new RuntimeServiceProviderEngine(serviceDescriptors, callback);
                    break;
                case ServiceProviderMode.ILEmit:
                    _engine = new ILEmitServiceProviderEngine(serviceDescriptors, callback);
                    break;
                case ServiceProviderMode.Expressions:
                    _engine = new ExpressionsServiceProviderEngine(serviceDescriptors, callback);
                    break;
                default:
                    throw new NotSupportedException("Mode");
            }
            //...
        }
    }

~~~

且第一次创建引擎对象时，引擎会创建包裹自己的引擎域（ServiceProviderEngineScope），该引擎域就是根域。

而以后创建子域（子引擎域）时，做的只是new一个引擎域，并把引擎的引用放到该子引擎域中。

所以根域和子域的创建流程是：

* 对根域来说，是根容器（ServiceProvider）先创建引擎对象（引擎作为跟容器的一个私有变量），在构造引擎时，引擎会主动new一个引擎域，并把自己的引用放进了这个引擎域的同时，也将自己的RootScope指向该域。
* 对子域来说，是引擎对象作为工厂职能，去创建了一个子引擎域对象，该子引擎域对象所包裹的引擎属性（Engine）指向自己。

    + 引擎职责2：本身作为一个容器

    放到下面的容器职能分析。

* 作为容器职能

引擎对象同时也实现了IServiceProvider容器职能，该职能不对外开放，只是作为容器/域的代理实现，容器都是通过引擎对象来实现获取实例的功能。

* 作为域工厂职能

引擎还做为域工厂，负责生成子域（ServiceProviderEngineScope）。

我们创建子域的CreateScope()扩展方法内部是这样实现：

~~~
    public static class ServiceProviderServiceExtensions
    {
        //
        // 摘要:
        //     Creates a new Microsoft.Extensions.DependencyInjection.IServiceScope that can
        //     be used to resolve scoped services.
        //
        // 参数:
        //   provider:
        //     The System.IServiceProvider to create the scope from.
        //
        // 返回结果:
        //     A Microsoft.Extensions.DependencyInjection.IServiceScope that can be used to
        //     resolve scoped services.
        public static IServiceScope CreateScope(this IServiceProvider provider)
        {
            return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }
        //...
    }

~~~

可以看到，其实做了2步，首先从容器中获取IServiceScopeFactory域工厂，这里获取的域工厂其实就是引擎对象；然后调用引擎对象的CreateScope方法创建子引擎域：

~~~
    internal abstract class ServiceProviderEngine : IServiceProviderEngine, IServiceProvider, IDisposable, IAsyncDisposable, IServiceScopeFactory
    {
        public IServiceScope CreateScope()
        {
            if (_disposed)
            {
                ThrowHelper.ThrowObjectDisposedException();
            }
            return new ServiceProviderEngineScope(this);
        }
    }

~~~

### ServiceProviderEngineScope（引擎域）

即实现了域职责（IServiceScope），也实现了容器职责（IServiceProvider）。

构造时必须传入引擎对象，引擎域使用自己的Engine属性记录引擎对象的地址：

~~~
  internal class ServiceProviderEngineScope : IServiceScope, IDisposable, IServiceProvider, IAsyncDisposable
  {
    public ServiceProviderEngineScope(ServiceProviderEngine engine)
    {
      this.Engine = engine;
    }
    public ServiceProviderEngine Engine { get; }
    //...
  }

~~~

* 作为域职责

如前所述，域职责包含2个功能：释放资源和封装容器。

    + 释放功能

    即在域被释放前，会调用域的Dispose()方法，该方法会去遍历域内的可释放实例池，挨个执行实例的Disposable方法实现对内存资源的释放。

~~~

  internal class ServiceProviderEngineScope : IServiceScope, IDisposable, IServiceProvider, IAsyncDisposable
  {
    private List<object> _disposables;//可释放实例池
    private bool _disposed;

    internal Dictionary<ServiceCacheKey, object> ResolvedServices { get; } = new Dictionary<ServiceCacheKey, object>();//持久化实例池

    public void Dispose()
    {
      List<object> objectList = this.BeginDispose();
      if (objectList == null)
        return;
      for (int index = objectList.Count - 1; index >= 0; --index)
      {
        IDisposable disposable = objectList[index] as IDisposable;
        if (disposable == null)
          throw new InvalidOperationException(Resources.FormatAsyncDisposableServiceDispose((object) TypeNameHelper.GetTypeDisplayName(objectList[index], true)));
        disposable.Dispose();
      }
    }

    private List<object> BeginDispose()
    {
      List<object> disposables;
      lock (this.ResolvedServices)
      {
        if (this._disposed)
          return (List<object>) null;
        this._disposed = true;
        disposables = this._disposables;
        this._disposables = (List<object>) null;
      }
      return disposables;
    }
    //...
  }

~~~

    + 封装容器功能

    封装包裹的容器对象指向自己本身。

~~~
  internal class ServiceProviderEngineScope : IServiceScope, IDisposable, IServiceProvider, IAsyncDisposable
  {
    public IServiceProvider ServiceProvider => (IServiceProvider) this;
    {
      get
      {
        return (IServiceProvider) this;
      }
    }
    //...
  }
~~~

* 作为容器职责

其GetService()方法同样使用代理模式，让其封装的引擎对象去执行具体操作。

~~~
  internal class ServiceProviderEngineScope : IServiceScope, IDisposable, IServiceProvider, IAsyncDisposable
  {
    public ServiceProviderEngineScope(ServiceProviderEngine engine)
    {
      this.Engine = engine;
    }

    public ServiceProviderEngine Engine { get; }

    public object GetService(Type serviceType)
    {
      if (this._disposed)
        ThrowHelper.ThrowObjectDisposedException();
      return this.Engine.GetService(serviceType, this);
    }
    //...
  }

~~~
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

只有一个职责：获取实例。

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

职责：


比价复杂的一个接口，内部封装了一个指向根域的引用。

~~~
  internal interface IServiceProviderEngine : IServiceProvider, IDisposable, IAsyncDisposable
  {
    IServiceScope RootScope { get; }

    void ValidateService(ServiceDescriptor descriptor);
  }

~~~


## 几个核心实现

### ServiceProvider（容器）

主要实现容器功能（IServiceProvider）。

该对象非常简单，内部封装了一个IServiceProviderEngine（容器引擎）对象，利用代理模式，让引擎去实现具体的容器功能。

目的是为了为DI容器进行一层封装隔离。

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

### ServiceProviderEngine（容器引擎）

比价复杂，即实现了容器引擎功能（IServiceProviderEngine），也实现了容器功能（IServiceProvider），还实现了域工厂功能（IServiceScopeFactory）。

* 作为容器引擎职责

RootScope（指向根域的引用）指向的是当前引擎所在的引擎域。

RootScope和Root本质是同一个ServiceProviderEngineScope对象，只是不同的多态表现而已。

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

    public IServiceScope RootScope
    {
      get
      {
        return (IServiceScope) this.Root;
      }
    }

    //...
  }

~~~

### ServiceProviderEngineScope（引擎域）

即实现了域职责（IServiceScope），也实现了容器职责（IServiceProvider）。

构造时需要传入一个引擎对象：

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
    public IServiceProvider ServiceProvider
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
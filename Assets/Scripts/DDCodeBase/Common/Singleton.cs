using System;
using System.Threading;

//泛型单例父类，带线程锁，可手动释放
public class Singleton<T> : IDisposable where T : new()
{
    private static T _instance;
    private static readonly object sync = new object();
    protected Singleton() { }
    public static T Instance
    {
        get
        {
            // 双重锁定只需要一句判断就可以了
            if (_instance == null)
            {
                // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
                // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
                // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
                lock (sync)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }
    #region Implementation IDisposeable

    public virtual void Dispose() { }

    #endregion
}

public class Singleton_Lazy<T> : IDisposable where T : new()
{
    private Singleton_Lazy() { }
    //.NET4或以上的版本支持Lazy<T>来实现延迟加载，它用最简洁的代码保证了单例的线程安全和延迟加载特性。
    private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());

    public static T Instance
    {
        get
        {
            return lazy.Value;
        }
    }

    #region Implementation IDisposeable

    public virtual void Dispose() { }

    #endregion
}

/// <summary>
/// 使用原子操作Interlocked
/// </summary>
public sealed class Singleton_Interlocked : IDisposable
{
    private static Singleton_Interlocked _instance;
    private Singleton_Interlocked() { }
    public static Singleton_Interlocked Instance
    {
        get
        {
            if (_instance == null)
            {
                Singleton_Interlocked t = new Singleton_Interlocked();
                Interlocked.CompareExchange(ref _instance, t, null);
            }

            return _instance;
        }
    }
    #region Implementation IDisposeable

    public void Dispose() { }

    #endregion
}

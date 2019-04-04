using UnityEngine;
using System.Collections;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    #region 泛型单例

    private static T instance;
    private static readonly object sync = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock(sync)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }               
            }

            return instance;
        }
    }

    #endregion

    //可重写的Awake虚方法，用于实例化对象
    protected virtual void Awake()
    {
        instance = this as T;
    }

    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        instance = null;
    }

    public void OnDestroy()
    {
        Debuger.Log("Destroy " + typeof(T).Name);
    }
}

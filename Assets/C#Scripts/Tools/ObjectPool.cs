using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    public Transform Box;
    public ObjectPool(T prefab)
    {
        this.prefab = prefab;
    }
    public T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(prefab,Box);
            return newObj;
        }
    }
    public void ReturnObject(T obj)
    {
        obj.transform.SetParent(Box);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
    public void ClearPool()
    {
        pool.Clear();
    }
}
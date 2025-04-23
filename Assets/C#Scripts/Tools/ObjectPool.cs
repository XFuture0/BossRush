using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    public ObjectPool(T prefab)
    {
        this.prefab = prefab;
    }
    public T GetObject(Transform transform)
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(prefab,transform);
            return newObj;
        }
    }
    public void ReturnObject(T obj,Transform transform)
    {
        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
    public void ClearPool()
    {
        pool.Clear();
    }
}
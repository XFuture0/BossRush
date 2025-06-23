using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItems : MonoBehaviour
{
    public ItemList.Item Thisitem;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO ClearItemEvent;
    [Header("¹ã²¥")]
    public VoidEventSO GetItemEvent;
    private void OnEnable()
    {
        MapManager.Instance.SaveItemEvent.OnEventRaised += OnSaveItem;
        ClearItemEvent.OnEventRaised += Destroy;
        GetItemEvent.RaiseEvent();
    }
    private void Update()
    {
        Thisitem.ItemPosition = transform.position;
    }
    private void OnSaveItem()
    {
        MapManager.Instance.AddItemList(Thisitem);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        GetItemEvent.RaiseEvent();
        MapManager.Instance.SaveItemEvent.OnEventRaised -= OnSaveItem;
        MapManager.Instance.DeleteItemList(Thisitem);
        ClearItemEvent.OnEventRaised -= Destroy;
    }
}

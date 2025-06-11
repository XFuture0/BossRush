using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItems : MonoBehaviour
{
    public ItemList.Item Thisitem;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO ClearItemEvent;
    private void OnEnable()
    {
        MapManager.Instance.SaveItemEvent.OnEventRaised += OnSaveItem;
        ClearItemEvent.OnEventRaised += Destroy;
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
        MapManager.Instance.SaveItemEvent.OnEventRaised -= OnSaveItem;
        MapManager.Instance.DeleteItemList(Thisitem);
        ClearItemEvent.OnEventRaised -= Destroy;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    private Button ThisButton;
    public Button DeleteDataButton;
    public int Index;
    private void Awake()
    {
        ThisButton = GetComponent<Button>();
        ThisButton.onClick.AddListener(OnLoad);
        DeleteDataButton.onClick.AddListener(OnDelete);
    }
    private void OnLoad()
    {
        DataManager.Instance.Load(Index);
        gameObject.transform.parent.parent.gameObject.SetActive(false);
    }
    private void OnDelete()
    {
        DataManager.Instance.Delete(Index);
    }
}

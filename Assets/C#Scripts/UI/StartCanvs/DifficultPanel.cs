using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultPanel : MonoBehaviour
{
    [System.Serializable]
    public class DifficultData
    {
        public int RoomCount;
        public Plot RoomPlot;
        public ChooseCardList CardList;
    }
    public int ThisIndex = 0;
    public Transform Content;
    public GameObject PointBox;
    public List<DifficultData> DifficultList = new List<DifficultData>();
    private void Start()
    {
        ChooseDifficult(ThisIndex);
    }
    private void OnEnable()
    {
        Content.GetChild(ThisIndex).gameObject.GetComponent<DifficultSlot>().OnChooseButton();
    }
    private void Update()
    {
        SetPoint();
    }
    private void SetPoint()
    {
        List<RaycastResult> result = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerEventData, result);
        foreach (RaycastResult raycastResult in result)
        {
            if (raycastResult.gameObject.name != null)
            {
                switch (raycastResult.gameObject.name)
                {
                    case "DifficultSlot":
                        PointBox.transform.SetParent(Content.GetChild(0));
                        break;
                    case "DifficultSlot1":
                        PointBox.transform.SetParent(Content.GetChild(1));
                        break;
                    case "DifficultSlot2":
                        PointBox.transform.SetParent(Content.GetChild(2));
                        break;
                    case "DifficultSlot3":
                        PointBox.transform.SetParent(Content.GetChild(3));
                        break;
                    case "DifficultSlot4":
                        PointBox.transform.SetParent(Content.GetChild(4));
                        break;
                    case "DifficultSlot5":
                        PointBox.transform.SetParent(Content.GetChild(5));
                        break;
                    case "DifficultSlot6":
                        PointBox.transform.SetParent(Content.GetChild(6));
                        break;
                }
                PointBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 0);
            }
        }
    }
    public void ClearChoose()
    {
        for(int i = 0; i < Content.childCount; i++)
        {
            Content.GetChild(i).GetComponent<Image>().color  = new Color(1, 1, 1, 1);
        }
    }
    public void ChooseDifficult(int index)
    {
        ThisIndex = index;
        SceneChangeManager.Instance.ShowRoomCount(DifficultList[ThisIndex].RoomCount);
        CardManager.Instance.SetCardList(DifficultList[ThisIndex].CardList);
        PlotManager.Instance.ThisRoomPlot = Instantiate(DifficultList[ThisIndex].RoomPlot);
    }
}

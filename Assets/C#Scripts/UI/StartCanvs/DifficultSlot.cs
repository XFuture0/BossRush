using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultSlot : MonoBehaviour
{
    [System.Serializable]
    public class DifficultData
    {
        public int Index;
        public int RoomCount;
        public Plot RoomPlot;
        public ChooseCardList CardList;
    }
    private Button LeftChange;
    private Button RightChange;
    public int ThisIndex = 0;
    public List<DifficultData> DifficultList = new List<DifficultData>();
    private void Awake()
    {
        LeftChange = transform.GetChild(0).GetComponent<Button>();
        RightChange = transform.GetChild(1).GetComponent<Button>();
        LeftChange.onClick.AddListener(OnLeftChange);
        RightChange.onClick.AddListener(OnRightChange);
    }
    private void Start()
    {
        SceneChangeManager.Instance.ShowRoomCount(DifficultList[ThisIndex].RoomCount);
        CardManager.Instance.SetCardList(DifficultList[ThisIndex].CardList);
        PlotManager.Instance.ThisRoomPlot = Instantiate(DifficultList[ThisIndex].RoomPlot);
    }
    private void OnLeftChange()
    {
        if(ThisIndex - 1 >= 0)
        {
            ThisIndex--;
            transform.GetChild(2).GetComponent<Text>().text = DifficultList[ThisIndex].Index.ToString();
            SceneChangeManager.Instance.ShowRoomCount(DifficultList[ThisIndex].RoomCount);
            CardManager.Instance.SetCardList(DifficultList[ThisIndex].CardList);
            PlotManager.Instance.ThisRoomPlot = Instantiate(DifficultList[ThisIndex].RoomPlot);
        }
    }
    private void OnRightChange()
    {
        if (ThisIndex + 1 <= DifficultList.Count - 1)
        {
            ThisIndex++;
            transform.GetChild(2).GetComponent<Text>().text = DifficultList[ThisIndex].Index.ToString();
            SceneChangeManager.Instance.ShowRoomCount(DifficultList[ThisIndex].RoomCount);
            CardManager.Instance.SetCardList(DifficultList[ThisIndex].CardList);
            PlotManager.Instance.ThisRoomPlot = Instantiate(DifficultList[ThisIndex].RoomPlot);
        }
    }
}

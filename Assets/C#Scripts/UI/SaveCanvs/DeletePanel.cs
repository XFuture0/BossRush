using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeletePanel : MonoBehaviour
{
    public int Index;
    public Text TimeText;
    public GameObject TrueBack;
    public GameObject FalseBack;
    public GameObject Point;
    private void Update()
    {
        SetPoint();
        TrueBack.GetComponent<Button>().onClick.AddListener(OnTrueButton);
        FalseBack.GetComponent<Button>().onClick.AddListener(OnFalseButton);
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
                    case "TrueBack":
                        Point.transform.SetParent(TrueBack.transform);
                        break;
                    case "FalseBack":
                        Point.transform.SetParent(FalseBack.transform);
                        break;
                }
                Point.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 0);
            }
        }
    }
    private void OnTrueButton()
    {
        TimeText.text = "";
        DataManager.Instance.Delete(Index);
        if (Index == GameManager.Instance.GlobalData.SaveTime1.Index)
        {
            GameManager.Instance.GlobalData.SaveTime1.Time = "";
        }
        if (Index == GameManager.Instance.GlobalData.SaveTime2.Index)
        {
            GameManager.Instance.GlobalData.SaveTime2.Time = "";
        }
        if (Index == GameManager.Instance.GlobalData.SaveTime3.Index)
        {
            GameManager.Instance.GlobalData.SaveTime3.Time = "";
        }
        DataManager.Instance.SaveGlobal();
        gameObject.SetActive(false);
    }
    private void OnFalseButton()
    {
        gameObject.SetActive(false);
    }
}

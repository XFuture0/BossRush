using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuCanvs : MonoBehaviour
{
    public GameObject SettingCanvs;
    public GameObject SaveCanvs;
    public GameObject Point;
    public Button LoadButton;
    public Button SettingButton;
    public Button QuitButton;
    public GameObject StartText;
    public GameObject SettingText;
    public GameObject ExitText;
    private void Awake()
    {
        LoadButton.onClick.AddListener(OnLoad);
        SettingButton.onClick.AddListener(OnSetting);
        QuitButton.onClick.AddListener(OnQuit);
    }
    private void OnEnable()
    {
        StartCoroutine(OpenMenu());
    }
    private void OnDisable()
    {
        CloseMenu();
    }
    private void Update()
    {
        SetPoint();
    }
    private void OnLoad()
    {
        SaveCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnSetting()
    {
        SettingCanvs.SetActive(true);
    }
    private void OnQuit()
    {
        Application.Quit();
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
                    case "StartText":
                        Point.transform.SetParent(StartText.transform);
                        break;
                    case "SettingText":
                        Point.transform.SetParent(SettingText.transform);
                        break;
                    case "ExitText":
                        Point.transform.SetParent(ExitText.transform);
                        break;
                }
                Point.GetComponent<RectTransform>().anchoredPosition = new Vector2(-370, 0);
            }
        }
    }
    private IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(0.5f);
        StartText.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SettingText.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ExitText.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Point.SetActive(true);
    }
    private void CloseMenu()
    {
        StartText.transform.parent.gameObject.SetActive(false);
        SettingText.transform.parent.gameObject.SetActive(false);
        ExitText.transform.parent.gameObject.SetActive(false);
        Point.SetActive(false);
    }
}

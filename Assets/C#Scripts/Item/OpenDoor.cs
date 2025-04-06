using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class OpenDoor : MonoBehaviour
{
    private int BossHealth;
    private bool IsOpen;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO CloseDoorEvent;
    private void OnEnable()
    {
        CloseDoorEvent.OnEventRaised += CloseDoor;
    }
    private void Update()
    {
        BossHealth = (int)GameManager.Instance.BossStats.CharacterData_Temp.NowHealth;
        if(BossHealth <= 0 && !IsOpen)
        {
            IsOpen = true;
            OnOpenDoor();
        }
        else if(BossHealth > 0)
        {
            IsOpen = false;
        }
    }
    private void OnOpenDoor()
    {
        var BallTypes = GameManager.Instance.ChooseBallType();
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Door>().ChooseBallType(BallTypes[0]);
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<PolygonCollider2D>().enabled = true;
        transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<Door>().ChooseBallType(BallTypes[1]);
    }
    public void CloseDoor()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = false;
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(1).GetComponent<PolygonCollider2D>().enabled = false;
        transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CloseDoorEvent.OnEventRaised -= CloseDoor;
    }
}

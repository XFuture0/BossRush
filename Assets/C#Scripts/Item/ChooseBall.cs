using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBall : MonoBehaviour
{
    public GameObject CardCanvs;
    private bool IsPlayer;
    public Sprite Attack;
    public Sprite Speed;
    [Header("ÊÂ¼þ¼àÌý")]
    public BallTypeEventSO BallTypeEvent;
    private void OnEnable()
    {
        BallTypeEvent.OnBallTypeEventRaised += ChooseBallType;
    }
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            KeyBoardManager.Instance.StopMoveKey = true;
            IsPlayer = false;
            CardCanvs.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsPlayer = false;
        }
    }
    public void ChooseBallType(BallType ballType)
    {
        switch (ballType)
        {
            case BallType.Attack:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Attack;
                break;
            case BallType.Speed:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Speed;
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        BallTypeEvent.OnBallTypeEventRaised -= ChooseBallType;
    }
}

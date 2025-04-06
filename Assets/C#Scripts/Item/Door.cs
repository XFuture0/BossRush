using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite Attack;
    public Sprite Speed;
    private BallType ThisBallType;
    private bool IsPlayer;
    public SceneData NowScene;
    public SceneData NextScene;
    public void ChooseBallType(BallType ballType)
    {
        ThisBallType = ballType;
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
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            GameManager.Instance.ThisBallType = ThisBallType;
            SceneChangeManager.Instance.ChangeScene(NowScene,NextScene);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
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
}

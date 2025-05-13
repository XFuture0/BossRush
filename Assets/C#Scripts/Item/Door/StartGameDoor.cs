using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameDoor : MonoBehaviour
{
    public SceneData CurrentScene;
    public SceneData NextScene;
    private bool IsPlayer;
    private void Update()
    {
        if (IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            KeyBoardManager.Instance.StopAnyKey = true;
            SceneChangeManager.Instance.OpenStartCanvs();
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
}

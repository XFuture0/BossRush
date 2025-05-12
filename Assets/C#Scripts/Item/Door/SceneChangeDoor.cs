using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeDoor : MonoBehaviour
{
    public SceneData CurrentScene;
    public SceneData NextScene;
    private bool IsPlayer;
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            SceneChangeManager.Instance.ChangeScene(CurrentScene,NextScene);
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
        if(other.tag == "Player")
        {
            IsPlayer = false;
        }
    }
}

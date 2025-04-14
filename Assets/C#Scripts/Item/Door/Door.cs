using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxcollider2D;
    private SpriteRenderer spriteRenderer;
    private bool IsPlayer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxcollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            CloseDoor();
        }
        if (spriteRenderer.color != ColorManager.Instance.DoorColor)
        {
            spriteRenderer.color = ColorManager.Instance.DoorColor;
        }
    }
    private void DoorDisable()//关闭门(动画中使用)
    {
        gameObject.SetActive(false);
    }
    public void SetDoor()
    {
        anim.SetTrigger("Idle");
        gameObject.transform.localPosition = new Vector3(-21, 0, 0);
    }
    public void OpenDoor()
    {
        boxcollider2D.enabled = true;
        anim.SetTrigger("Open");
    }
    public void CloseDoor()
    {
        boxcollider2D.enabled = false;
        anim.SetTrigger("Close");
    }
    private void NextRoom()//关闭门(动画中使用)
    {
        SceneChangeManager.Instance.ChangeScene();
        gameObject.SetActive(false);
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

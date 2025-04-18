using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPaper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool IsPlayer;
    [Header("¹ã²¥")]
    public VoidEventSO OpenCardCanvsEvent;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(spriteRenderer.color != ColorManager.Instance.UpdateColor(2))
        {
            spriteRenderer.color = ColorManager.Instance.UpdateColor(2);
        }
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            OpenCardCanvsEvent.RaiseEvent();
            Destroy(gameObject);
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

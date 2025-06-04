using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool IsPlayer;
    public GameObject RKey;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.sprite = GameManager.Instance.PlayerData.CurrentColor.FrultSprite;
    }
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            GetFruit();
        }
    }
    private void GetFruit()
    {
        FruitManager.Instance.GetFruit(GameManager.Instance.PlayerData.CurrentColor.FrultSprite, GameManager.Instance.PlayerData.CurrentColor.InvokeName,GameManager.Instance.PlayerData.CurrentColor.Index);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IsPlayer = true;
            RKey.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IsPlayer = false;
            RKey.SetActive(false);
        }
    }
}

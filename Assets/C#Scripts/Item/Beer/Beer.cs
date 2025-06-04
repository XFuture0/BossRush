using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ReloadBeer(int index)
    {
        switch (index)
        {
            case 1:
                spriteRenderer.sprite = SceneChangeManager.Instance.PlayerRoomData.Beer1;
                break;
            case 2:
                spriteRenderer.sprite = SceneChangeManager.Instance.PlayerRoomData.Beer2;
                break;
            case 3:
                spriteRenderer.sprite = SceneChangeManager.Instance.PlayerRoomData.Beer3;
                break;
        }
        
    }
}

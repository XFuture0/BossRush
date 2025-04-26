using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCanvs : MonoBehaviour
{
    private int PlayerHeart_Count;
    public GameObject PlayerHeart;
    public GameObject PlayerHeartBox;
    public List<GameObject> PlayerHearts;
    private void Update()
    {
        UpdataHeart();
    }
    private void UpdataHeart()
    {
        var HeartCount = GameManager.Instance.PlayerStats.CharacterData_Temp.NowHealth;
        if(PlayerHeart_Count > HeartCount && PlayerHearts.Count > 0)
        {
            Destroy(PlayerHearts[PlayerHearts.Count - 1]);
            PlayerHearts.Remove(PlayerHearts[PlayerHearts.Count - 1]);
            PlayerHeart_Count--;
        }
        if (PlayerHeart_Count < HeartCount)
        {
            var NewHeart = Instantiate(PlayerHeart, PlayerHeartBox.transform);
            PlayerHearts.Add(NewHeart);
            PlayerHeart_Count++;
        }
    }
}

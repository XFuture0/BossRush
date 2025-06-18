using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCanvs : MonoBehaviour
{
    private int PlayerHeart_Count;
    private int PlayerShield_Count;
    public GameObject PlayerHeart;
    public GameObject PlayerShield;
    public GameObject PlayerHeartBox;
    public List<GameObject> PlayerHearts;
    public List<GameObject> PlayerShields;
    private void Update()
    {
        UpdataHeart();
        UpdateShield();
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
    private void UpdateShield()
    {
        var ShieldCount = GameManager.Instance.PlayerStats.CharacterData_Temp.Shield;
        if (PlayerShield_Count > ShieldCount && PlayerShields.Count > 0)
        {
            Destroy(PlayerShields[PlayerShields.Count - 1]);
            PlayerShields.Remove(PlayerShields[PlayerShields.Count - 1]);
            PlayerShield_Count--;
        }
        if (PlayerShield_Count < ShieldCount)
        {
            var NewShield = Instantiate(PlayerShield, PlayerHeartBox.transform);
            PlayerShields.Add(NewShield);
            PlayerShield_Count++;
        }
    }
}

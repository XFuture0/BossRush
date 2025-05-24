using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStats : MonoBehaviour
{
    public void Glass()
    {

    }
    public void IceBlock()
    {
        GameManager.Instance.Player().Speed += 200;
    }
    public void CancelIceBlock()
    {
        GameManager.Instance.Player().Speed -= 200;
    }
    public void CaramelPudding()
    {
        GameManager.Instance.PlayerData.JumpForce += 7;
    }
    public void CancelCaramelPudding()
    {
        GameManager.Instance.PlayerData.JumpForce -= 7;
    }
    public void AcidicLemon()
    {
        GameManager.Instance.Player().AttackRate -= 0.5f;
    }
    public void CancelAcidicLemon()
    {
        GameManager.Instance.Player().AttackRate += 0.5f;
    }
    public void Cherry()
    {
        CoinManager.Instance.GiveCoins(GameManager.Instance.PlayerStats.gameObject.transform.position + new Vector3(5, 5), 50);
    }
}

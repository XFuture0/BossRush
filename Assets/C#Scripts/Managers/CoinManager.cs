using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : SingleTons<CoinManager>
{
    public GameObject Coin;
    public Transform CoinBox;
   public void GiveCoins(Vector3 GivePosition,int CoinCount)
   {
        StartCoroutine(OnGetCoins(GivePosition,CoinCount));
   }
    private IEnumerator OnGetCoins(Vector3 GivePosition, int CoinCount)
    {
        for (int i = 0; i < CoinCount; i++)
        {
            var NewCoin = Instantiate(Coin, GivePosition, Quaternion.identity,CoinBox);
            var CoinSpeedX = UnityEngine.Random.Range(-10, 10f);
            var CoinSpeedY = UnityEngine.Random.Range(0, 10f);
            NewCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(CoinSpeedX,CoinSpeedY);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

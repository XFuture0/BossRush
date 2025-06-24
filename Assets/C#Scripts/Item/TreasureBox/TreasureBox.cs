using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool IsPlayer;
    private bool HaveExtraGem;
    public GameObject RKey;
    public TreasureList TreasureList;
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            StartCoroutine(OpenTreasureBox());
        }
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
        if (other.tag == "Player")
        {
            IsPlayer = false;
            RKey.SetActive(false);
        }
    }
    private IEnumerator OpenTreasureBox()
    {
        foreach (var Treasure in TreasureList.TreasureLists)
        {
            var Treasure_Probability = UnityEngine.Random.Range(0f,100f);
            if(Treasure_Probability < Treasure.Probability)
            {
                if(Treasure.ItemObject.tag == "ExtraGem")
                {
                    if (!HaveExtraGem)
                    {
                        HaveExtraGem = true;
                        Instantiate(Treasure.ItemObject, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                else if(!(Treasure.ItemObject.tag == "ExtraGem"))
                {
                    Instantiate(Treasure.ItemObject, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        Destroy(gameObject);
    }
}

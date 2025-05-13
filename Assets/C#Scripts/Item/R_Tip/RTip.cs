using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTip : MonoBehaviour
{
    public bool IsPlayer;
    public GameObject RTipImage;
    private void Update()
    {
        if (IsPlayer)
        {
            RTipImage.SetActive(true);
        }
        else
        {
            RTipImage.SetActive(false);
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
        if (other.tag == "Player")
        {
            IsPlayer = false;
        }
    }
}

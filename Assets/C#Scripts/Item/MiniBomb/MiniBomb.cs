using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBomb : MonoBehaviour
{
    public float SaveTime;
    private void Awake()
    {
        Invoke("Destorying", SaveTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.Attack(GameManager.Instance.PlayerStats, 1);
            Destroy(gameObject);
        }
    }
    private void Destorying()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBall : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Destroying", 10);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            GameManager.Instance.Attack(GameManager.Instance.PlayerStats, 1);
            Destroy(gameObject);
        }
    }
    private void Destroying()
    {
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public Collider2D BossBound;
    [Header("¹ã²¥")]
    public BoundEventSO BoundEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            BoundEvent.BoundRaiseEvent(BossBound);
            Destroy(gameObject);
        }
    }
}

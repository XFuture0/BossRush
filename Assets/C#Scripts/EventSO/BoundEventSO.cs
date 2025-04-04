using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/BoundEventSO")]
public class BoundEventSO : ScriptableObject
{
    public UnityAction<Collider2D> OnBoundEventRaised;
    public void BoundRaiseEvent(Collider2D collider2D)
    {
        OnBoundEventRaised?.Invoke(collider2D);
    }
}

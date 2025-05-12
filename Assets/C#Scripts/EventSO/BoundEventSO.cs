using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/BoundEventSO")]
public class BoundEventSO : ScriptableObject
{
    public UnityAction<Collider2D,float> OnBoundEventRaised;
    public void RaiseBoundEvent(Collider2D collider2D,float Size)
    {
        OnBoundEventRaised?.Invoke(collider2D,Size);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/Vector3EventSO")]
public class Vector3EventSO : ScriptableObject
{
    public UnityAction<Vector3> OnVector3EventRaised;
    public void RaiseVector3Event(Vector3 vector3)
    {
        OnVector3EventRaised?.Invoke(vector3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/BallTypeEventSO")]

public class BallTypeEventSO : ScriptableObject
{
    public UnityAction<BallType> OnBallTypeEventRaised;
    public void BallTypeRaiseEvent(BallType ballType)
    {
        OnBallTypeEventRaised?.Invoke(ballType);
    }

}

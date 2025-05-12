using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New SceneData", menuName = "Data/SceneData")]
public class SceneData : ScriptableObject
{
    public string SceneName;
    public float Size;//摄影机画面大小
}

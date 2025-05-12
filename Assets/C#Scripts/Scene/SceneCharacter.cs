using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCharacter : MonoBehaviour
{
    public SceneData CurrentScene;
    private PolygonCollider2D Bound;
    private void Awake()
    {
        Bound = GetComponent<PolygonCollider2D>();
    }
    private void Start()
    {
        GameManager.Instance.OnBoundEvent(Bound,CurrentScene.Size);
    }
}

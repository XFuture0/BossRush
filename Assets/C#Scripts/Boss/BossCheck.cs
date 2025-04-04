using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    public bool LeftWall;
    public Vector2 LeftWallOffect;
    public bool RightWall;
    public Vector2 RightWallOffect;
    public bool IsGround;
    public Vector2 GroundOffect;
    public LayerMask Ground;

    private void Update()
    {
        LeftWall = Physics2D.OverlapCircle((Vector2)transform.position + LeftWallOffect,0.1f,Ground);
        RightWall = Physics2D.OverlapCircle((Vector2)transform.position + RightWallOffect,0.1f,Ground);
        IsGround = Physics2D.OverlapCircle((Vector2)transform.position + GroundOffect,0.1f,Ground);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + LeftWallOffect, 0.1f);
        Gizmos.DrawSphere((Vector2)transform.position + RightWallOffect, 0.1f);
        Gizmos.DrawSphere((Vector2)transform.position + GroundOffect, 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    public GameObject Monster10;
    [Header("æ‡¿Îº‡≤‚")]
    public Vector2 LeftUpPo;
    public Vector2 RightDownPo;
    public LayerMask Player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ThisStats = GetComponent<CharacterStats>();
    }
    private void FixedUpdate()
    {
        if (Physics2D.OverlapArea((Vector2)transform.position + LeftUpPo, (Vector2)transform.position + RightDownPo, Player))
        {
            var MoveRo = (SceneChangeManager.Instance.Player.transform.position - transform.position).normalized;
            rb.velocity = MoveRo * ThisStats.CharacterData_Temp.Speed * Time.deltaTime;
        }
    }
    private void OnDestroy()
    {
        for(int i = 0;i < 2; i++)
        {
            var SetPo = new Vector2(UnityEngine.Random.Range(-1,1), UnityEngine.Random.Range(-1, 1));
            Instantiate(Monster10, (Vector2)transform.position + SetPo, Quaternion.identity);
        }
    }
}

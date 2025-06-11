using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster12 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ThisStats = GetComponent<CharacterStats>();
    }
    private void FixedUpdate()
    {
        var MoveRo = (SceneChangeManager.Instance.Player.transform.position - transform.position).normalized;
        rb.velocity = MoveRo * ThisStats.CharacterData_Temp.Speed * Time.deltaTime;
    }
}

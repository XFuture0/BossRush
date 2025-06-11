using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster18 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float JumpHigh;
    public float JumpDistance;
    public GameObject ShootBall;
    [Header("跳跃计时器")]
    public float JumpTime;
    private float JumpTimeCount;
    public float JumpWaitTime;
    [Header("攻击计时器")]
    public float AttackTime;
    private float AttackTimeCount;
    private void Awake()
    {
        AttackTimeCount = AttackTime;
        rb = GetComponent<Rigidbody2D>();
        JumpTimeCount = JumpTime;
    }
    private void Update()
    {
        if (JumpTimeCount >= -2)
        {
            JumpTimeCount -= Time.deltaTime;
        }
        if (JumpTimeCount < 0)
        {
            JumpTimeCount = JumpTime;
            StartCoroutine(Jump());
        }
        if (AttackTimeCount > -2)
        {
            AttackTimeCount -= Time.deltaTime;
        }
        if (AttackTimeCount <= 0)
        {
            AttackTimeCount = AttackTime;
            Shoot();
        }
    }
    private IEnumerator Jump()
    {
        for (int i = 0; i < 3; i++)
        {
            var jumpro = UnityEngine.Random.Range(0f, 1f);
            if (GameManager.Instance.PlayerStats.gameObject.transform.position.x - transform.position.x > 0)
            {
                rb.AddForce(new Vector2(JumpDistance, JumpHigh), ForceMode2D.Impulse);
            }
            else if (GameManager.Instance.PlayerStats.gameObject.transform.position.x - transform.position.x < 0)
            {
                rb.AddForce(new Vector2(-JumpDistance, JumpHigh), ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(JumpWaitTime);
        }
    }
    private void Shoot()
    {
        var NewBall = Instantiate(ShootBall, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
    }
}

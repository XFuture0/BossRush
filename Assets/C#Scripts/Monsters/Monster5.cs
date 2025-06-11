using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster5 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float JumpHigh;
    public float JumpDistance;
    [Header("ÌøÔ¾¼ÆÊ±Æ÷")]
    public float JumpTime;
    private float JumpTimeCount;
    public float JumpWaitTime;
    private void Awake()
    {
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
    }
    private IEnumerator Jump()
    {
        for(int i = 0;i < 3; i++)
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
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LayerMask LaserLayerMask;
    public LayerMask Ground1;
    public LayerMask Ground2;
    private LineRenderer line;
    public bool IsLevel5;
    [Header(" ‹…Àº∆ ±∆˜")]
    private float HurtTime_Count;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if(HurtTime_Count > -1)
        {
            HurtTime_Count -= Time.deltaTime;
        }
        if (line.material.color != ColorManager.Instance.UpdateColor(2))
        {
            line.material.color = ColorManager.Instance.UpdateColor(2);
        }
        UpdateLongth();
        if (IsLevel5)
        {
            LaserLayerMask = Ground2;
        }
        else if (!IsLevel5)
        {
            LaserLayerMask = Ground1;
        }
    }
    private void UpdateLongth()
    {
        var Direction = transform.GetChild(0).position - transform.position;
        var ray = Physics2D.Raycast(transform.position, Direction, 100, LaserLayerMask);
        line.SetPosition(0, transform.parent.position);
        line.SetPosition(1, ray.point);
        if(ray.collider != null)
        {
            if (ray.collider.gameObject.tag == "Player" && HurtTime_Count < 0)
            {
                HurtTime_Count = 0.5f;
                GameManager.Instance.Attack(GameManager.Instance.BossStats, GameManager.Instance.PlayerStats);
            }
        }
    }
    private void OnDisable()
    {
        IsLevel5 = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletBox : MonoBehaviour
{
    public Transform bulletBox;
    public Bullet bullet;
    [HideInInspector]public ObjectPool<Bullet> BulletPool;
    [Header("攻速计时器")]
    public float BaseAttackSpeedTime;
    private float AttackSpeedTime_Count;
    [Header("事件监听")]
    public GameObjectEventSO ReturnPoolEvent;
    private void Awake()
    {
        BulletPool = new ObjectPool<Bullet>(bullet);
        BulletPool.Box = transform.GetChild(0);
    }
    private void Update()
    {
        if (AttackSpeedTime_Count >= -1)
        {
            AttackSpeedTime_Count -= Time.deltaTime;
        }
        if (KeyBoardManager.Instance.GetKey_Mouse0() && AttackSpeedTime_Count < 0)
        {
            AttackSpeedTime_Count = BaseAttackSpeedTime * GameManager.Instance.PlayerStats.CharacterData_Temp.AttackRate;
            StartCoroutine(SetBullet());
        }
    }
    private IEnumerator SetBullet()
    {
        var NewBullet = BulletPool.GetObject();
        NewBullet.transform.localPosition = Vector3.zero;
        var bulletRotation = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.parent.parent.position).normalized;
        NewBullet.GetComponent<Bullet>().BulletRotation = bulletRotation;
        yield return new WaitForSeconds(0.02f);
        NewBullet.transform.SetParent(bulletBox);
    }
    private void OnEnable()
    {
        ReturnPoolEvent.OnGameObjectEventRaised += ReturnPool;
    }
    private void ReturnPool(GameObject bullet)
    {
        BulletPool.ReturnObject(bullet.GetComponent<Bullet>());
    }
    private void OnDisable()
    {
        ReturnPoolEvent.OnGameObjectEventRaised -= ReturnPool;
    }
    public void ClearPool()
    {
        for(int i = 0;i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
        for (int i = 0; i < bulletBox.childCount; i++)
        {
            Destroy(bulletBox.GetChild(i).gameObject);
        }
        BulletPool.ClearPool();
    }
}

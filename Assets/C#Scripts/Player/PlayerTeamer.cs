using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerTeamer : MonoBehaviour
{
    private Animator anim;
    public GameObject Player;
    public GameObject WeaponBox;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        TeamerAnim();
       if (Player.transform.localScale.x == 1)
       {
            WeaponBox.transform.localScale = new Vector3(1, 1, 1);
       }
       else if(Player.transform.localScale.x == -1)
       {
            WeaponBox.transform.localScale = new Vector3(-1, 1, 1);
       }
    }
    private void TeamerAnim()
    {
        anim.SetBool("Attack", KeyBoardManager.Instance.GetKey_Mouse0());
        anim.SetFloat("Speed", math.abs(Player.GetComponent<Rigidbody2D>().velocity.x));
    }
    public void RefreshSlimeData(SlimeData slimeData)
    {
        anim.runtimeAnimatorController = slimeData.SlimeAnim;
        WeaponBox.GetComponent<Weapon>().SlimeData = slimeData;
    }
}

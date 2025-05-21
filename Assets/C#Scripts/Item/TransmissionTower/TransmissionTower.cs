using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionTower : MonoBehaviour
{
    private Animator anim;
    private bool CanTransmission;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(CanTransmission && KeyBoardManager.Instance.GetKeyDown_R())
        {
            MapManager.Instance.OpenTransmission(Physics2D.OverlapPoint(SceneChangeManager.Instance.Player.transform.position,SceneChangeManager.Instance.Room).gameObject.transform.position);
        }
    }
    private void OnEnable()
    {
        if (MapManager.Instance.GetRoom(Physics2D.OverlapPoint(transform.position, SceneChangeManager.Instance.Room).gameObject.transform.position))
        {
            anim.SetBool("Open", true);
            CanTransmission = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("Open", true);
            CanTransmission = true;
        }
    }
}

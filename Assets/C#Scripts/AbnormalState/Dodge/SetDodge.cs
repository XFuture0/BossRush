using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDodge : MonoBehaviour
{
    public GameObject Dodge;
    public void OnSetDodge()
    {
        var NewDodge = Instantiate(Dodge, transform.position, Quaternion.identity);
        switch (gameObject.tag) 
        {
            case "Player":
                NewDodge.transform.position += new Vector3(0, 1, 0);
                break;
            case "Boss":
                NewDodge.transform.position += new Vector3(0, 2.1f, 0);
                break;
        }
    }
}

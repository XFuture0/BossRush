using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public float UpSpeed;//…œ…˝ÀŸ∂»
    private void FixedUpdate()
    {
        transform.position += Vector3.up * UpSpeed * Time.deltaTime;
    }
    private void Destorying()
    {
        Destroy(gameObject);
    }
}

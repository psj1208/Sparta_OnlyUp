using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            Rigidbody rg = collision.transform.GetComponent<Rigidbody>();
            rg.AddForce(jumpPower * transform.up, ForceMode.Impulse);
        }
    }
}

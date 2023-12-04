using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.CHARACTER_TAG))
        {
            other.GetComponent<Character>().OnDead();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody rb;
    public Character owner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.CHARACTER_TAG))
        {
            other.GetComponent<Character>().SetAttacker(owner);
            other.GetComponent<Character>().OnDead();
            
        }
    }
    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }
}

using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Character owner;

    private int range;
    private int speed;

    public void OnInit(Character owner,Vector3 direction,WeaponType weaponType)
    { 
        this.range = weaponType.range;
        this.speed = weaponType.speed;
        this.owner = owner;
        rb.velocity = direction.normalized * speed;
        OnDespawn();
    }

    private void OnDespawn()
    {
        StartCoroutine(DestroyWeapon());
    }
   
    private IEnumerator DestroyWeapon()
    {
        yield return new WaitForSeconds(2f);
        owner.SetActiveCurrentWeapon(true);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Character crt = other.GetComponent<Character>();
        if (other.CompareTag(Const.CHARACTER_TAG) && crt!=owner)
        {
            owner.SetActiveCurrentWeapon(true);
            crt.SetAttacker(owner);
            crt.OnHit();
            Destroy(this.gameObject);
            
        }
    }

}

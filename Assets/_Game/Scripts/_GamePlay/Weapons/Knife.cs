using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{

    void Start()
    {
        OnInit();
        
    }

    private void OnInit()
    {
        rb.velocity = transform.forward * 5f;
        OnDespawn();
    }

    private void OnDespawn()
    {
        StartCoroutine(DestroyWeapon());
    }

    private IEnumerator DestroyWeapon()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
   

}

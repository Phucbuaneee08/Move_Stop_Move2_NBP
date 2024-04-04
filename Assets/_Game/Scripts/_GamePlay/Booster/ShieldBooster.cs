using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBooster : GameUnit
{

    public Character owner;

    private void OnEnable()
    {
        StartCoroutine(OnDespawn());
    }
    private void OnTriggerEnter(Collider other)
    {
    
        Bullet bullet = other.GetComponent<Bullet>();
        if (other.CompareTag(Const.BULLET_TAG) && owner!=bullet.owner)
        {
            SimplePool.Despawn(bullet);
            SimplePool.Despawn(this);
            bullet.owner.IsHavingBooster = false;
        }
       
    }
    private IEnumerator OnDespawn()
    {
        yield return new WaitForSeconds(10f);
        SimplePool.Despawn(this);
    }
  

}

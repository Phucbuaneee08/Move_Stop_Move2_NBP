using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    public void Throw(Character owner,Vector3 direction,WeaponType weaponType)
    {
        Bullet bullet = Instantiate(bulletPrefab, owner.transform.position + new Vector3(0,1,0), owner.transform.rotation);
       
        bullet.OnInit(owner, direction, weaponType);
    }
    
   
}

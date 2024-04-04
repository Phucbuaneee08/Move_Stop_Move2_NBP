using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Booster : GameUnit
{
 
    private BoosterType _type;
   public enum BoosterType { 
        Booster_Shield=PoolType.Booster_Shield,
        Booster_X3= PoolType.Booster_X3,
        Booster_UpRange = PoolType.Booster_UpRange
    }
   public void DropBooster(Vector3 pos)
    {
        _type = Utilities.RandomEnumValue<BoosterType>();
        SimplePool.Spawn<Booster>(PoolType.Booster,pos,Quaternion.identity);
    }
    private void OnCollisionEnter(Collision collision)
    {

        _type = BoosterType.Booster_Shield;
        Character crt = collision.gameObject.GetComponent<Character>();
        if (collision.gameObject.CompareTag(Const.CHARACTER_TAG))
        {
            crt.BoosterType = _type;
            crt.IsHavingBooster = true;
            if (_type == BoosterType.Booster_Shield)
            {
                InitShield(crt);
            }
            SimplePool.Despawn(this);
        }
    }
    public void InitShield(Character crt)
    {
        ShieldBooster shieldBooster = SimplePool.Spawn<ShieldBooster>((PoolType)_type, crt.transform);
        if(shieldBooster != null)
        {
            shieldBooster.owner = crt;
        }
       
    }



}

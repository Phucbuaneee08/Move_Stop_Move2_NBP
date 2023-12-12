
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scriptable
{
    public enum WeaponName
    {
        Hammer = 0,
        Knife = 1,
        Candy = 2,
        Boomerang = 3
    }

    [CreateAssetMenu(menuName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {

        [SerializeField] WeaponType[] weaponTypes;

        public Weapon GetWeapon(WeaponName weaponName)
        {
            return weaponTypes[(int)weaponName].model;
        }
        public WeaponType GetWeaponType(WeaponName weaponName)
        {
            return weaponTypes[(int)weaponName];
        }
       
        public Sprite GetIcon(PantName weaponName)
        {
            return weaponTypes[(int)weaponName].icon;
        }

    }
}
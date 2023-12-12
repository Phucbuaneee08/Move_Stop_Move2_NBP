
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scriptable
{
    public enum ShieldName
    {
        Khien = 0,
        Shield = 1  
    }

    [CreateAssetMenu(fileName ="New Shield",menuName = "ShieldData")]
    public class ShieldData : ScriptableObject
    {

        [SerializeField] ShieldType[] shieldTypes;


        public GameObject GetShield(ShieldName shieldName)
        {
            return shieldTypes[(int)shieldName].model;
        }
        public Sprite GetIcon(ShieldName shieldName)
        {
            return shieldTypes[(int)shieldName].icon;
        }

    }
}
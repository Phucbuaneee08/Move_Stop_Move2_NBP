
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

namespace Scriptable
{
    public enum HatName
    {
        Arrow = 0,
        Cowboy = 1,
        Crown = 2,
        Ear = 3,
        Hat = 4,
        Hat_Cap = 5,
        Hat_Yellow = 6,
        Headphone = 7,
        Rau = 8,
    }

    [CreateAssetMenu(menuName = "HatData")]
    public class HatData : ScriptableObject
    {

        [SerializeField] HatType[] hatTypes;

        public GameObject GetHat(HatName hatName)
        {
            return hatTypes[(int)hatName].model;
        }

        public Sprite GetIcon(HatName hatName)
        {
            return hatTypes[(int)hatName].icon;
        }

    }
}
using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ShieldType")]
public class ShieldType : ScriptableObject  
{
    public ShieldName shieldName;
    public Sprite icon;
    public GameObject model;
}

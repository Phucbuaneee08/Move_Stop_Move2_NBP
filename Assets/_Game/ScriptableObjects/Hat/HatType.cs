using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HatType")]
public class HatType : ScriptableObject
{
    public HatName hatName;
    public string hName;
    public int price;
    public Sprite icon;
    public GameObject model;
}

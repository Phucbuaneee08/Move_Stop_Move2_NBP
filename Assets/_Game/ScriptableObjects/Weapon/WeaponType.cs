using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponType")]
public class WeaponType : ScriptableObject
{
    public WeaponName WeaponName;
    public int index;
    public string wpName;
    public Weapon model;
    public Sprite icon;

    public int price;
    public int range;
    public int speed;
}
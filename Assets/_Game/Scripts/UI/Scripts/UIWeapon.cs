using Scriptable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UICanvas
{
    public Transform weaponPoint;

    [SerializeField] WeaponData weaponData;
    [SerializeField] ButtonState buttonState;
    [SerializeField] Text nameTxt;
    [SerializeField] Text playerCoinTxt;
    [SerializeField] Text coinTxt;
    [SerializeField] Text adsTxt;

    private Weapon currentWeapon;
    private WeaponName weaponName;
    private int currentWeaponPrice;
    public override void Setup()
    {
        base.Setup();
        ChangeWeapon(UserData.Ins.playerWeapon);
        playerCoinTxt.text =UserData.Ins.coin.ToString();
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();

        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = null;
        }

        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void NextButton()
    {
        ChangeWeapon(weaponData.NextType(weaponName));
    }

    public void PrevButton()
    {
        ChangeWeapon(weaponData.PrevType(weaponName));
    }

    public void BuyButton()
    {
        //TODO: check tien
        if (UserData.Ins.coin >= currentWeaponPrice)
        {
            UserData.Ins.coin -= currentWeaponPrice;
            UserData.Ins.SetEnumData(weaponName.ToString(), ShopItem.State.Bought);
            UserData.Ins.SetIntData(UserData.Key_Coin, ref UserData.Ins.coin, UserData.Ins.coin);
            ChangeWeapon(weaponName);
        }
    }

    //public void AdsButton()
    //{
    //    int ads = UserData.Ins.GetDataState(weaponName + "Ads", 0);
    //    UserData.Ins.SetDataState(weaponName + "Ads", ads + 1);

    //    if (ads + 1 >= weaponData.GetWeaponItem(weaponName).ads)
    //    {
    //        UserData.Ins.SetDataState(weaponName.ToString(), 1);
    //        ChangeWeapon(weaponName);
    //    }
    //}

    public void EquipButton()
    {
        Debug.Log("Equip");
        UserData.Ins.SetEnumData(weaponName.ToString(), ShopItem.State.Equipped);
        UserData.Ins.SetEnumData(UserData.Ins.playerWeapon.ToString(), ShopItem.State.Bought);
        UserData.Ins.SetEnumData(UserData.Key_Player_Weapon, ref UserData.Ins.playerWeapon, weaponName);
        ChangeWeapon(weaponName);
        LevelManager.Ins.player.TryCloth(UIShop.ShopType.Weapon, weaponName);
    }

    public void ChangeWeapon(WeaponName weaponName)
    {
        this.weaponName = weaponName;

        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
        }
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponName, Vector3.zero, Quaternion.identity, weaponPoint);

        //check data dong
        ButtonState.State state = (ButtonState.State)UserData.Ins.GetDataState(weaponName.ToString(), 0);
        buttonState.SetState(state);
                                                                                                                                     
        WeaponType item = weaponData.GetWeaponType(weaponName);
        nameTxt.text =item.name;
        coinTxt.text = item.price.ToString();
        currentWeaponPrice = item.price;
        //adsTxt.text = item.ads.ToString();
    }

}

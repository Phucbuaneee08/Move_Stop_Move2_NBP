using Scriptable;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class Player : Character
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody rb;
    public VariableJoystick variableJoystick;
    private Vector3 moveVector;
    private Vector3 direction;
    private float rotateSpeed = 10f;
 
    SkinType skinType = SkinType.Normal;
    WeaponName weaponName = WeaponName.Boomerang;
    HatName hatName = HatName.Arrow;
    PantName pantName = PantName.Dabao;
    ShieldName shieldName = ShieldName.Khien;
    private bool IsCanUpdate => GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Setting);

    public void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        if (variableJoystick != null && !IsDead && IsCanUpdate)
        {
            moveVector.x = variableJoystick.Horizontal;
            moveVector.z = variableJoystick.Vertical;
            direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            if (moveVector.magnitude > 0.1f && Input.GetMouseButton(0))
            {
                StopAllCoroutines();
                IsCanAttack = true;
                ChangeAnim(Const.ANIM_RUN);
                transform.rotation = Quaternion.LookRotation(direction);
                Vector3 targetPosition = transform.position + direction;
               
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                OnAttack();
                ChangeAnim(Const.ANIM_IDLE);
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public override void OnInit()
    {
        characterName = "You";
        OnTakeUserData();
        base.OnInit();
        ScaleUp(1); // set player default size 
        variableJoystick = null;
        IsDead = false;
        ChangeAnim(Const.ANIM_IDLE);
    }
    public override void OnDeath()
    {
        base.OnDeath();

    }
    public override void OnAttack()
    {
        if (IsCanAttack)
        {
            base.OnAttack();
        }

    }
    public void SetVariableJoyStick(VariableJoystick Vj)
    {
        this.variableJoystick = Vj;
    }
    public override void ResetAttack()
    {
        base.ResetAttack();
        CancelInvoke(nameof(StartReset));
        Invoke(nameof(StartReset), 3f);

    }
    public override void StartReset()
    {
        base.StartReset();
        IsCanAttack = true;
        SetActiveCurrentWeapon(true);
    }
    public override void ScaleUp(float size)
    {
        base.ScaleUp(size);
        CameraFollow.Ins.ScaleOffset(size);
    }
    public override void AddTarget(Character crt)
    {
        if (!crt.IsDead)
        {
            base.AddTarget(crt);
            crt.SetBoxIndicator(true);
        }
    }
    internal void OnRevive()
    {
        ChangeAnim(Const.ANIM_IDLE);
        IsDead = false;
        ClearTarget();
        //reviveVFX.Play();
    }
    public override void RemoveTarget(Character crt)
    {
        base.RemoveTarget(crt);
        crt.SetBoxIndicator(false);
    }
    public override void OnInitItem()
    {
        base.OnInitItem();
        ChangeSkin(skinType);
        ChangeHat(hatName);
        ChangePant(pantName);
        ChangeWeapon(weaponName);
        ChangeShield(shieldName);
    }
   
    public void OnTakeUserData()
    {
        weaponName = DataManager.Ins.playerData.playerWeapon;
        hatName = DataManager.Ins.playerData.playerHat;
        skinType = DataManager.Ins.playerData.playerSkin;
        pantName = DataManager.Ins.playerData.playerPant;
        shieldName = DataManager.Ins.playerData.playerShield;
    
    }
    public override void SetPoint(int point)
    {
        base.SetPoint(point);
        SimplePool.Spawn<FlyCoin>(PoolType.VFX_FlyCoin, transform.parent);
    }

    public void TryCloth(UIShop.ShopType shopType, Enum type)
    {
        switch (shopType)
        {
            case UIShop.ShopType.Hat:
                currentSkin.DespawnHat();
                ChangeHat((HatName)type);
                break;

            case UIShop.ShopType.Pant:
                ChangePant((PantName)type);
                break;

            case UIShop.ShopType.Shield:
                currentSkin.DespawnShield();
                ChangeShield((ShieldName)type);
                break;

            case UIShop.ShopType.Skin:
                OnDespawnItem();
                skinType = (SkinType)type;
                OnInitItem();
                break;
            case UIShop.ShopType.Weapon:
                currentSkin.DespawnWeapon();
                ChangeWeapon((WeaponName)type);
                break;
            default:
                break;
        }

    }



}

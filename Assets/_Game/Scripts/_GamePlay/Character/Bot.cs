using Scriptable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private IState currentState;
    public Transform obj;
    private Vector3 destination;
    private Vector3 direction;
    private Vector3 randomDirection3D;
    private Vector2 randomDirection2D;
    private int randomDistance;


    private bool IsCanRunning => (GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Setting));

    public override void OnInit()
    {
        base.OnInit();
        SetBoxIndicator(false);
        ResetAnim();
    }
    public void Update()
    {

        if (IsDead || !IsCanRunning) return;
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void SetDestination(Vector3 des)
    {
        this.destination = des;
        agent.enabled = true;
        agent.SetDestination(destination);
    }
    public override void RandomMove()
    {
        if (IsDead) return;
        //randomDirection2D = Random.insideUnitCircle.normalized;
        //randomDistance = Random.Range(10, 20);
        //randomDirection3D.x = randomDirection2D.x* randomDistance;
        //randomDirection3D.z = randomDirection2D.y* randomDistance;
        //direction = randomDirection3D - transform.position;
        //transform.rotation = Quaternion.LookRotation(direction);
        //SetDestination(randomDirection3D);
        SetDestination(LevelManager.Ins.RandomPoint());
        ChangeAnim(Const.ANIM_RUN);

    }
    public override void OnMoveStop()
    {
        base.OnMoveStop();
        agent.enabled = false;
        //ChangeAnim(Const.ANIM_IDLE);
    }
    public void StopRandomMove()
    {
        if (Vector3.Distance(TF.position, destination) - Mathf.Abs(TF.position.y - destination.y) < 0.1f)
        {
            agent.enabled = false;
            ChangeAnim(Const.ANIM_IDLE);
            ChangeState(new IdleState());
        }
    }
    public override void AddTarget(Character crt)
    {
        base.AddTarget(crt);
        if (!crt.IsDead && IsCanAttack && !this.IsDead && IsCanRunning)
        {
            ChangeState(new AttackState());
        }

    }
    public override void BotAttack()
    {
        SetDestination(transform.position);
    }
    public override void ResetAttack()
    {
        CancelInvoke(nameof(ResetAttack));
        Invoke(nameof(StartReset), 2f);
    }
    public override void StartReset()
    {
        base.StartReset();
        IsCanAttack = true;
        ChangeState(new PatrolState());

    }
    public override void OnDeath()
    {
        agent.enabled = false;
        ChangeState(null);
        base.OnDeath();
        Invoke(nameof(OnDespawn), 2f);

    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);

    }

    public virtual void ChangeState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    // Wearing Skin, Weapon, Pant ,...
    public override void OnInitItem()
    {
        base.OnInitItem();
        ChangeSkin(Utilities.RandomEnumValue<SkinType>());
        ChangeShield(Utilities.RandomEnumValue<ShieldName>());
        ChangeHat(Utilities.RandomEnumValue<HatName>());
        ChangePant(Utilities.RandomEnumValue<PantName>());
        ChangeWeapon(Utilities.RandomEnumValue<WeaponName>());

    }







}

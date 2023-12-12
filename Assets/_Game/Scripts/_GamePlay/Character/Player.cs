using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{   
    [SerializeField] private float moveSpeed = 300f;
    [SerializeField] private Rigidbody rb;
    public VariableJoystick variableJoystick;
    private Vector3 moveVector;
    private Vector3 direction;
    
    private float rotateSpeed = 10f;
    private void Start()
    {
        OnInit();
        ChangePant(PantName.Dabao);
        ChangeWeapon(WeaponName.Boomerang);
        ChangeHat(HatName.Crown);
        ChangeShield(ShieldName.Khien);
    }

    public override void Update()
    {
        base.Update();
    }


    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        
        moveVector.x = variableJoystick.Horizontal;
        moveVector.z = variableJoystick.Vertical;


        direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
        if(moveVector.magnitude >0.01f)
        {
            isAttack = false;
            ChangeState(new RunState());
            transform.rotation = Quaternion.LookRotation(direction);
            rb.velocity = direction* moveSpeed * Time.deltaTime;
        }
        else if(!isAttack)
        {
            ChangeState(new IdleState());
            rb.velocity = Vector3.zero;
        }
        

    }
    public override void OnDead()
    {
        base.OnDead();
    }
    public override void ResetAttack()
    {
        base.ResetAttack();
        isAttack = false;
        SetActiveCurrentWeapon(true);
        
    }
}

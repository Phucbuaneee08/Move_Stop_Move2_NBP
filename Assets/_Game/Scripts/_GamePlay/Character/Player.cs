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
    private bool isMove;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isMove = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMove = false;
            ChangeState(new IdleState());
        }
    }
    private void FixedUpdate()
    {
        if (isMove) Move();
        else Stop();
    }
    private void Stop()
    {
        ChangeAnim(Const.ANIM_IDLE);
        rb.velocity = Vector3.zero;
    }
    private void Move()
    {
        ChangeAnim(Const.ANIM_RUN);
        moveVector.x = variableJoystick.Horizontal;
        moveVector.z = variableJoystick.Vertical;
        direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(direction);
        rb.velocity = direction* moveSpeed * Time.deltaTime;

    }
}

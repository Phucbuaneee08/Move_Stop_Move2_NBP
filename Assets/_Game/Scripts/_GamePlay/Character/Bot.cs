using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    public Transform obj;
    private Vector3 destination;
    private Vector3 direction;
    private Vector3 randomDirection3D;
    private Vector2 randomDirection2D;
    private int randomDistance;

    private float timeInterval = 2f;
    private float countTime = 0f;
    private void Start()
    {
        ChangeState(new IdleState());

    }
    public override void Update()
    {
        base.Update();
        
    }
    public void SetDestination(Vector3 des)
    {
        this.destination = des;
        agent.SetDestination(destination);
    }
    public override void RandomMove()
    {
        ChangeAnim(Const.ANIM_RUN);
        randomDirection2D = Random.insideUnitCircle.normalized;
        randomDistance = Random.Range(10, 20);
        randomDirection3D.x = randomDirection2D.x* randomDistance;
        randomDirection3D.z = randomDirection2D.y* randomDistance;
        direction = randomDirection3D - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
          
        SetDestination(randomDirection3D);
      
    }
    public override void Stop()
    {
        if (Vector3.Distance(transform.position,randomDirection3D) < 4.92f)
        {
            Debug.Log("Stip");
            ChangeState(new IdleState());
        }
    }
    public override void BotAttack()
    {
        SetDestination(transform.position);

    }
    public override void ResetAttack()
    {
        ChangeState(new PatrolState());
    }
    public override void OnDead()
    {
        base.OnDead();
        SetDestination(transform.position);
    }
    private void MoveRandomDirection()
    {

    }
    
   



}

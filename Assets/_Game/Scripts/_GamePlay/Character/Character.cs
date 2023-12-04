using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Character : MonoBehaviour
{
  
    [SerializeField] private Animator anim;
    private string currentAnim;

    [HideInInspector] public Character currentAttacker;
    [HideInInspector] public string characterName;
    [SerializeField] private GameObject setTargetCircle;
    [SerializeField] private Weapon weaponPrefab;
    public List<Character> targets = new List<Character>();
    public Character currentTarget;
    private Vector3 targetDirection;
    private IState currentState;
    


    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    /********************************
              ANIMATION
    ********************************/

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    /********************************
               ATTACK
     ********************************/
    public void AddTarget(Character crt)
    {
        targets.Add(crt);
    }
    public bool CheckTarget(Character crt)
    {
        return targets.Contains(crt);
        
    }
    public void RemoveTarget(Character crt)
    {
        targets.Remove(crt);
    }
    public void Attack()
    {
        ChangeAnim(Const.ANIM_ATTACK);
        Instantiate(weaponPrefab, transform.position + new Vector3(0,1,1.2f), transform.rotation);
    }
    public void FocusTarget()
    {
        if (currentTarget != null) {
            targetDirection = currentTarget.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection); 
        }
    }
    public void ResetAttack()
    {
        ChangeAnim(Const.ANIM_IDLE);
    }

    public void ChooseTarget()
    {

        if (targets.Count>0)
        {
            currentTarget = targets[0]; 

            ChangeState(new AttackState());
        }
        
       
    }
    public void OnDead()
    {
        ChangeAnim(Const.ANIM_DEAD);
        StartCoroutine(OnDespawn());
    }
    private IEnumerator OnDespawn()
    {
        yield return new WaitForSeconds(2f);
        Destroy(targets[0].gameObject);
    }
   
    /********************************
              State Machine
    ********************************/
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
}

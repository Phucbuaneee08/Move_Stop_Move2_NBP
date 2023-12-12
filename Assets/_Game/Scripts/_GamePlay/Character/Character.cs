using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Character : MonoBehaviour
{
  
    [SerializeField] private Animator anim;
    private string currentAnim;
    // properties for attack
    [HideInInspector] public Character currentAttacker;
    [HideInInspector] public string characterName;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private WeaponType currentWeaponType;
    // properties for pant
    [SerializeField] private Renderer pantRenderer;
    [SerializeField] private PantData pantData;
    // properties for hat
    [SerializeField] private GameObject hatHolder;
    [SerializeField] private GameObject currentHat;
    [SerializeField] private HatData hatData;
    // properties for shield
    [SerializeField] private GameObject shieldHolder;
    [SerializeField] private GameObject currentShield;
    [SerializeField] private ShieldData shieldData;


    //
    public List<Character> targets = new List<Character>();
    public Character currentTarget;
    private Vector3 targetDirection;
    private IState currentState;
    public bool isAttack;
    private bool isWeaponActive;
    


    public virtual void Update()
    {
        
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void OnInit()
    {
        isAttack = false;
        isWeaponActive = true;
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
        StartCoroutine(IAttack());
    }
    private IEnumerator IAttack()
    {
        yield return new WaitForSeconds(0.3f);
        currentWeapon.Throw(this, targetDirection,currentWeaponType);
        SetActiveCurrentWeapon(false);

    }
    public void FocusTarget()
    {
        if (currentTarget != null) {
            targetDirection = currentTarget.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection); 
        }
    }
    public void SetActiveCurrentWeapon(bool wp)
    {
        currentWeapon.gameObject.SetActive(wp);
        isWeaponActive = wp;
    }
    public virtual void ResetAttack()
    {
        
    }
   
    public void SetAttacker(Character attacker)
    {
        this.currentAttacker = attacker;
    }
    public void ChooseTarget()
    {
        if (targets.Count>0)
        { 
            currentTarget = targets[0];
            if (currentTarget != null && isWeaponActive) {  
                isAttack = true;
                ChangeState(new AttackState());
            }
            Debug.Log(currentTarget);
            //if (currentTarget == null) { }
            //{
            //    Debug.Log("remove target");
            //    RemoveTarget(currentTarget);
            //}
        }
    }
  
    public void OnHit()
    {
         if (currentAttacker.CheckTarget(this))
        {
            currentAttacker.RemoveTarget(this);
        }
        OnDead();
    }
    public virtual void OnDead()
    {
       ChangeState(new DeadState());
       StartCoroutine(OnDespawn());
    }
    private IEnumerator OnDespawn()
    {
        yield return new WaitForSeconds(2f);
        DestroyImmediate(this.gameObject);   
    }
    /********************************
              Change Item
    ********************************/
    public void ChangePant(PantName pantName)
    {
        pantRenderer.material = pantData.GetMat(pantName);
    }
    public void ChangeWeapon(WeaponName weaponName)
    {
        currentWeaponType = weaponData.GetWeaponType(weaponName);
        Weapon newWeapon = Instantiate(weaponData.GetWeapon(weaponName),weaponHolder.transform.position,Quaternion.identity);
        newWeapon.transform.parent = weaponHolder.transform;
        currentWeapon = newWeapon;
    }
    public void ChangeHat(HatName hatName)
    {
        currentHat = Instantiate(hatData.GetHat(hatName), hatHolder.transform.position, Quaternion.identity);
        currentHat.transform.parent = hatHolder.transform;
    }
    public void ChangeShield(ShieldName shieldName)
    {
        currentShield = Instantiate(shieldData.GetShield(shieldName),shieldHolder.transform.position,Quaternion.identity);
        currentShield.transform.parent = shieldHolder.transform;
    }

















    /********************************
              State Machine
    ********************************/

    public virtual void RandomMove() { }
    public virtual void Stop() { }
    public virtual void BotAttack() {}
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

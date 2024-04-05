using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Character : AbstractCharacter
{

    [SerializeField] private Animator anim;
    [SerializeField] protected Skin currentSkin;
    [SerializeField] private string currentAnim;
    // properties for attack
    [HideInInspector] public Character currentAttacker;
    [HideInInspector] public string characterName;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private WeaponType currentWeaponType;
    [SerializeField] private GameObject boxIndicator;
    [SerializeField] private Transform characterSprite;
    // properties for character

    public Booster.BoosterType BoosterType { get;  set; }
    public bool IsHavingBooster { get;  set; }
    public int scalePoint;
    private int score;
    private float size = 1;
    public float Size => size;
    public List<Character> targets = new List<Character>();
    public Character currentTarget;
    private Vector3 targetDirection;
    private Vector3 localScale;

    public bool isWeaponActive;
    public bool IsDead { get; protected set; }
    public bool IsCanAttack { get; protected set; }

    /********************************
             ABI Function   
   ********************************/

    public override void OnInit()
    {
        ClearTarget();
        OnInitItem();
        localScale = currentSkin.transform.localScale;
        isWeaponActive = true;
        IsDead = false;
        IsCanAttack = true;
        scalePoint = 0;
        score = 0;

    }
    public override void OnDespawn()
    {

        OnDespawnItem();
        //DestroyImmediate(this.gameObject);
        CancelInvoke();
    }

    public override void OnDeath()
    {
        IsDead = true;
        ChangeAnim(Const.ANIM_DEAD);
        LevelManager.Ins.OnDeadEvent(this);
        if (currentAttacker.CheckTarget(this))
        {
            currentAttacker.RemoveTarget(this);
        }

    }

    /********************************
              ANIMATION
    ********************************/

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            currentSkin.Anim.ResetTrigger(animName);
            currentAnim = animName;
            currentSkin.Anim.SetTrigger(currentAnim);
        }
    }

    /********************************
               ATTACK
     ********************************/
    public virtual void AddTarget(Character crt)
    {
        targets.Add(crt);
    }
    public bool CheckTarget(Character crt)
    {
        return targets.Contains(crt);

    }
    public virtual void RemoveTarget(Character crt)
    {
        targets.Remove(crt);
    }
    public virtual void ClearTarget()
    {
        currentTarget = null;
        targets.Clear();
    }
    private IEnumerator IAttack()
    {
        yield return new WaitForSeconds(Const.DELAY_ATTACK); // delay attack animation 
        currentSkin.Weapon.Throw(this, targetDirection, currentWeaponType);
        SetActiveCurrentWeapon(false);

    }
    public void FocusTarget()
    {
        if (currentTarget != null)
        {
            targetDirection = (currentTarget.transform.position - transform.position);
            targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
    public void SetActiveCurrentWeapon(bool wp)
    {
        currentSkin.Weapon.gameObject.SetActive(wp);
        isWeaponActive = wp;
    }
    public void SetAttacker(Character attacker)
    {
        this.currentAttacker = attacker;
    }
    public void ChooseTarget()
    {
        float minDistance = float.PositiveInfinity;
        currentTarget = null;

        for (int i = 0; i < targets.Count; i++)
        {

            if (targets[i] != null && !targets[i].IsDead && Vector3.Distance(TF.position, targets[i].TF.position) <= Const.ATT_RANGE * size + targets[i].Size)
            {
                float distance = Vector3.Distance(TF.position, targets[i].TF.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentTarget = targets[i];

                }
            }

        }

    }
    public override void OnAttack()
    {

        ChooseTarget();

        if (currentTarget != null)
        {

            IsCanAttack = false;
            FocusTarget();
            ChangeAnim(Const.ANIM_ATTACK);
            StartCoroutine(IAttack());
            ResetAttack();
        }
        else
        {
            ResetAttack();
        }
    }
    public virtual void ResetAttack() { }

    public void OnHit()
    {
        NotiManager.Ins.PopUpWindow(this.characterName, currentAttacker.characterName);
        if (currentAttacker.CheckTarget(this))
        {
            currentAttacker.RemoveTarget(this);
        }
        OnDeath();
    }
    public void SetBoxIndicator(bool active)
    {
        boxIndicator.SetActive(active);
    }

    /********************************
              Scale 
    ********************************/
    public virtual void ScaleUp(float size)
    {
        //currentSkin.transform.localScale = Vector3.one + new Vector3(0.2f, 0.2f, 0.2f) * size;
        this.size = Mathf.Clamp(size, Const.MIN_SIZE, Const.MAX_SIZE);
        TF.localScale = Vector3.one * size;
        ParticlePool.Play(Utilities.RandomInMember(ParticleType.LevelUp_1, ParticleType.LevelUp_2, ParticleType.LevelUp_3), TF.position);
    }

    public virtual void SetPoint(int point)
    {
        scalePoint += Const.POINT_UNIT;
        score += Const.SCORE_UNIT;
        if (scalePoint % 2 == 0)
        {
            ScaleUp(1 + scalePoint / 2 * 0.1f);
        }

    }
    public void SetScore(int score)
    {
        this.score = score;
    }
    public int GetScore()
    {
        return score;
    }

    public Vector3 getCharacterScale()
    {
        return localScale;
    }
    public virtual void SpeedUp()
    {
        
        ParticlePool.Play(Utilities.RandomInMember(ParticleType.SpeedUp), TF);
        Invoke(nameof(ResetSpeed), 5f);
    }
    public virtual void ResetSpeed() {

    }
    /********************************
              Change Item
    ********************************/
    public void ChangePant(PantName pantName)
    {
        //pantRenderer.material = pantData.GetMat(pantName);
        currentSkin.ChangePant(pantName);
    }
    public void ChangeWeapon(WeaponName weaponName)
    {
        currentWeaponType = weaponData.GetWeaponType(weaponName);
        //currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponName, weaponHolder.transform);
        currentSkin.ChangeWeapon(weaponName);
    }
    public void ChangeHat(HatName hatName)
    {

        //currentHat = SimplePool.Spawn<Hat>((PoolType)hatName, hatHolder.transform);
        currentSkin.ChangeHat(hatName);

    }
    public void ChangeShield(ShieldName shieldName)
    {
        //currentShield = SimplePool.Spawn<Shield>((PoolType)shieldName, shieldHolder.transform);
        currentSkin.ChangeShield(shieldName);
    }
    public void ChangeSkin(SkinType skinType)
    {
        currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
    }
    public virtual void OnInitItem()
    {

    }
    public virtual void OnDespawnItem()
    {
        currentSkin?.OnDespawn();
        SimplePool.Despawn(currentSkin);
    }


    /********************************
              State Machine
    ********************************/

    public virtual void RandomMove() { }
    public virtual void BotAttack() { }

    /********************************
              ABI FUNCTION
    ********************************/

    public override void OnMoveStop()
    {

    }
    public virtual void StartReset()
    {

    }
    public void ResetAnim()
    {
        currentAnim = "";
    }

}

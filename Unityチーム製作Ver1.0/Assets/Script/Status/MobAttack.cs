using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    private float attackCooldown;
    [SerializeField] private Collider attackCollider;
    [SerializeField] private Collider ClawAttackCollider;

    private MobStatus _status;

    static public MobAttack instance;

    private bool isAttackNow = false;

    // Start is called before the first frame update
    private void Start()
    {
        _status = GetComponent<MobStatus>();

        attackCollider.enabled = false;
        ClawAttackCollider.enabled = false;

        attackCooldown = Random.Range(2, 10);
    }

    private void Awake()
    {
        instance = this;
    }

    public bool AttackNow()
    {
        return isAttackNow;
    }

    // Update is called once per frame
    public void AttackIfPossible()
    {
        if (isAttackNow) return;

        _status.GoToAttackStateIfPossible();
    }

    public void ClawAttackIfPossible()
    {
        if (isAttackNow) return;

        _status.GoToClawAttackStateIfPossible();
    }

    public void FlyIfPossible()
    {
        if (isAttackNow) return;

        _status.FlyStateIfPossible();
    }

    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }

    public void OnClawAttackRangeEnter(Collider collider)
    {
        ClawAttackIfPossible();
    }
    public void OnFlyRangeEnter(Collider collider)
    {
        FlyIfPossible();
    }

    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    public void OnClawAttackStart()
    {
        ClawAttackCollider.enabled = true;
    }

    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
        attackCooldown = Random.Range(2, 10);
    }

    public void OnClawAttackFinished()
    {
        ClawAttackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
        attackCooldown = Random.Range(2, 10);
    }

    public void attackColliderFalse()
    {
        attackCollider.enabled = false;
        ClawAttackCollider.enabled = false;
    }

    public void OnAttackNow ()
    {
        isAttackNow = true;
    }

    public void OnAttackEnd()
    {
        isAttackNow = false;
    }

    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>();
        if(null == targetMob) return;

        targetMob.Damage(1);
    }

    public void OnHitAttack2(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>();
        if (null == targetMob) return;

        targetMob.Damage(1);
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GotoNormalStateIfPossible();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatus : MonoBehaviour
{
    protected enum StateEnum
    { 
        Normal,
        Attack,
        Die
    }

    public bool IsMovable => StateEnum.Normal == _state;

    public bool IsAttackable => StateEnum.Normal == _state;

    [SerializeField] public float lifeMax = 10;
    private float _life = 0;
    public float _lifeRate => _life / lifeMax;
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _life = lifeMax;
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void OnDie()
    {
        if(lifeMax == 0)
        {
            _state = StateEnum.Die;
        }
    }

    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;

        if (_life > 0) 
        {
            OnDmg();
            return;
        }

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");
        OnDie();
    }

    protected virtual void OnDmg() {}

    public void GoToAttackStateIfPossible()
    {
        if(!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    public void GoToClawAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack2");
    }

    public void FlyStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Fly");
    }

    public void GotoNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;
        _state = StateEnum.Normal;
    }
}
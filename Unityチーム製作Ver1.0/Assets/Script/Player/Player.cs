using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 3.0f;

    [SerializeField] private float AttackWaitMax = 60;

    private float AttackWait = 0;

    private int AttackCombo = 0;

    private CharacterController _CharacterControler;

    static public Player instance;

    private PlayerStatus _status;
    private MobAttack _attack;

    public GameObject particle;

    public GameObject _object;

    public ParticleSystem p;

    private Transform _transform;
    private Vector3 _moveVelocity;
    private bool AvoidKey = false;
    private bool IsAnimationEnd = false;
    private bool IsCanAttack = true;
    private int MoveType = 0;

    void Start()
    {
        _CharacterControler = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
        p = _object.GetComponent<ParticleSystem>();

        p.Stop();
    }

    void Update()
    {
        //Debug.Log(_CharacterControler.isGrounded ? "地上にいます" : "空中です");

        if (IsAnimationEnd)
        {
            AttackCombo = 0;
            AttackWait--;
            if (AttackWait <= 0)
            {
                AttackWait = 0;
                animator.ResetTrigger("Attack");
                IsCanAttack = true;
                IsAnimationEnd = false;
            }
        }

        if (IsCanAttack && !AvoidKey && !_status.DamageFlg())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                IsCanAttack = false;
                ComboCheck();
                animator.SetTrigger("Attack");
                p.Play();
            }
        }

        if (!AvoidKey)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Avoid");
                OnComboReset();
                _status.DamageFlgFalse();
                p.Stop();
            }
        }
        _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveType = 2;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            MoveType = 1;
        }

        if (_status.DamageFlg())
        {
            animator.ResetTrigger("Avoid");
            animator.ResetTrigger("Attack");

            AttackCombo = 0;
            AttackWait = 0;
            IsCanAttack = true;
            AvoidKey = false;
            MoveType = 1;
            _attack.attackColliderFalse();
            p.Stop();
        }

        if (AttackWait > 0 || _status.DamageFlg() || _status.DieCheak())
        {
            _moveVelocity.x = 0;
            _moveVelocity.z = 0;
        }

        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * _moveVelocity.z + Camera.main.transform.right * _moveVelocity.x;

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        _CharacterControler.Move(moveForward * Time.deltaTime);

        animator.SetInteger("AttackCombo", AttackCombo);

        animator.SetInteger("MoveType", MoveType);

        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0,
        _moveVelocity.z).magnitude);
    }

    public void OnAvoidStart()
    {
        AvoidKey = true;
    }

    public void OnAvoidFinished()
    {
        AvoidKey = false;
    }

    public void OnComboReset()
    {
        IsCanAttack = true;
        AttackCombo = 0;
        AttackWait = 0;
    }

    public void OnAnimationFinished()
    {
        IsAnimationEnd = true;

    }

    public void CanAttack()
    {
        IsCanAttack = true;
        p.Stop();
    }

    public void CanAttackEnd()
    {
        IsCanAttack = false;
        p.Stop();
    }

    public void OnDamageEnd()
    {
        _status.DamageFlgFalse();
    }

    void ComboCheck()
    {
        AttackWait = AttackWaitMax;

        if (AttackCombo == 0)
        {
            AttackCombo = 1;
        }
        else if (AttackCombo == 1)
        {
            AttackCombo = 2;

        }
        else if (AttackCombo == 2)
        {
            AttackCombo = 3;
        }
    }
}

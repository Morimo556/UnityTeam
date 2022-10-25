using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;
    private bool IsDie = false;

    static public EnemyStatus instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie();
        StartCoroutine(DestroyCorotine());
        _agent.isStopped = true;
    }

    public bool DeathCheck()
    {
        return IsDie;
    }

    private IEnumerator DestroyCorotine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameClear");
        //ScoreManager.instance.AddScore();

        Destroy(gameObject);
    }
}

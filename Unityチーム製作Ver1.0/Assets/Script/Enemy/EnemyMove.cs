using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(MobAttack))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private EnemyStatus _status;

    private bool IsDetect = false;
    private bool IsRotateDetect = false;

    [SerializeReference]public CollisionDetector noRotateRange;

    //// Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }

    public void Update()
    {
        if (target == null ||MobAttack.instance.AttackNow()) { return; }

        // CollosionDitecter‚É“ü‚Á‚Ä‚¢‚½‚ç‰ñ“]‚µ‚È‚¢
        if (noRotateRange.onTrigger) { return; }

        Vector3 vR = Vector3.RotateTowards(transform.forward, toTarget, 2.0f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(vR);

    }

    public void OnDetectObject(Collider collider)
    {
        if (_agent.isStopped || MobAttack.instance.AttackNow()){ return; }

        if (collider.CompareTag("Player"))
        {
            _agent.destination = collider.transform.position;
        }
    }

    private Transform target = null;
    private Vector3 toTarget;

    public void EnemyRotate(Collider collider)
    {
        target = collider.transform;
        toTarget = target.position - transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_status.DeathCheck()) { return; }
        if(other.gameObject.name == "AttackHitDetector")
        {
            _status.Damage(1);
        }
    }
}

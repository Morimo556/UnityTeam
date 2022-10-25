using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AttackHitDetector")
        {
            EnemyStatus.instance.Damage(1);
        }
    }
}

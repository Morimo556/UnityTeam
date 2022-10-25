using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MobStatus
{
    private bool IsDmg = false;
    private bool IsDie = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDie()
    {
        base.OnDie();
        IsDie = true;
        StartCoroutine(DestroyCorotine());
    }

    public bool DieCheak()
    {
        return IsDie;
    }

    protected override void OnDmg()
    {
        base.OnDmg();
        IsDmg = true;
        _animator.SetTrigger("Damage");
    }

    public bool DamageFlg()
    {
        return IsDmg;
    }

    public bool DamageFlgFalse()
    {
        IsDmg = false;
        return IsDmg;
    }

    private IEnumerator DestroyCorotine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);

        SceneManager.LoadScene("GameOver");
    }
}

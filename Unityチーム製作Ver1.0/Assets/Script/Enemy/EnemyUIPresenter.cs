using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Player))]
public class EnemyUIPresenter : MonoBehaviour
{
    EnemyStatus _enemy;
    [SerializeField] private Slider PurpleGage;
    [SerializeField] private Slider YellowGage;

    private float DecreaseSpeed;

    private bool DecreaseFlg = false;

    private float WaitTime;

    private float DecreaseDamage = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = GameObject.Find("Red");
        if (enemy == null) { return; }

        _enemy = enemy.GetComponent<EnemyStatus>();

        PurpleGage.value = 1;

        YellowGage.value = PurpleGage.value;

        WaitTime = 0;

        DecreaseSpeed = 0.1f;

    }
    // Update is called once per frame
    private void Update()
    {
        PurpleGage.value = _enemy._lifeRate;
        if (PurpleGage.value != YellowGage.value)
        {
            DecreaseFlg = true;
            if (DecreaseFlg)
            {
                WaitTime += Time.deltaTime;
                if (WaitTime > 2.0f)
                {
                    ReceiveDamage();
                }
            }
        }
    }
    //public void a()
    //{
    //    if(!DecreaseFlg)
    //    {
    //        StartCoroutine("RedGage");
    //    }
    //}

    private void ReceiveDamage()
    {
        YellowGage.value -= DecreaseDamage * Time.deltaTime;
        if (YellowGage.value <= PurpleGage.value)
        {
            YellowGage.value = PurpleGage.value;
            WaitTime = 0;
            DecreaseFlg = false;
        }
    }
   

}

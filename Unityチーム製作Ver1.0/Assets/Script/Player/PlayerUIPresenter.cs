using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Player))]
public class PlayerUIPresenter : MonoBehaviour
{
    PlayerStatus _player;
    [SerializeField] private Slider GreenGage;
    [SerializeField] private Slider RedGage;

    private float DecreaseSpeed;

    private bool DecreaseFlg = false;

    private float WaitTime;

    private float DecreaseDamage = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null) { return; }

        _player = player.GetComponent<PlayerStatus>();

        GreenGage.value = 1;

        RedGage.value = GreenGage.value;

        WaitTime = 0;

        DecreaseSpeed = 0.1f;

    }
    // Update is called once per frame
    private void Update()
    {
        GreenGage.value = _player._lifeRate;
        if (GreenGage.value != RedGage.value)
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
        RedGage.value -= DecreaseDamage * Time.deltaTime;
        if (RedGage.value <= GreenGage.value)
        {
            RedGage.value = GreenGage.value;
            WaitTime = 0;
            DecreaseFlg = false;
        }
    }
   

}

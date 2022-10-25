using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour 
{
    [SerializeField] Vector3 ObjectTrans;

    public GameObject player;

    private Vector3 offset; 

    private Vector3 setPosition;

    void Start () 
    {
        offset = transform.position + player.transform.position;
    }

    void Update () 
    {
        if(player == null)
        {
            Destroy(gameObject);
            return;
        }

        setPosition.x = ObjectTrans.x + player.transform.position.x;

        setPosition.y = ObjectTrans.y + player.transform.position.y;

        setPosition.z = ObjectTrans.z + player.transform.position.z;

        transform.position = setPosition;
    }
}

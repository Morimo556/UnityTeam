using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public GameObject aimObject;

    private Vector3 offset;

    [SerializeField] private float SensiX = 3.0f;
    [SerializeField] private float SensiY = 3.0f;

    // プレイヤーとカメラとの距離
    [SerializeField] private float toPlayerDist = 8;

    // ラジアン:半径1の円があった時、円周１になるときの角度
    // デグリー：一周360度
    private float degCamRotY = 0; // カメラのY軸の回転角度
    private float degCamRotX = 0; // カメラのX軸の回転角度

    void Start () 
    {
        offset = transform.position + player.transform.position;

        transform.position += offset;

        Cursor.visible = false;
    }

    void LateUpdate() 
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (player == null)
        {
            return;
        }

　　　　　//＊３fのところを変数にしたら感動設定できる

　　　　　//*AimControllerと一緒の値にすること

        degCamRotY += Input.GetAxis("Mouse X") * SensiY;
        degCamRotX -= Input.GetAxis("Mouse Y") * SensiX;

        if(degCamRotX >= 80)
        {
            degCamRotX = 80;
        }

        if(degCamRotX <= 10)
        {
            degCamRotX = 10;
        }

        Vector3 setPosition = new Vector3(0,0,0); // 原点の位置にカメラを設置する
        setPosition.z -= toPlayerDist; // カメラとの距離分後ろに下げる
        setPosition = Quaternion.Euler(degCamRotX, degCamRotY, 0) * setPosition; //後ろに下げたカメラを回転させる
        setPosition += player.transform.position; // 最後にカメラをプレイヤーの位置に持っていく

        transform.position = setPosition;
        transform.LookAt(aimObject.transform); // 視点用オブジェクトを見る
    }
}

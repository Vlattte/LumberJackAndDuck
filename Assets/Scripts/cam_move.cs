using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_move : MonoBehaviour
{
    public GameObject player;
    public float zPos;

    [SerializeField]
    public float rightLimit;
    [SerializeField]
    public float leftLimit;
    [SerializeField]
    public float upperLimit;
    [SerializeField]
    public float bottomLimit;

    //Камера перемещается вместе с игроком
    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zPos);

        transform.position = new Vector3
            (
                Mathf.Clamp(player.transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(player.transform.position.y, bottomLimit, upperLimit),
                zPos
            );
    }


}


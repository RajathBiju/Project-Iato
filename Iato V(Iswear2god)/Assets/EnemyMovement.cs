using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    bool FollowPlayer = false;
    bool RotationApplied = false;

    public GameObject Player;
    public float Speed;

    float AngleBetweenPlayer;

    private void OnTriggerEnter(Collider other)
    {
        //Follow player is true here;

        if(other.tag == "Player")
        {
            Player = other.gameObject;
            FollowPlayer = true;
            Debug.Log("Player found");

            AngleBetweenPlayer = Vector3.SignedAngle(transform.forward.normalized, Vector3.Normalize(Player.transform.position - transform.position), Vector3.up);

            Debug.Log(AngleBetweenPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FollowPlayer = false;
            Debug.Log("Player lost");
        }
    }

    private void Update()
    {
        if (FollowPlayer)
        {
            //transform.Translate(transform.position.normalized * Speed, Space.Self);
            AngleBetweenPlayer = Vector3.SignedAngle(transform.forward.normalized, Vector3.Normalize(Player.transform.position - transform.position), Vector3.up);

            //TODO
            transform.DORotate(new Vector3(0, (transform.eulerAngles.y - AngleBetweenPlayer > 0) ? 180-AngleBetweenPlayer : AngleBetweenPlayer, 0), 1).OnComplete(() => RotationApplied = true);
        }


        Debug.DrawRay(transform.position , Vector3.Normalize(Player.transform.position - transform.position) * 100, Color.red);
        Debug.Log(Vector3.SignedAngle(transform.forward.normalized, Vector3.Normalize(Player.transform.position - transform.position), Vector3.up));
    }
}

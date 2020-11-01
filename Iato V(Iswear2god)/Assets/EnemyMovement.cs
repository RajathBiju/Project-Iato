﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    bool FollowPlayer = false;
    bool RotationApplied = true;

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

            RotationApplied = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FollowPlayer = false;
            RotationApplied = false;
            Debug.Log("Player lost");
        }
    }

    private void Update()
    {
        if (FollowPlayer)
        {
            //foreach(Transform n in Player.transform)

            //transform.Translate(transform.forward.normalized * Speed, Space.Self);
            if(Mathf.Abs(Vector3.Distance(Player.transform.position, transform.position)) > 1) transform.position += transform.forward * Speed;
            AngleBetweenPlayer = Vector3.SignedAngle(transform.forward.normalized, Vector3.Normalize(Player.transform.position - transform.position), Vector3.up);

            //TODO
            if (!RotationApplied)
            {
                Debug.Log(AngleBetweenPlayer);
                transform.DORotate(new Vector3(0, (transform.eulerAngles.y >= 0) ? transform.eulerAngles.y + AngleBetweenPlayer : transform.eulerAngles.y - AngleBetweenPlayer, 0), 3);//.OnComplete(() => RotationApplied = false);
            }
            //RotationApplied = true;
        }


        Debug.DrawRay(transform.position , Vector3.Normalize(Player.transform.position - transform.position) * 100, Color.red);
        //Debug.Log(Vector3.SignedAngle(transform.forward.normalized, Vector3.Normalize(Player.transform.position - transform.position), Vector3.up));
    }
}

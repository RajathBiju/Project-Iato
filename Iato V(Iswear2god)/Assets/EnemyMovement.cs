using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    public bool FollowPlayer = false;
    bool RotationApplied = true;
    public bool InAttackRange = false;
    public bool InTurnRange = false;

    Animator Animator;

    public GameObject Player;
    public float Speed;

    float WalkSpeed;
    float SpeedSmoothVelocity = 0;
    float SpeedSmoothTime = 0.1f;

    float AttackSpeed;
    float AttackSmoothVelocity = 0;
    float AttackSmoothTime = 0.1f;

    public float AngleBetweenPlayer;

    void Awake() { Animator = GetComponent<Animator>(); }

    private void OnTriggerEnter(Collider other)
    {
        //Follow player is true here;

        if(other.tag == "Player")
        {
            Player = other.gameObject;
            FollowPlayer = true;
            //Debug.Log("Player found");

            RotationApplied = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("What Left - " + other.name);
            FollowPlayer = false;
            RotationApplied = false;
            InAttackRange = false;
            //Debug.Log("Player lost");
        }
    }

    private void FixedUpdate()
    {
        if (FollowPlayer)
        {
            if (Mathf.Abs(Vector3.Distance(Player.transform.position, transform.position)) > 1f)
            {
                transform.position += transform.forward * Speed;
                InAttackRange = false;
            }
            else
            {
                Debug.Log("In Attack Range");
                InAttackRange = true;
            }

            if (!InAttackRange)
            {
                SetWalkRunSpeed(1, 0);
            }
            else
            {
                SetWalkRunSpeed(0, 1);
            }

            AngleBetweenPlayer = Vector3.SignedAngle(transform.forward.normalized, Vector3.Normalize(Player.transform.position - transform.position), Vector3.up);
            
            if (!RotationApplied)
            {
                transform.DORotate(new Vector3(0, (transform.eulerAngles.y >= 0) ? transform.eulerAngles.y + AngleBetweenPlayer : transform.eulerAngles.y - AngleBetweenPlayer, 0), 3);//.OnComplete(() => RotationApplied = false);
            }
        }
        else
        {
            SetWalkRunSpeed(0, 0);
        }


        //Debug.DrawRay(transform.position , Vector3.Normalize(Player.transform.position - transform.position) * 100, Color.red);
        //Debug.Log(Vector3.SignedAngle(transform.forward.normalized, Vector3.Normalize(Player.transform.position - transform.position), Vector3.up));
    }

    private void SetWalkRunSpeed(float WalkSpeed, float RunSpeed)
    {
        WalkSpeed = Mathf.SmoothDampAngle(WalkSpeed, WalkSpeed, ref SpeedSmoothVelocity, SpeedSmoothTime);
        Animator.SetFloat("MovementBlend", WalkSpeed);

        AttackSpeed = Mathf.SmoothDampAngle(AttackSpeed, RunSpeed, ref AttackSmoothVelocity, AttackSmoothTime);
        Animator.SetFloat("AttackBlend", AttackSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject Cube1, Cube2;
    public EnemyMovement Movement;

    public GameObject PunchHeavy, PunchMid, PunchLight;

    /*w2private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AttackNode")
        {
            Debug.Log("Hit");
        }
    }*/

    private void Awake()
    {
        Movement = GetComponent<EnemyMovement>();
    }

    public void HITHand1()
    {
        if (Movement.InAttackRange && (Movement.AngleBetweenPlayer < 15f && Movement.AngleBetweenPlayer > -15f))
        {
            Debug.Log(Movement.AngleBetweenPlayer);
            Destroy(Instantiate(PunchMid, Cube1.transform.position, transform.rotation), 1.5f);
        }
    }

    public void HITHand2()
    {
        if (Movement.InAttackRange && (Movement.AngleBetweenPlayer < 15f && Movement.AngleBetweenPlayer > -15f))
        {
            Debug.Log(Movement.AngleBetweenPlayer);
            Destroy(Instantiate(PunchMid, Cube2.transform.position, transform.rotation), 1.5f);
        }
    }
}

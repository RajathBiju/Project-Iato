using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject Cube1, Cube2;
    public EnemyMovement Movement;

<<<<<<< HEAD
=======
    public GameObject PunchHeavy, PunchMid, PunchLight;

>>>>>>> c9b8d7fbc2253a6f999257fbe72a9c1aced7da72
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

<<<<<<< HEAD
    public void HIT()
    {
        if(Movement.InAttackRange)
        Debug.Log("Hit");
=======
    public void HITHand1()
    {
        if (Movement.InAttackRange)
        {
            Debug.Log("Hit");
            Destroy(Instantiate(PunchMid, Cube1.transform.position, transform.rotation), 1.5f);
        }
    }

    public void HITHand2()
    {
        if (Movement.InAttackRange)
        {
            Debug.Log("Hit");
            Destroy(Instantiate(PunchMid, Cube2.transform.position, transform.rotation), 1.5f);
        }
>>>>>>> c9b8d7fbc2253a6f999257fbe72a9c1aced7da72
    }
}

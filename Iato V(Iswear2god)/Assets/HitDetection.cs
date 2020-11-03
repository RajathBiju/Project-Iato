using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject Cube1, Cube2;
    public EnemyMovement Movement;

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

    public void HIT()
    {
        if(Movement.InAttackRange)
        Debug.Log("Hit");
    }
}

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

    public int N = 0;
    private void Awake()
    {
        Movement = GetComponent<EnemyMovement>();

        bool reversing = false;
        string str = "";
        int i = 1;
        while(i <= N*2-1 && i > 0)
        {
            if (!reversing)
            {
                str = "";

                for (int j = 0; j < i; j++) { str += "*"; }
                Debug.Log(str);

                if (i >= N) reversing = true;
                else i++;
            }
            else
            {
                str = "";
                i--;
                if (i > 0)
                {
                    for (int j = 0; j < i; j++) { str += "*"; }
                    Debug.Log(str);
                }
            }
        }
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

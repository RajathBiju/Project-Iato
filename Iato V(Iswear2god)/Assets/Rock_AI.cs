using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_AI : MonoBehaviour
{
    bool RockLaunched = false;
    Vector3 PlayerPosition = new Vector3();

    public GameObject ParabolaRoot;

    ParabolaController P_Controller;

    private void Awake()
    {
        P_Controller = GetComponent<ParabolaController>();
        P_Controller.ParabolaRoot = GameObject.FindGameObjectWithTag("Parabola Root");
        P_Controller.Autostart = false;
        P_Controller.Animation = false;
    }

    public void LaunchRock()
    {
        P_Controller.Autostart = true;
        P_Controller.Animation = true;
        P_Controller.Speed = 30;
        P_Controller.StartLaunch();
    }
    
    public void AnimOver()
    {
        Debug.Log("Anim Over");
        Destroy(gameObject, 0.8f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LaunchRock();
        }

        //if(RockLaunched)
        //{
        //    RockTossAnim += Time.deltaTime;
        //    RockTossAnim = RockTossAnim % 2;

        //    transform.position = MathParabola.Parabola(transform.position, PlayerPosition, 2f, RockTossAnim / 2f);
        //}
    }
}

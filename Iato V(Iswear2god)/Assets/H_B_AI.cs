using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_B_AI : MonoBehaviour
{
    public GameObject Rock;
    public GameObject RockFromHellRock;
    GameObject RockBeingTossed;

    public Transform RockToInstantiateTo;
    public GameObject RockFromHellRockImerger;

    public GameObject RockFromHellRockParticleFX;

    public float RocksAirTime;

    Rigidbody RB;

    Vector3 g = new Vector3(0, -9.807f, 0);

    public Transform ParabolaTransform;

    void InstantiateRock()
    {
        RockBeingTossed = Instantiate(Rock, RockToInstantiateTo.transform.position, RockToInstantiateTo.transform.rotation, RockToInstantiateTo);
        RockBeingTossed.transform.position = new Vector3(0, 0, 0);

        Rock_AI RockAI = RockBeingTossed.GetComponent<Rock_AI>();
        RockAI.ParabolaRoot = ParabolaTransform.gameObject;

        RB = RockBeingTossed.GetComponent<Rigidbody>();
    }

    float RockTossAnim;

    void ThrowRock()
    {
        RockBeingTossed.transform.parent = null;

        Rock_AI RockAI = RockBeingTossed.GetComponent<Rock_AI>();
        
        RockAI.LaunchRock();
    }

    Vector3 Dir = new Vector3();
    void RocksFromHell()
    {
        StartCoroutine(RockMaker());
    }

    List<Transform> RocksTransfrom = new List<Transform>();
    List<Transform> ParticleFXTransfrom = new List<Transform>();
    IEnumerator RockMaker()
    {
        int RocksFormed = 0;

        while(RocksFormed < 12)
        {
            if(RocksFormed != 0) RocksTransfrom.Add(Instantiate(RockFromHellRock, new Vector3(RockFromHellRockImerger.transform.position.x, transform.forward.y, RockFromHellRockImerger.transform.position.z) + transform.forward * (RocksFormed-1) * 2 - Vector3.up * 10, Quaternion.identity).transform);
            ParticleFXTransfrom.Add(Instantiate(RockFromHellRockParticleFX, new Vector3(RockFromHellRockImerger.transform.position.x, 0, RockFromHellRockImerger.transform.position.z) + transform.forward * RocksFormed * 2, Quaternion.identity).transform);
            Destroy(ParticleFXTransfrom[RocksFormed].gameObject, 5);
            RocksFormed++;
            yield return new WaitForSeconds(0.2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFromHellScript : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(RockAscend());
        StartCoroutine(RockDescend());

        Destroy(gameObject, 10);
    }

    IEnumerator RockAscend()
    {
        yield return new WaitForSeconds(3);

        while(transform.position.y < 1f)
        {
            transform.position += new Vector3(0, 1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator RockDescend()
    {
        yield return new WaitForSeconds(7);

        while (transform.position.y > -5f)
        {
            transform.position -= new Vector3(0, 1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

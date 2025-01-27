using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBurble : MonoBehaviour
{
    public GameObject generador;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(4);
        generador.SetActive(true);
    }
}

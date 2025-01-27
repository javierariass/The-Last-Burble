using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbleDirection : MonoBehaviour
{
    public Transform transform;

    // Update is called once per frame
    void Update()
    {
        Vector3.MoveTowards(transform.position, transform.position, 2 * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShoot : MonoBehaviour
{
    public GameObject Rocket;

    public void Shoot()
    {
        Instantiate(Rocket, transform.position, transform.rotation);
    }
}

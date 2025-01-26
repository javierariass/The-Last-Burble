using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPanel : MonoBehaviour
{
    public GameObject player;
    public GameObject Planet;

    public void Active()
    {
        Planet.SetActive(true);
        Planet.transform.position = player.transform.position;
        
    }
}

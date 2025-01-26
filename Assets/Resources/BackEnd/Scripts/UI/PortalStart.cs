using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalStart : MonoBehaviour
{
    public GameObject panel;
    public GameObject Background;
    public MovMenu player;
    public GameObject burble;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        burble.SetActive(false);
        Background.SetActive(false);
        panel.SetActive(true);
        player.Stop = true;
    }
}

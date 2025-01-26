using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbleDialog : MonoBehaviour
{
    public GameObject Dialog;
    private bool Active = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !Active)
        {
            Dialog.SetActive(true);
            Active = true;
        }
    }
}

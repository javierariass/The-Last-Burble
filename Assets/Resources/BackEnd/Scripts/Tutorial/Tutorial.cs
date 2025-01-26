using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Instruction;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.inCinematic = true;
            Instruction.GetComponent<dialogScript>().player = player;
            Instruction.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<AudioSource>().Stop();
            player.animator.SetBool("inMove", false);
        }
    }
}

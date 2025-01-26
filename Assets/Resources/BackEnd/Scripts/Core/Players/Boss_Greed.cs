using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Greed : MonoBehaviour
{
    public GameObject Dialogo;
    public BoxCollider2D collider2D;
    public AudioSource Audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Audio.Play();
            Dialogo.GetComponent<dialogScript>().collider = collider2D;
            Dialogo.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}

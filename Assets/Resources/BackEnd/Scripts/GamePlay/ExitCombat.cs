using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCombat : MonoBehaviour
{
    public Enemy enemy;
    public GameObject Droped;
    public TextMeshProUGUI Experiencia, Dropeado;
    public GameObject particlePrefab;
    public Vector3 Pos;
    public AudioClip Win, Lose;
    public AudioSource Audio;
    public string Life;
    public Player player;
    public void GenerateDatos()
    {
        Audio.clip = Win;
        Audio.Play();
        transform.localScale = Vector3.one;
        Dropeado.text = Droped != null ? "Item dropeado: " + Droped.GetComponent<Item>().name : "";
        Experiencia.text = "Burbujas obtenidas: " + enemy.expDropped;
    }

    public void GeneratePerdidas()
    {
        Audio.clip = Lose;
        Audio.Play();
        transform.localScale = Vector3.one;
        Experiencia.text = "Has Muerto...";
        Dropeado.text = "Reapariciones restantes: " + Life;
        player.desLevelUp();
    }
    public void ExitZone()
    {
        transform.localScale = Vector3.zero;
        Instantiate(particlePrefab, Pos, Quaternion.identity);
        if (!GameObject.FindGameObjectWithTag("Greed") && int.Parse(Life) > 0) SceneManager.LoadScene(2);
        if(int.Parse(Life) == 0) SceneManager.LoadScene(0);
    }

    
}

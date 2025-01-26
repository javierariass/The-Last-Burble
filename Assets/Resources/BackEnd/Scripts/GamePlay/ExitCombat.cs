using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ExitCombat : MonoBehaviour
{
    public Enemy enemy;
    public GameObject Droped;
    public TextMeshProUGUI Experiencia, Dropeado;
    public GameObject particlePrefab;
    public Vector3 Pos;

    public void GenerateDatos()
    {
        transform.localScale = Vector3.one;
        Dropeado.text = Droped != null ? "Item dropeado: " + Droped.GetComponent<Item>().name : "";
        Experiencia.text = "Burbujas obtenidas: " + enemy.expDropped;
    }

    public void ExitZone()
    {
        transform.localScale = Vector3.zero;
        Instantiate(particlePrefab, Pos, Quaternion.identity);
    }
}

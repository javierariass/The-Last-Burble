using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasladePlayer : MonoBehaviour
{
    public PolygonCollider2D Collider;
    public Transform Position;
    public CinemachineConfiner Confiner;
    public GameObject Panel;
    private void Start()
    {
        Confiner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineConfiner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Panel.GetComponent<PanelTransition>().Collider = Collider;
            Panel.GetComponent<PanelTransition>().Position = Position;
            Panel.GetComponent<PanelTransition>().Confiner = Confiner;
            Panel.GetComponent<PanelTransition>().player = collision.gameObject;
            Panel.SetActive(true);
            
        }
    }
}

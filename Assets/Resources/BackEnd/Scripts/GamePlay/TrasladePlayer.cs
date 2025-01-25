using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasladePlayer : MonoBehaviour
{
    public PolygonCollider2D Collider;
    public Transform Position;
    public CinemachineConfiner Confiner;

    private void Start()
    {
        Confiner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineConfiner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = Position.position;
            Confiner.m_BoundingShape2D = Collider;
            Confiner.InvalidatePathCache();
        }
    }
}

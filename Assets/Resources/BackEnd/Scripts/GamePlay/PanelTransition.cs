using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PanelTransition : MonoBehaviour
{
    public PolygonCollider2D Collider;
    public Transform Position;
    public CinemachineConfiner Confiner;
    public GameObject player;

    public void Moving()
    {
       player.transform.position = Position.position;
       Confiner.m_BoundingShape2D = Collider;
       Confiner.InvalidatePathCache();       
    }

    public void EndTransitionn()
    {
        gameObject.SetActive(false);
    }
}

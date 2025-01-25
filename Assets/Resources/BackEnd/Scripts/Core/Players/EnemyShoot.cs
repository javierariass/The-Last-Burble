using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int Damage = 0;
    private Player player;
    private Vector3 Direction;

    public float SpeedMove = 1.5f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Direction = player.transform.position;
        Destroy(gameObject,3);
    }

    void Update()
    {
        //Movement
        transform.position = Vector2.MoveTowards(transform.position, Direction, SpeedMove * Time.deltaTime);
    }

    //Collision player damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.takeDamage(Damage);
        }
    }
}

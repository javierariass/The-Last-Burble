using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int Damage = 0;
    private Player player;
    public float SpeedMove = 1.5f;
    private Vector3 Position;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Position = player.transform.position;
        Destroy(gameObject,4f);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Position, SpeedMove * Time.deltaTime);
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

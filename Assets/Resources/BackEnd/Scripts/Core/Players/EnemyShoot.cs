using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int Damage = 0;
    private GameObject player;
    private Vector3 Direction;

    public float SpeedMove = 1.5f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("object");
        Direction = player.transform.position;
        Destroy(gameObject,3);
    }

    void Update()
    {
        //Movement
        transform.position = Vector2.MoveTowards(transform.position, Direction, SpeedMove * Time.deltaTime);
        if (!GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>().inDefense) Destroy(gameObject);
        if (transform.position == Direction) Destroy(gameObject);
    }

    //Collision player damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("object") && GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>().inDefense)
        {
            collision.gameObject.GetComponent<PlayerCorePerson>().player.takeDamage(Damage);
        }
    }
}

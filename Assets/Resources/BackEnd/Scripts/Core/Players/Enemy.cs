using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name;
    public int Life = 5;
    public int expDropped;
    public int DamageMin = 2;
    public float DamageMax = 3;
    public float SpeedMove = 2f;
    private EnemyShoot shoot;
    public GameObject[] PointRutine;
    private int PointIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        //shoot.Damage = Random.Range(DamageMin, DamageMax);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        transform.position = Vector2.MoveTowards(transform.position, PointRutine[PointIndex].transform.position, SpeedMove * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Change Direction
        if (collision.gameObject.CompareTag("Rutine"))
        {
            int i = PointIndex;
            while (i == PointIndex)
            {
                PointIndex = Random.Range(0, PointRutine.Length);
            }          
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Start Battle");
            StartBattle();
        }
    }


    //Combat function
    private void StartBattle()
    {
        Debug.Log("Battle");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    public string Name;
    public int Life;
    public int LifeMax;
    public int expDropped;
    public int DamageMin = 2;
    public float DamageMax = 3;
    public float SpeedMove = 2f;
    private EnemyShoot shoot;
    public GameObject[] PointRutine;
    public GameObject[] droppedItem;
    private int PointIndex = 0;
    public GameObject Person;
    public CinemachineVirtualCamera cameraVirtual;
    public GameObject Camera;
    public PolygonCollider2D confiner;
    private BattleController Bc;
    public int attackDuration = 5;


    // Start is called before the first frame update
    void Start()
    {
        //shoot.Damage = Random.Range(DamageMin, DamageMax);
        Camera = GameObject.FindGameObjectWithTag("Camera");
        cameraVirtual = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
        Person = GameObject.FindGameObjectWithTag("object");
        confiner = GameObject.FindGameObjectWithTag("ConfinerBattle").GetComponent<PolygonCollider2D>();
        Bc = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Life = LifeMax;
    }


    // Update is called once per frame
    void Update()
    {
        //Movement
        if(!Person.GetComponent<PlayerCorePerson>().player.inCinematic) transform.position = Vector2.MoveTowards(transform.position, PointRutine[PointIndex].transform.position, SpeedMove * Time.deltaTime);
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
            
            StartBattle(collision.gameObject.GetComponent<Player>());
        }
    }

    public void restarLife(int restar)
    {
        Life -= restar;
        if (Life <= 0)
        {
            Life = 0;
            cameraVirtual.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            cameraVirtual.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameObject.FindGameObjectWithTag("StageConfiner").GetComponent<PolygonCollider2D>();
            cameraVirtual.GetComponent<CinemachineConfiner>().InvalidatePathCache();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inCinematic = false;
            Bc.Combat.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().takeExperience(expDropped);

            //Dropeo
            if (droppedItem.Length != 0)
            {
                int Probabilidad = Random.Range(0, 11);
                if (Probabilidad > 6)
                {
                    Probabilidad = Random.Range(0, droppedItem.Length);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().addItem(droppedItem[Probabilidad]);
                }
            }

            //Eliminar objeto
            Destroy(gameObject);
        }
    }


    //Combat function
    private void StartBattle(Player player)
    {       
        player.PanelCombat.SetActive(true);
        Person.GetComponent<PlayerCorePerson>().player.inCinematic = true;
        Bc.enemy = gameObject.GetComponent<Enemy>();    
        
    }

    public void ChangePosition()
    {
        cameraVirtual.Follow = null;
        cameraVirtual.LookAt = Person.transform;
        cameraVirtual.GetComponent<CinemachineConfiner>().m_BoundingShape2D = confiner;
        cameraVirtual.GetComponent<CinemachineConfiner>().InvalidatePathCache();
        Bc.InitBatle();
    }
}

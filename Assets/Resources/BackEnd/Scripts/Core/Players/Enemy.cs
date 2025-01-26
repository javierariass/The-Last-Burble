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
    public GameObject particlePrefab;
    public PolygonCollider2D confiner;
    private BattleController Bc;
    public int attackDuration = 5;
    public GameObject uiUser;
    private ExitCombat HistCombat;
    private AudioSource Audio;
    private Sounds sounds;
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
        uiUser = GameObject.FindGameObjectWithTag("uiUser");
        HistCombat = GameObject.FindGameObjectWithTag("InfoCombat").GetComponent<ExitCombat>();
        gameObject.AddComponent<AudioSource>();
        Audio = GetComponent<AudioSource>();
        sounds = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<Sounds>();
    }


    // Update is called once per frame
    void Update()
    {
        //Movement
        if (!Person.GetComponent<PlayerCorePerson>().player.inCinematic) transform.position = Vector2.MoveTowards(transform.position, PointRutine[PointIndex].transform.position, SpeedMove * Time.deltaTime);
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
            collision.gameObject.GetComponent<Player>().animator.SetBool("inMove", false);
            collision.gameObject.GetComponent<AudioSource>().Stop();
            StartBattle(collision.gameObject.GetComponent<Player>());
        }
    }

    public void restarLife(int restar)
    {
        Audio.clip = sounds.EnemytakeDamage;
        Audio.loop = false;
        Audio.Play();
        Life -= restar;
        if (Life <= 0)
        {
            Life = 0;
            cameraVirtual.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            cameraVirtual.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameObject.FindGameObjectWithTag("StageConfiner").GetComponent<PolygonCollider2D>();
            cameraVirtual.GetComponent<CinemachineConfiner>().InvalidatePathCache();
            sounds.Audio.clip = sounds.SoundStage;
            sounds.Audio.Play();
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
                    HistCombat.Droped = droppedItem[Probabilidad];
                }
            }

            //Eliminar objeto
            GameObject player = GameObject.FindWithTag("Player");
            HistCombat.Pos = player.transform.position;
            HistCombat.particlePrefab = particlePrefab;
            HistCombat.enemy = this;
            HistCombat.GenerateDatos();
            uiUser.SetActive(true);
            Destroy(gameObject);


        }
    }


    //Combat function
    private void StartBattle(Player player)
    {

        if (gameObject.CompareTag("Greed")) sounds.Audio.clip = sounds.Boss;
        else sounds.Audio.clip = sounds.Battle;
        sounds.Audio.Play();
        player.PanelCombat.SetActive(true);
        Person.GetComponent<PlayerCorePerson>().player.inCinematic = true;
        Bc.enemy = gameObject.GetComponent<Enemy>();
        uiUser.SetActive(false);


    }

    public void ChangePosition()
    {
        cameraVirtual.Follow = null;
        cameraVirtual.LookAt = Person.transform;
        cameraVirtual.GetComponent<CinemachineConfiner>().m_BoundingShape2D = confiner;
        cameraVirtual.GetComponent<CinemachineConfiner>().InvalidatePathCache();
        if (Bc.TutorialComplete)
        {
            Bc.InitBatle();
        }
        else
        {
            Bc.InitTutorial();
        }
    }


}

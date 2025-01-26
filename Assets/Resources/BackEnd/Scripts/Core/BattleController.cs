using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public bool inDefense = false;
    public GameObject CombatSystem,DefenseZone,Combat,Person,SliderHit,SpawnObject,ButtonBack;
    public GameObject[] SpawnPoint;
    public Enemy enemy;
    public SpriteRenderer Enemy;
    private Player player;
    private bool InBatle = false;
    public bool CombatEnabled = false;
    public TextMeshProUGUI EnemyName, PlayerLife, EnemyLife;
    private int Counterlife;
    public Transform Inventory;
    public GameObject uiUser;

    public void atacking(int damage)
    {
        int atack = 0;
        if ((damage >= 0 && damage <= 10) || (damage <= 100 && damage >= 90))
        {
            atack = 0;
        }
        else if ((damage > 10 && damage <= 20) || (damage < 90 && damage >= 80))
        {
            atack = (int)(player.damage * 0.2f);
        }
        else if ((damage > 20 && damage <= 30) || (damage < 70 && damage >= 80))
        {
            atack = (int)(player.damage * 0.4f);
        }
        else if ((damage > 30 && damage <= 40) || (damage < 70 && damage >= 60))
        {
            atack = (int)(player.damage * 0.6f);
        }
        else if ((damage > 40 && damage <= 49) || (damage < 60 && damage > 50))
        {
            atack = (int)(player.damage * 0.8f);
        }
        else
        {
            atack = player.damage;
        }
        enemy.restarLife(atack);
        CombatEnabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Counterlife = 3;
    }

    // Update is called once per frame
    void Update()
    {
       if(InBatle) UpdateLifes();
       if(player.Life == 0)
        {
            LoseBatle();
        }
    }

    private void LoseBatle()
    {
        if (Counterlife > 0)
        {

            InBatle = false;
            inDefense = false;
            enemy.Life = enemy.LifeMax;
            int sp = Random.Range(0, SpawnPoint.Length);
            player.transform.position = SpawnPoint[sp].transform.position;
            player.desLevelUp();
            enemy.cameraVirtual.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            enemy.cameraVirtual.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameObject.FindGameObjectWithTag("StageConfiner").GetComponent<PolygonCollider2D>();
            enemy.cameraVirtual.GetComponent<CinemachineConfiner>().InvalidatePathCache();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inCinematic = false;
            Combat.SetActive(false);
            enemy = null;
            Counterlife--;
            uiUser.SetActive(true);
        }
        else SceneManager.LoadScene(0);
    }

    public void InitBatle()
    {
        if(enemy != null)
        {
            Enemy.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
            InBatle = true;
            player.inCinematic = true;
            Combat.SetActive(true);
            CombatSystem.SetActive(true);
            Person.SetActive(false);
            EnemyName.text = enemy.Name;
        }
    }

    //Inventary
    public void InitInventory()
    {
        CombatSystem.SetActive(false);
        Inventory.localScale = Vector3.one;
        ButtonBack.SetActive(true);
    }

    public void Back()
    {
        CombatSystem.SetActive(true);
        Inventory.localScale = Vector3.zero;
        ButtonBack.SetActive(false);
    }
    private void UpdateLifes()
    {
        PlayerLife.text = "Vida: " + player.Life.ToString() + "/" + player.LifeMax.ToString();
        EnemyLife.text = "Vida: " + enemy.Life.ToString() + "/" + enemy.LifeMax.ToString();
        
    }

    public void InitAttack()
    {
        CombatEnabled = true;
        CombatSystem.SetActive(false);
        SliderHit.SetActive(true);
    }

    public void InitDefense()
    {
        Person.transform.position = SpawnObject.transform.position;
        StartCoroutine(Defense());
    }

    IEnumerator Defense()
    {
        DefenseZone.SetActive(true);
        DefenseZone.GetComponent<EnemySpawnerController>().ShootSpawn = false;
        Person.SetActive(true);
        inDefense = true;
        yield return new WaitForSeconds(enemy.attackDuration);
        DefenseZone.SetActive(false);
        Person.SetActive(false);
        inDefense = false;
        InitBatle();
    }
}

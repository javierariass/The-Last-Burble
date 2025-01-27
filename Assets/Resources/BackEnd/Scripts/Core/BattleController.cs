using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public bool inDefense = false;
    public GameObject CombatSystem,DefenseZone,Combat,Person,SliderHit,SpawnObject,ButtonBack,Alert;
    public GameObject SpawnPoint;
    public Enemy enemy;
    public SpriteRenderer Enemy;
    private Player player;
    private bool InBatle = false;
    public bool CombatEnabled = false;
    public TextMeshProUGUI EnemyName, PlayerLife, EnemyLife;
    private int Counterlife;
    public Transform Inventory;
    public GameObject uiUser;
    public bool TutorialComplete = false;
    private int PasoTutorial = 0;
    public GameObject[] CombatTutorial;
    public Sounds Audios;
    public AudioSource audioSource;
    public ExitCombat UIexitCombat;
    public bool EnTexto = false;

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
            Counterlife--;
            InBatle = false;
            inDefense = false;
            enemy.Life = enemy.LifeMax;
            player.transform.position = SpawnPoint.transform.position;
            enemy.cameraVirtual.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            enemy.cameraVirtual.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameObject.FindGameObjectWithTag("StageConfiner").GetComponent<PolygonCollider2D>();
            enemy.cameraVirtual.GetComponent<CinemachineConfiner>().InvalidatePathCache();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inCinematic = false;
            Combat.SetActive(false);
            enemy = null;
            uiUser.SetActive(true);
            audioSource.clip = Audios.SoundStage;
            audioSource.Play();
            UIexitCombat.Life = Counterlife.ToString();
            UIexitCombat.GeneratePerdidas();
        }
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

        if (TutorialComplete)
        {
            SliderHit.SetActive(true);
        }
        else if(!TutorialComplete && PasoTutorial == 1)
        {
            CombatTutorial[1].SetActive(true);
            CombatTutorial[1].GetComponent<dialogScript>().CombatAction = SliderHit;
            PasoTutorial++;
        }
    }

    public void InitDefense()
    {
        Person.transform.position = SpawnObject.transform.position;
        if (TutorialComplete) StartCoroutine(DefenseAlert());
        else if(!TutorialComplete && PasoTutorial == 2)
        {
            CombatTutorial[2].SetActive(true);
            CombatTutorial[2].GetComponent<dialogScript>().bc = this;
            PasoTutorial++;
        }
    }

    public void InitTutorial()
    {
        InBatle = true;
        Person.SetActive(false);
        Combat.SetActive(true);
        CombatTutorial[0].GetComponent<dialogScript>().CombatAction = CombatSystem;
        CombatTutorial[0].SetActive(true);
        PasoTutorial++;
        EnemyName.text = enemy.Name;
    }

    public IEnumerator DefenseAlert()
    {
        Alert.SetActive(true);
        yield return new WaitForSeconds(2f);
        Alert.SetActive(false);
        StartCoroutine(Defense());
    }
    public IEnumerator Defense()
    {
        DefenseZone.SetActive(true);
        DefenseZone.GetComponent<EnemySpawnerController>().ShootSpawn = false;
        Person.SetActive(true);
        inDefense = true;
        yield return new WaitForSeconds(enemy.attackDuration);
        DefenseZone.SetActive(false);
        Person.SetActive(false);
        inDefense = false;
        if(TutorialComplete) InitBatle();
        else if(PasoTutorial == 3 && InBatle)
        {
            TutorialComplete = true;
            CombatTutorial[3].SetActive(true);
            CombatTutorial[3].GetComponent<dialogScript>().bc = this;
            CombatTutorial[3].GetComponent<dialogScript>().EndTutorial= true;
            
        }
    }
}

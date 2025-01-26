using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Transform PlayerTransform;

    public int Life;
    private float MoveX, MoveY;

    public Animator animator;
    public GameObject playerMinigame;

    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;

    //Leveling
    public int ExperienceRequired = 5;
    public int Experience = 0;
    public int Level = 1;


    //Attribute
    public int damage = 5;
    public int LifeMax = 15;
    public float SpeedMove = 1f;
    public float SpeedDef = 5f;
    public float SpeedOff = 5f;

    //Inventory
    public Casilla[] Inventory;

    //Estates
    public bool inCinematic;
    public bool isLive;
    public GameObject PanelCombat;


    //Sonidos
    private AudioSource Audio;
    public AudioClip Steps, TakeDamage, LevelUp, Health;
    private bool reproducing = false;

    private void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        Life = LifeMax;
        inCinematic = false;
        isLive = true;
        Audio = GetComponent<AudioSource>();
    }

    //Movement
    private void Update()
    {
        if (!inCinematic)
        {
            MoveX = Input.GetAxis("Horizontal") * SpeedMove * Time.deltaTime;
            MoveY = Input.GetAxis("Vertical") * SpeedMove * Time.deltaTime;
            GetComponent<CapsuleCollider2D>().enabled = true;

            if (MoveY != 0 || MoveX != 0)
            {
                animator.SetBool("inMove", true);
                Audio.clip = Steps;
                if (!reproducing)
                {
                    Audio.Play();
                    Audio.loop = true;
                    reproducing = true;
                }
            }
                
            else
            {
                animator.SetBool("inMove", false);
                if (reproducing)
                {
                    reproducing = false;
                    Audio.Stop();
                }
            }
                


            if (MoveX < 0)
                transform.localScale = new Vector3(-3, 3, 1);
            else if (MoveX > 0)
                transform.localScale = new Vector3(3, 3, 1);

            Vector2 newPosition = PlayerTransform.position + new Vector3(MoveX, MoveY, 0);
            PlayerTransform.position = newPosition;
        }
        else GetComponent<CapsuleCollider2D>().enabled = false;
        updateText();
    }

    public void updateText()
    {
        lifeText.text = "Vida: " + Life + "/" + LifeMax;
        levelText.text = "Nivel: " + Level;
        expText.text = "Burbujas: " + Experience + "/" + ExperienceRequired;
    }

    //Recovery Health
    public void recoveryHealth(int recovery)
    {
        Audio.loop = false;
        Audio.clip = Health;
        Audio.Play();
        if (Life + recovery >= LifeMax)
            Life = LifeMax;  
        else
            Life += recovery;
        
    }

    //Update Health
    public void updateHealth(int health)
    {      
        LifeMax += health;
        Life += health;
    }


    //Take Damage
    public void takeDamage(int damage)
    {
        Audio.loop = false;
        Audio.clip = TakeDamage;
        Audio.Play();
        bool isLive = true;
        if (damage >= Life)
        {
            Life = 0;
            isLive = false;
            if (Level > 1)
                desLevelUp();
        }
        else
            Life -= damage;
    }


    //Take Experience
    public void takeExperience(int Experience)
    {
        if (this.Experience + Experience >= ExperienceRequired)
        {
            this.Experience += Experience;
            levelUp();
        }
            
        else
            this.Experience += Experience;
    }

    public void updateSpeedMov(float speed)
    {
        SpeedMove += speed;
    }

    public void updateSpeedOff(float speed)
    {
        SpeedOff += speed;
    }

    public void updateSpeedDef(float speed)
    {
        SpeedDef += speed;
    }

    public void updateDamage(int damage)
    {
        this.damage += damage;
    }

    public void levelUp()
    {
        Audio.clip = LevelUp;
        Audio.loop = false;
        Audio.Play();
        Level++;
        damage++;
        LifeMax += 5;
        Life = LifeMax;
        SpeedMove += 0.1f;
        SpeedDef += 0.1f;
        SpeedOff += 0.1f;
        int ExperienciaExtra = Experience - ExperienceRequired;
        Experience = 0;
        ExperienceRequired += 10;
        takeExperience(ExperienciaExtra);
    }

    public void desLevelUp()
    {
        if(Level >1)
        {
            Level--;
            damage--;
            LifeMax -= 5;
            Life = LifeMax;
            SpeedMove -= 0.1f;
            SpeedDef -= 0.1f;
            SpeedOff -= 0.1f;
        }
        else
        {
            Life = LifeMax;
            Experience = 0;
        }
    }

    public void addItem(GameObject item)
    {
        bool encontrado = false;
        for(int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i].Item != null)
            {
                if (Inventory[i].Item.GetComponent<Item>().name == item.GetComponent<Item>().name)
                {
                    Inventory[i].Cantidad++;
                    encontrado = true;
                    break;
                }
            }
        }

        if (!encontrado)
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].Item == null)
                {
                    Inventory[i].Item = item;
                    Inventory[i].Cantidad = 1;
                    Inventory[i].Sprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
                    Inventory[i].Text.text = "1";
                    break;
                }
            }
        }
    }
}

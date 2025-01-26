using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform PlayerTransform;

    public int Life;
    private float MoveX, MoveY;

    public Animator animator;
    public GameObject playerMinigame;

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


    public List<GameObject> inventory;

    //Estates
    public bool inCinematic;
    public bool isLive;
    public GameObject PanelCombat;

    private void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        Life = LifeMax;
        inCinematic = false;
        isLive = true;
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
                animator.SetBool("inMove", true);
            else
                animator.SetBool("inMove", false);


            if (MoveX < 0)
                transform.localScale = new Vector3(-3, 3, 1);
            else if (MoveX > 0)
                transform.localScale = new Vector3(3, 3, 1);

            Vector2 newPosition = PlayerTransform.position + new Vector3(MoveX, MoveY, 0);
            PlayerTransform.position = newPosition;
        }
        else GetComponent<CapsuleCollider2D>().enabled = false;
        
    }

    //Recovery Health
    public void recoveryHealth(int recovery)
    {
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
        Level++;
        damage++;
        LifeMax += 5;
        Life = LifeMax;
        SpeedMove += 0.1f;
        SpeedDef += 0.1f;
        SpeedOff += 0.1f;
        Experience -= ExperienceRequired;
        ExperienceRequired += 10;
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
        }
    }

    public void addItem(GameObject item)
    {
        inventory.Add(item);
    }

    public void deleteItem(GameObject item)
    {
        bool exist = false;
        foreach(GameObject i in inventory)
        {
            if(i==item)
            {
                exist = true;
                inventory.Remove(i);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform PlayerTransform;

    public int Life;
    private float MoveX, MoveY;

    public Animator animator;

    //Leveling
    public int ExperienceRequired = 5;
    public int Experience = 0;
    public int Level = 1;


    //Attribute
    public int damage = 5;
    public int LifeMax = 15;
    public float SpeedMove = 5f;
    public float SpeedDef = 5f;
    public float SpeedOff = 5f;


    private void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        Life = 5;

    }

    private void Update()
    {
        Debug.Log(Life);
        MoveX = Input.GetAxis("Horizontal") * SpeedMove * Time.deltaTime;
        MoveY = Input.GetAxis("Vertical") * SpeedMove * Time.deltaTime;


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

        //
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
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
    public bool takeDamage(int damage)
    {
        bool isLive = true;
        if (damage >= Life)
        {
            Life = 0;
            isLive = false;
            if (Level > 1)
                Level--;
        }
        else
            Life -= damage;
        return isLive;
    }


    //Take Experience
    public void takeExperience(int Experience)
    {
        if (this.Experience + Experience >= ExperienceRequired)
            Level++;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform PlayerTransform;

    public int Life;
    public int LifeMax = 5;
    

    //Movement
    public float SpeedMove = 5f;
    private float MoveX, MoveY;

    //Leveling
    private int ExperienceRequired = 5;
    public int Experience = 0;
    public int Level = 1;


    //Attribute

    public int streng;
    

    private void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        Life = LifeMax;
    }

    private void Update()
    {
        MoveX = Input.GetAxis("Horizontal") * SpeedMove * Time.deltaTime;
        MoveY = Input.GetAxis("Vertical") * SpeedMove * Time.deltaTime;

        //Move the transform
        Vector2 newPosition = PlayerTransform.position + new Vector3(MoveX,MoveY,0);
        PlayerTransform.position = newPosition;

        //
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    //Recovery Health
    public void recoveryHealth(int recovery)
    {
        if(Life + recovery >= LifeMax)
            Life = LifeMax;
        else
            Life += recovery;
    }

    //Update Health
    public void updateHealth(int health)
    {
        LifeMax+=health;
        Life+=health;
    }


    //Take Damage
    public bool takeDamage(int damage)
    {
        bool isLive = true;
        if(damage>=Life)
        {
            Life = 0;
            isLive=false;
            if(Level > 1)
                Level--;
        }
        else
            Life-=damage;
        return isLive;
    }


    //Take Experience
    public void takeExperience(int Experience)
    {
        if(this.Experience + Experience >= ExperienceRequired)
            Level++;
        else
            this.Experience += Experience;
    }
}

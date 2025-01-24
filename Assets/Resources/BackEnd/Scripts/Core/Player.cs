using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform PlayerTransform;

    public float Life;
    public float LifeMax = 5f;
    

    //Movement
    public float SpeedMove = 5f;
    private float MoveX, MoveY;

    //Leveling
    private float ExperienceRequired = 5f;
    public float Experience = 0;

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
    public void recoveryHealth(float recovery)
    {
        if(Life + recovery >= LifeMax)
            Life = LifeMax;
        else
            Life += recovery;
    }


    public bool takeDamage(float damage)
    {
        bool isLive = true;
        if(damage>=Life)
        {
            Life = 0;
            isLive=false;
        }
        else
            Life-=damage;
        return isLive;
    }
}

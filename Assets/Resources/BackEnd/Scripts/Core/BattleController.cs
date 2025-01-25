using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public bool inDefense = false;
    public bool isDeath = false;
    public Enemy enemy;
    private Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    private void atacking(int damage)
    {
        int atack;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        

    }
}

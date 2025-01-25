using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoosterItem : MonoBehaviour
{
    public string effect = "Este item duplica tu daño de ataue en el siguiente ataque";

    private Player player;

    private int damageBooster(int playerDamage)
    {
        return playerDamage * 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.updateDamage(damageBooster(player.damage));   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

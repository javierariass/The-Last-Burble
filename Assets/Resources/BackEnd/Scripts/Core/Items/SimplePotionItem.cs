using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePotionItem : MonoBehaviour
{
    public string effect = "Este item recupera 5 puntos de vida";
    private Player player;

    private int recovery = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.recoveryHealth(recovery);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

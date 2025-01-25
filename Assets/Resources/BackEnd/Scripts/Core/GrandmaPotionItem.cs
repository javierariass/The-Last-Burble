using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaPotionItem : MonoBehaviour
{
    private string effect = "Este item tiene probabilidad de hundirte o ayudarte";
    public bool accepted = false;

    private Player player;


    public int recovery(bool action)
    {
        int recoveryOrNot;
        if (action)
        {
            recoveryOrNot = Random.Range(5, 10);
        }
        else
        {
            recoveryOrNot = Random.Range(-5, -10);
        }

        return recoveryOrNot;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per fram
    void Update()
    {
        player.updateHealth(recovery(accepted));
    }
}

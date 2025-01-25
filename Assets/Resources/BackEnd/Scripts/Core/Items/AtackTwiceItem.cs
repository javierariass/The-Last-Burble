using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackTwiceItem : MonoBehaviour
{
    public string effect = "Este item te permite atacar dos veces en tu siguiente turno";
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject); 
    }
}

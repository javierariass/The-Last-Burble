using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelDefUpVelOffDown : MonoBehaviour
{
    public string effect = "Este item aumenta tu defensa (te mueves más rápido)";
    private Player player;

    private float modifyVelDef(float velInitPlayer)
    {
        return velInitPlayer * 1.2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        player.updateSpeedDef(modifyVelDef(player.SpeedDef));
        Destroy(gameObject);
    }
}

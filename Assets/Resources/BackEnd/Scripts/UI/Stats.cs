using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private Player player;
    public TextMeshProUGUI Life, Exp, AttackVel, DefVel, MoveVel;
    private bool Active = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        //Update del panel de estadisticas
        Life.text = "Life: " + player.Life.ToString() + "/" + player.LifeMax.ToString();
        Exp.text = "Burbles: " + player.Experience.ToString() + "/" + player.ExperienceRequired.ToString();
        AttackVel.text = "Atk Vel: " + player.SpeedOff.ToString();
        DefVel.text = "Def Vel: " + player.SpeedDef.ToString();
        MoveVel.text = "Move Vel: " + player.SpeedMove.ToString();

        //Apertura y cierre por acceso directo
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (Active)
            {
                transform.localScale = Vector3.zero;
                Active = false;
            }
            else
            {
                transform.localScale = Vector3.one;
                Active = true;
            }
        }
    }
}

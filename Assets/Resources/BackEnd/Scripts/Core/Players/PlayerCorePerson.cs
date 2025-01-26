using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class PlayerCorePerson : MonoBehaviour
{
    public Player player;
    public BattleController controller;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    private void Update()
    {
        if(controller.inDefense)
        {
            float MoveX = Input.GetAxis("Horizontal") * player.SpeedMove * Time.deltaTime;
            float MoveY = Input.GetAxis("Vertical") * player.SpeedMove * Time.deltaTime;
            Vector2 newPosition = transform.position + new Vector3(MoveX, MoveY, 0);
            transform.position = newPosition;
        }
    }
}

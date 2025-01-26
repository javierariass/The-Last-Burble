using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnemyTransition : MonoBehaviour
{
    public BattleController bc;

    public void Change()
    {
        bc.enemy.ChangePosition();
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }
}

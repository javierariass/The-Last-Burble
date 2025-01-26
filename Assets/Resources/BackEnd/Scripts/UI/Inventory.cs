using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Casilla casillaSelected;
    public TextMeshProUGUI Description;
    private Player player;
    private bool Active = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if(casillaSelected != null)
        {
            Description.text = "";
        }
        //Apertura y cierre por acceso directo
        if (Input.GetKeyDown(KeyCode.B) && !player.inCinematic)
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
    public void Use()
    {
        if(casillaSelected.Item != null)
        {
            casillaSelected.Cantidad--;
            //Usar item
            GameObject item = Instantiate(casillaSelected.Item, new Vector3(1000, 1000, 1000), Quaternion.identity);
            Destroy(item, 1);
            //Revisar si quedan
            if(casillaSelected.Cantidad ==0)
            {
                casillaSelected.Item = null;
                casillaSelected.Text.text = "";
                casillaSelected.Sprite.sprite = casillaSelected.defaultSprite;
            }
        }
        casillaSelected = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Casilla : MonoBehaviour, IPointerDownHandler
{
    
    public int Cantidad = 0;
    public GameObject Item;
    public TextMeshProUGUI Text;
    public Image Sprite;
    public Sprite defaultSprite;
    public Inventory inventory;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Item != null)
        {
            Text.text = Cantidad.ToString();
            Sprite.sprite = Item.GetComponent<SpriteRenderer>().sprite;
            inventory.casillaSelected = this;
        }
        else
        {
            Text.text = "";
            Sprite.sprite = defaultSprite;
            inventory.casillaSelected = null;
        }
    }
}

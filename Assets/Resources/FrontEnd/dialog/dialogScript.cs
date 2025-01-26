using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogScript : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public string[] lines;

    public float textSpeed = 0.1f;

    public Animator animator;

    int index;

    public Player player;

    public GameObject CombatAction;

    public BattleController bc;

    public bool EndTutorial = false;

    public BoxCollider2D collider;
    private AudioSource Audio;
    void Start()
    {
        dialogText.text = string.Empty;
        startDialog();
        Audio = GetComponent<AudioSource>();
        
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(dialogText.text == lines[index]){
                nextLine();
            }
            else{
                StopAllCoroutines();
                dialogText.text = lines[index];
            }

        }
    }

    public void startDialog(){
        index = 0;
           
        StartCoroutine(writeLine());

    } 

    IEnumerator writeLine(){

        foreach (char letter in lines[index].ToCharArray()){
            dialogText.text += letter;        
            //Audio.Play();
            yield return new WaitForSeconds(textSpeed);
        }

    }

    public void nextLine(){
        if(index < lines.Length - 1){
            index++;
            dialogText.text = string.Empty;
            StartCoroutine(writeLine());
        }
        else{
            if(animator != null)animator.SetTrigger("Traslade");
            if (CombatAction != null) CombatAction.SetActive(true);
            if (bc != null && !EndTutorial) bc.StartCoroutine(bc.Defense());
            if (EndTutorial) bc.InitBatle();
            if (collider != null) collider.size = new Vector2(3f, 3f);
            gameObject.SetActive(false);
            if (player != null) player.inCinematic = false;
        }
    }
}

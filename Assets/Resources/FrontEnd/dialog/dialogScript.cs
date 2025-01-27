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

    public AudioSource Audio;
    void Start()
    {
        dialogText.text = "";
        startDialog();
        Audio = GetComponent<AudioSource>();
        if (GameObject.FindGameObjectWithTag("BattleController"))GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>().EnTexto = true;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(dialogText.text == lines[index]){
                Audio.Play();
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
        Audio.Play();
        StartCoroutine(writeLine());

    } 

    IEnumerator writeLine(){

        foreach (char letter in lines[index].ToCharArray()){
            dialogText.text += letter;             
            yield return new WaitForSeconds(textSpeed);
        }
        Audio.Stop();
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
            if (bc != null && !EndTutorial) bc.StartCoroutine(bc.DefenseAlert());
            if (EndTutorial) bc.InitBatle();
            if (collider != null) collider.size = new Vector2(3f, 3f);
            if (player != null) player.inCinematic = false;
            if(GameObject.FindGameObjectWithTag("BattleController")) GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>().EnTexto = false;
            gameObject.SetActive(false);
                  
        }
    }
}

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


    void Start()
    {
        dialogText.text = string.Empty;
        startDialog();
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
            animator.SetTrigger("Traslade");
            gameObject.SetActive(false);        
        }
    }
}

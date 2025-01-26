using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMenu : MonoBehaviour
{

    private Transform PlayerTransform;
    private float MoveX, MoveY;
    public Animator animator;
    public float SpeedMove = 1f;
    public GameObject Panel;
    public bool Stop = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!Panel.gameObject.activeSelf && !Stop)
        {
            MoveX = Input.GetAxis("Horizontal") * SpeedMove * Time.deltaTime;
            MoveY = Input.GetAxis("Vertical") * SpeedMove * Time.deltaTime;
            GetComponent<CapsuleCollider2D>().enabled = true;

            if (MoveY != 0 || MoveX != 0)
                animator.SetBool("inMove", true);
            else
                animator.SetBool("inMove", false);


            if (MoveX < 0)
                transform.localScale = new Vector3(-5, 5, 1);
            else if (MoveX > 0)
                transform.localScale = new Vector3(5, 5, 1);

            Vector2 newPosition = PlayerTransform.position + new Vector3(MoveX, MoveY, 0);
            PlayerTransform.position = newPosition;
        }
        else
        {
            animator.SetBool("inMove", false);
        }

    }
}

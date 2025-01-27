using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurbleGenerator : MonoBehaviour
{
    public Sprite burble;

    private void Start()
    {
        StartCoroutine(Burble());
    }

    IEnumerator Burble()
    {
        yield return new WaitForSeconds(1);
        GameObject obj = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        obj.AddComponent<SpriteRenderer>().sprite = burble;
        obj.transform.localScale = new Vector3(2, 2, 2);
        obj.AddComponent<Rigidbody2D>().gravityScale = -0.02f;
        StartCoroutine(Burble());
    }
}

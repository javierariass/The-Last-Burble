using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public List<Object> spawners = new List<Object>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(shootSpawner());
    }

    IEnumerator shootSpawner()
    {
        int cont = 0;
        List<int> results = randomShoot();
        while(cont<5)
        {
           // spawners[results[cont]].shoot();
            cont++;
        }
        yield return new WaitForSeconds(Random.Range(1,3));
    }
    
    public List<int> randomShoot()
    {
        List<int> result = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            result.Add(Random.Range(1,20));
        }
        return result;
    }

}

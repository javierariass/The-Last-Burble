using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public List<SpawnerShoot> spawners = new List<SpawnerShoot>();
    public bool ShootSpawn = false;
    public int cantSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(!ShootSpawn)
       {
        int cont = 0;
        List<int> results = randomShoot();
        while(cont<cantSpawner)
        {
            spawners[results[cont]].Shoot();
            cont++;
        }
        StartCoroutine(shootSpawner());
       } 
    }

    IEnumerator shootSpawner()
    {
        ShootSpawn = true;
        yield return new WaitForSeconds(Random.Range(1,3));
        ShootSpawn = false;
    }
    
    public List<int> randomShoot()
    {
        List<int> result = new List<int>();

        for (int i = 0; i < cantSpawner; i++)
        {
            result.Add(Random.Range(1,spawners.Count));
        }
        return result;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour, IActivable
{
    [SerializeField] bool active;

    [SerializeField] SpawnChoix spawnChoix;

    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int nomberOfSpawn;
    [SerializeField] float timeBetweenSpawn;

    int nombreDeSpawn;
    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            ChoixSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChoixSpawn ()
    {
        switch(spawnChoix)
        {
            case SpawnChoix.spawnContinue:
                StartCoroutine(CoroutineSpawnsContinue());
                break;
            case SpawnChoix.spawnNombre:
                StartCoroutine(CoroutineSpawns());
                break;

        }
    }

    void SpawnObject ()
    {
        Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
    }

    enum SpawnChoix
    {
        aucun,
        spawnContinue,
        spawnNombre,
    }

    
    

    IEnumerator CoroutineSpawnsContinue()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        SpawnObject();
        StartCoroutine(CoroutineSpawnsContinue());
    }

    IEnumerator CoroutineSpawns()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        if (nombreDeSpawn < nomberOfSpawn)
        {
            
            SpawnObject();
            nombreDeSpawn++;
           
            StartCoroutine(CoroutineSpawns());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    public void ChangeActiveState()
    {
        if (!this.active)
        {
            active = !active;
            ChoixSpawn();
        }
        
    }
}

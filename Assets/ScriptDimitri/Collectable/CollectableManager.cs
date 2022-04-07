using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] List<int> collectable;
    [SerializeField] List< GameObject> spawnerCollectable;
    [SerializeField] GameObject collectablePrefab;
    [SerializeField] CollectableDonner donner;

    public delegate void AddCollectable(int id);
    public static AddCollectable AddACollectable;
    // Start is called before the first frame update
    void Start()
    {
        AddACollectable += AddACollectables;
        collectable = new List<int>();
        for (int i = 0; i < spawnerCollectable.Count; i++)
        {
            if (donner.Collecter[i])
            {
                collectable.Add(1);
            }
            else
            {
                collectable.Add(0);
            }
        }
        SpawnCollectable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCollectable ()
    {
        for (int i = 0; i < spawnerCollectable.Count; i++)
        {
            if(collectable[i] != 1)
            {
                GameObject collectables = Instantiate(collectablePrefab, spawnerCollectable[i].transform.position, Quaternion.identity, spawnerCollectable[i].transform);
                collectables.GetComponent<CollectableConttroler>().idCollectable = i;
            }
        }
    }

    void AddACollectables (int id)
    {
        if (collectable[id] != 1)
        {
            collectable[id] += 1;
            donner.SetCollect(id);
        }
        else
        {
            Debug.Log("DejaRecup Chelou");
        }
             
        
    }

}

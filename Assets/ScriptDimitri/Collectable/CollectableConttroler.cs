using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableConttroler : MonoBehaviour
{
    [SerializeField]public int idCollectable;
    [SerializeField] int LayerForCollect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerForCollect)
        {
          
            CollectableManager.AddACollectable(idCollectable);
            Destroy(gameObject);
        }
    }
}

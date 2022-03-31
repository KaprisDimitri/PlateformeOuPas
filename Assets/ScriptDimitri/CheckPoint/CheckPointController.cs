using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public bool check;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material materialCheck;
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
        if(!check)
        {
            if(other.gameObject.layer == 7)
            {
               
                check = true;
                mesh.material = materialCheck;
            }
        }
    }
}

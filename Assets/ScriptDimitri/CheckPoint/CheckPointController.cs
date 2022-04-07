using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public bool check;
    [SerializeField] GameObject basegam;
    [SerializeField] GameObject newGam;

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
                SoundManger.playSound(20, transform.position);
                Instantiate(newGam, basegam.transform.position, Quaternion.identity, gameObject.transform);
                Destroy(basegam);
            }
        }
    }
}

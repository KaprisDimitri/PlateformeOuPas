using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour , IDiferenteWorld
{
    bool canSwipe;
    // Start is called before the first frame update
    void Start()
    {
        canSwipe = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfCanSwipe ()
    {
        return canSwipe;
    }

    public void SetInvisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -15);
        if (gameObject.transform.childCount != 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void SetVisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (gameObject.transform.childCount != 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            canSwipe = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            canSwipe = true;
        }
    }
}

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
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (gameObject.transform.childCount != 0)
        {
            //gameObject.transform.GetComponent<MeshRenderer>().enabled = true;//.gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void SetVisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -15);
        if (gameObject.transform.childCount != 0)
        {
            //gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touche");
        if (other.gameObject.layer == 7)
        {
            Debug.Log("touche2");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour , IDiferenteWorld
{
    [SerializeField] bool ajoutVie;
    [SerializeField] int nombreAjoutVie;
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
        if(other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<Health>().AddRemoveHearth(nombreAjoutVie, ajoutVie);
            Destroy(gameObject);
        }
    }

    public void SetInvisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -15);
    }

    public void SetVisible()
    {
        Debug.Log("yo");
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}

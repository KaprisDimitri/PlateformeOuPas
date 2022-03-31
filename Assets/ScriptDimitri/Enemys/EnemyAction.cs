using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public bool dedoublement;

    public bool canDedouble;
    int layerPlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineCanDedouble());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Dedoublement ()
    {
        Debug.Log("yo"+gameObject.name);
        GameObject en =Instantiate(gameObject, gameObject.transform.position, Quaternion.identity,gameObject.transform.parent);
        en.GetComponent<EnemyController>().choixActiossn = EnemyController.ChoixAction.aucun;
        gameObject.GetComponent<EnemyController>().choixActiossn = EnemyController.ChoixAction.aucun;
        en.GetComponent<EnemyAction>().dedoublement = false;
        dedoublement = false;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (dedoublement)
        {
            Debug.Log("yo" + gameObject.name);
            if (canDedouble && other.gameObject.layer == 7)
            {
                Debug.Log("yo2" + gameObject.name);
                if (!(other.gameObject.GetComponent<PlayerMovementDim>().grounded))
                {
                    Debug.Log("yo3" + gameObject.name);
                    canDedouble = false;
                    StopAllCoroutines();
                    other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().velocity.x, 10, 0);//.AddForce((other.gameObject.transform.position - transform.position) * 300);
                    Dedoublement();
                    StartCoroutine(CoroutineCanDedouble());
                }
            }
        }
    }
    

   IEnumerator CoroutineCanDedouble ()
    {
        yield return new WaitForSeconds(1f);
        canDedouble = true;
    }
}

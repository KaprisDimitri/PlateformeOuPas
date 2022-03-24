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
        Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (dedoublement)
        {
            
            if (canDedouble && other.gameObject.layer == 7)
            {
                
                if (!(other.gameObject.GetComponent<PlayerMovement>().IsGrounded()))
                {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicsController : MonoBehaviour , IDiferenteWorld
{
    [SerializeField] Vector2 pointOuJump;
    [SerializeField] float diametreOverLaJumpp;
    [SerializeField] float forceDeSaut;
    [SerializeField] LayerMask playerLayer;

    //WorkInProgresse
    [SerializeField] Vector2 pointOuRepouce;
    [SerializeField] float forceDeSautCoter;
    [SerializeField] float diametreOverLaReponce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        AjoutDeForceEnPlayer(CheckIfPlayerOverLaJumpp(playerLayer, pointOuJump, diametreOverLaJumpp), forceDeSaut);
        //WorkInProgresse AjoutDeForceEnPlayerRepouce(CheckIfPlayerOverLaReponce(playerLayer, pointOuRepouce, diametreOverLaReponce), forceDeSautCoter);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector3)pointOuJump + transform.position, diametreOverLaJumpp);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector3)pointOuRepouce + transform.position, diametreOverLaReponce);
    }

    Rigidbody CheckIfPlayerOverLaJumpp(LayerMask layerplayer, Vector2 center, float diametreOverlap)
    {
        Collider[] col = Physics.OverlapSphere((Vector3)center + transform.position, diametreOverlap, layerplayer);
        if (col.Length > 0)
        {
            return col[0].gameObject.GetComponent<Rigidbody>();
        }
        else
        {
            return null;
        }
    }

    //WorkInProgresse
    GameObject CheckIfPlayerOverLaReponce(LayerMask layerplayer, Vector2 center, float diametreOverLaReponce)
    {
        Collider[] col = Physics.OverlapSphere((Vector3)center + transform.position, diametreOverLaReponce, layerplayer);
        if (col.Length > 0)
        {
            return col[0].gameObject;
        }
        else
        {
            return null;
        }
    }

    void AjoutDeForceEnPlayer(Rigidbody rb, float forceDeSaut)
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, forceDeSaut);
        }
    }


    //WorkInProgresse
    void AjoutDeForceEnPlayerRepouce(GameObject player, float forceDeSautCoter)
    {

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;

            Rigidbody rb = player.GetComponent<Rigidbody>();
            // rb.velocity = Vector3.zero;
            // rb.velocity= (new Vector2(direction.x * forceDeSautCoter*1000, forceDeSautCoter));
            rb.AddForce(direction * forceDeSautCoter);
            Debug.Log((new Vector2(direction.x * forceDeSautCoter * 10, forceDeSautCoter)));
        }
    }

    public void SetInvisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -15);
    }

    public void SetVisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }


}
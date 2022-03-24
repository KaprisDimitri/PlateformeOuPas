using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeAction : MonoBehaviour
{
    bool lancer;
    bool finishTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator CoroutineStartTime(float time)
    {
        yield return new WaitForSeconds(time);
        finishTime = true;
       
    }

    public bool PlatefomeDestroy (float time, MeshRenderer mesh, BoxCollider col, BoxCollider trig)
    {
        if(!lancer && !finishTime)
        {
            lancer = true;
            StopAllCoroutines();
            StartCoroutine(CoroutineStartTime(time));
           
        }

        if(finishTime&& lancer)
        {
            EnableView(mesh, col,trig, false);
            finishTime = false;
            lancer = false;
            return true;
        }
        
        return false;
    }

    public void EnableView (MeshRenderer mesh, BoxCollider col, BoxCollider trig, bool active)
    {
        mesh.enabled = active;
        trig.enabled = active;
        col.enabled = active;
    }
}

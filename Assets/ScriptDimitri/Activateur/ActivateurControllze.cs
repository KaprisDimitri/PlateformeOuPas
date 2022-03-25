using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateurControllze : MonoBehaviour
{
    [SerializeField] ChoixActivation choixActivation;
    [SerializeField] GameObject activable;
    [SerializeField] float tempsAvantValidation;

    [Space]
    [SerializeField] int layerToTrig;

    bool activer;
    
    bool waited;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    enum ChoixActivation
    {
        aucun,
        trigContinue,
        trigActifDesactif,
        trigDoisRester,
    }

    void SetActivable ()
    {
        activable.GetComponent<IActivable>().ChangeActiveState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(choixActivation == ChoixActivation.trigContinue)
        {
            if(!activer && other.gameObject.layer == layerToTrig)
            {
                
                activer = true;
                StartCoroutine(CoroutineValidation());
            }
        }
        else if (choixActivation == ChoixActivation.trigActifDesactif)
        {
            if (other.gameObject.layer == layerToTrig)
            {
                StartCoroutine(CoroutineValidation());
            }
        }
        else if (choixActivation == ChoixActivation.trigDoisRester)
        {
            if (other.gameObject.layer == layerToTrig)
            {
                
                StartCoroutine(CoroutineValidation());

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (choixActivation == ChoixActivation.trigDoisRester)
        {
            if (other.gameObject.layer == layerToTrig)
            {
                

                SetActivable();
                
            }
        }

        if (other.gameObject.layer == layerToTrig)
        {

            StopAllCoroutines();
        }
    }

    IEnumerator CoroutineValidation ()
    {
        yield return new WaitForSeconds(tempsAvantValidation);
        SetActivable();
    }
}

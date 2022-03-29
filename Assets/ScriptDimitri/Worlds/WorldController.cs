using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{

    [SerializeField] public GameObject decor;
    [SerializeField] GameObject plateformes;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject activateur;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject pics;
    [SerializeField] GameObject bonusMalus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAllInvisible ()
    {
        for(int i = 0; i< decor.transform.childCount;i++)
        {
            decor.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetInvisible();
        }

        for(int i=0;i< plateformes.transform.childCount;i++)
        {
            plateformes.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetInvisible();
        }

        for (int i = 0; i < spawn.transform.childCount; i++)
        {
            spawn.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetInvisible();
        }

        for (int i = 0; i < activateur.transform.childCount; i++)
        {
            activateur.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetInvisible();
        }

        for (int i = 0; i < enemy.transform.childCount; i++)
        {
            enemy.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetInvisible();
        }

        for (int i = 0; i < pics.transform.childCount; i++)
        {
            pics.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetInvisible();
        }

        for (int i = 0; i < bonusMalus.transform.childCount; i++)
        {
            bonusMalus.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetInvisible();
        }
    }

    public void SetAllVisible ()
    {
        for (int i = 0; i < decor.transform.childCount; i++)
        {
            decor.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetVisible();
        }

        for (int i = 0; i < plateformes.transform.childCount; i++)
        {
            plateformes.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetVisible();
        }

        for (int i = 0; i < spawn.transform.childCount; i++)
        {
            spawn.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetVisible();
        }

        for (int i = 0; i < activateur.transform.childCount; i++)
        {
            activateur.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetVisible();
        }

        for (int i = 0; i < enemy.transform.childCount; i++)
        {
            enemy.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetVisible();
        }

        for (int i = 0; i < pics.transform.childCount; i++)
        {
            pics.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetVisible();
        }

        for (int i = 0; i < bonusMalus.transform.childCount; i++)
        {
            bonusMalus.transform.GetChild(i).GetComponent<IDiferenteWorld>().SetVisible();
        }
    }
}

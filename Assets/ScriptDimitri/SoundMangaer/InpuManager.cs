using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InpuManager : MonoBehaviour
{
    [SerializeField] InputsStorage inputPreSet;
    [SerializeField] InputsStorage inputPlay;


    public static KeyCode right;
    public static KeyCode left;
    public static KeyCode jump;
    public static KeyCode CahngeWorld;



    // Start is called before the first frame update
    void Start()
    {
        right = inputPlay.right;
        left = inputPlay.left;
        jump = inputPlay.jump;
        CahngeWorld = inputPlay.changeWorld;
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ResetInputs()
    {
         inputPlay.right = inputPreSet.right;
        inputPlay.left = inputPreSet.left;
         inputPlay.jump = inputPreSet.jump;
        inputPlay.changeWorld = inputPreSet.changeWorld;
    }

    public void  ChangeRight (KeyCode code)
    {
        inputPlay.right = code;
        right = inputPlay.right;
    }

    public void ChangeLeft(KeyCode code)
    {
        inputPlay.left = code;
        left = inputPlay.left;
    }

    public void ChangeJump(KeyCode code)
    {
        inputPlay.jump = code;
        jump = inputPlay.jump;
    }

    public void ChangeWorld(KeyCode code)
    {
        inputPlay.changeWorld = code;
        CahngeWorld = inputPlay.changeWorld;
    }
}

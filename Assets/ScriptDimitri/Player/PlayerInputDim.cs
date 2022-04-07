using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputDim : MonoBehaviour
{
    KeyCode jump;
    KeyCode left;
    KeyCode right;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPlayerInputDim (KeyCode jump, KeyCode left, KeyCode right)
    {
        this.jump = InpuManager.jump;
        this.left = InpuManager.left;
        this.right = InpuManager.right;
        
    }

    public Vector2 DirectionPlayer ()
    {
        Vector2 direction = Vector2.zero;
        if(Input.GetKeyDown(InpuManager.jump))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(InpuManager.left))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(InpuManager.right))
        {
            direction += Vector2.right;
        }
        return direction;
    }
}

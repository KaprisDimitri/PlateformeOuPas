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
        this.jump = jump;
        this.left = left;
        this.right = right;
        
    }

    public Vector2 DirectionPlayer ()
    {
        Vector2 direction = Vector2.zero;
        if(Input.GetKeyDown(jump))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(left))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(right))
        {
            direction += Vector2.right;
        }
        return direction;
    }
}

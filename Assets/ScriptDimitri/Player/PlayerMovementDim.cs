using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDim : MonoBehaviour
{
    Transform firstPositionForRayCast;
    Transform SecondPositionForRayCast;

    Transform rightSide;
    Transform leftSide;

    LayerMask layerGround;

    float veloX;

    bool grounded;
   

    float timeGround;
    bool wasGrounded;
    bool canJump;
    bool jump;
    float t2;
    int u;

    public int wallDirection;
    bool wallJump;

    float timeWallJump;
    float timeToReachWallJump;

     float forceJumpWallY;
     float forceJumpWallX;
    // Start is called before the first frame update
    void Start()
    {
        wasGrounded = false;
        veloX = 0;
       
        timeGround = 0.2f;
       
    }

    // Update is called once per frame
    void Update()
    {
       

        if(wallJump)
        {
            TimerWallJump();
        }
       
        grounded = CheckIfIsGrounded();
        ChangeWallDirection();
        wasGrounded = !grounded;
        if(wasGrounded && u == 0 && !jump)
        {
            canJump = true;
        }
        Debug.Log(wasGrounded);
       // Debug.Log(wallJump);
       // Debug.Log(wallDirection);
        //Debug.Log(grounded + "canJump");

        if (wasGrounded)
        {
            TimerGround();
        }

        
    }

    public void InitPlayerMovement (Transform firstPositionForRayCast, Transform SecondPositionForRayCast, LayerMask layerGround, Transform rightSide, Transform leftSide,float timeToReachWallJump,float forceJumpWallY, float forceJumpWallX)
    {
        this.firstPositionForRayCast = firstPositionForRayCast;
        this.SecondPositionForRayCast = SecondPositionForRayCast;
        this.layerGround = layerGround;
        this.rightSide = rightSide;
        this.leftSide = leftSide;
        this.timeToReachWallJump = timeToReachWallJump;
        this.forceJumpWallX = forceJumpWallX;
        this.forceJumpWallY = forceJumpWallY;
    }

    public void MovePlayer(Rigidbody rd, Vector2 direction, float force, float vitesseDeplacement, float forceJump, float vitessePerteVitesse)
    {
        if (direction.y == 1)
        {
            if ((grounded || canJump)&&! wallJump )
            {
                jump = true;
                canJump = false;
                float forceJumpBase = 5;
                if (veloX < 0)
                {
                    forceJump *= ((veloX / force * -1));
                }
                else
                {
                    forceJump *= ((veloX / force));
                }

                Debug.Log("yo");
                rd.velocity = new Vector3(veloX, direction.y * forceJumpBase + forceJump);

            }
            else
            {
                if (wallDirection != 0 && !wallJump && !canJump)
                {
                    WallJump(rd, force, forceJump);
                }
                else if (!wallJump)
                {
                    rd.velocity = new Vector3(veloX, rd.velocity.y);
                }
            }
        }

        if (direction.y < 1 && direction.x !=0 && !wallJump)
        {
            
            if (veloX < force && veloX > -force)
            {
                vitesseDeplacement *= (1 - (veloX / (force*direction.x)));
                veloX += (Time.deltaTime * (vitesseDeplacement)*direction.x);
                ChangeVeloxWalled();
                rd.velocity = new Vector3(veloX, rd.velocity.y);
            }
            else
            {
                ChangeVeloxWalled();
                rd.velocity = new Vector3(veloX, rd.velocity.y);
            }
        }
       

        if(direction.x == 0 && veloX !=0 && !wallJump)
        {
            if(veloX >0)
            {
                veloX -= (Time.deltaTime * vitessePerteVitesse);
                if (veloX < 0)
                {
                    veloX = 0;
                }
            }
            else
            {
                veloX += (Time.deltaTime * vitessePerteVitesse);
                if (veloX > 0)
                {
                    veloX = 0;
                }
            }
            rd.velocity = new Vector3(veloX, rd.velocity.y);
        }

        if(rd.velocity.y <0 && rd.velocity.y > -((forceJump / 3)*2))
        {
           
            rd.velocity = new Vector3(veloX, rd.velocity.y *1.2f);
        }

    }

    bool CheckIfIsGrounded ()
    {
        Collider[] col = Physics.OverlapBox(firstPositionForRayCast.position, new Vector3(0.4f,0.1f,0.4f), transform.rotation, layerGround) ;
       
       if(col.Length>0)
        {

            canJump = false;
            jump = false;
            t2 = 0;
            u =0;
            return true;
        }
       else
        {
            
            return false;
        }
    }

    void TimerGround()
    {
        if (t2 < timeGround)
        {
            t2 += Time.deltaTime;
        }
        else
        {
            u = 1;
                canJump = false;
            
        }
    }

    public bool WillWalled ()
    {
        Collider[] col = Physics.OverlapBox(rightSide.position, new Vector3(2f, 0.4f, 0.4f), transform.rotation, layerGround);
        if (col.Length > 0)
        {
            return true;
        }
        col = Physics.OverlapBox(leftSide.position, new Vector3(2f, 0.4f, 0.4f), transform.rotation, layerGround);
        if (col.Length > 0)
        {
            return true;
        }
        return false;
    }

    void ChangeWallDirection ()
    {
        Collider[] col = Physics.OverlapBox(rightSide.position, new Vector3(0.1f, 0.4f, 0.4f), transform.rotation, layerGround);
        if (col.Length > 0)
        {
            if (!grounded)
            {
                wallDirection = 1;
            }
        }
        else
        {
            wallDirection = 0;
        }

        col = Physics.OverlapBox(leftSide.position, new Vector3(0.1f, 0.4f, 0.4f), transform.rotation, layerGround);
        if (col.Length > 0)
        {
            if (!grounded)
            {
                wallDirection = -1;
            }
        }

        
    }

    void ChangeVeloxWalled ()
    {
        Collider[] col = Physics.OverlapBox(rightSide.position, new Vector3(0.1f, 0.4f, 0.4f), transform.rotation, layerGround);

        if (col.Length > 0)
        {
            if (veloX > 0)
            {
                veloX = 0;
            }
            if (!grounded)
            {
                wallDirection = 1;
            }
        }
       

       col = Physics.OverlapBox(leftSide.position, new Vector3(0.1f, 0.4f, 0.4f), transform.rotation, layerGround);

        if (col.Length > 0)
        {
            if (veloX <0)
            {
                veloX = 0;
            }
            if (!grounded)
            {
                wallDirection = -1;
            }
        }

        if (grounded)
        {
            wallDirection = 0;
        }
    }
    
    void WallJump (Rigidbody rd,float force, float forceJump)
    {
        
            rd.velocity = new Vector3((forceJumpWallX) *(wallDirection*-1), forceJumpWallY);
        //Debug.Log(rd.velocity);
        wallJump = true;


    }

   void TimerWallJump ()
    {
        if(timeWallJump<timeToReachWallJump)
        {
            timeWallJump += Time.deltaTime;
        }
        else
        {
            timeWallJump = 0;
            wallJump = false;
        }
    }

}

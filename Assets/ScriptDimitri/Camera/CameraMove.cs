using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float maxDist;
    Vector3 playerPosition;
    Vector3 playerDirection;
    Vector3 playerDirection2;

    Vector3 cameraDirection;

    Vector3 poseDeBase;

    Vector3 postionSave;
    float t;
    bool surJoueur;
    bool surPosition;
    bool resetT;
    // Start is called before the first frame update
    void Start()
    {
        poseDeBase = gameObject.transform.position-player.transform.position;
        surJoueur = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        playerDirection = player.GetComponent<Rigidbody>().velocity.normalized;
        playerDirection2 = player.GetComponent<PlayerControllerDim>().directionPlayer;
        Debug.Log(playerDirection);

        MoveTheCamera();
    }

    void MoveTheCamera ()
    {
        /*if (playerDirection != Vector3.zero && playerDirection != new Vector3(1,0,1))
        {
            resetT = false;
            surJoueur = false;
            if (cameraDirection != playerDirection)
            {
                SavePosition();
                
                t = 0;
                surPosition = false;
               cameraDirection = playerDirection;
            }
            else if (!surPosition)
            {
               
                surPosition= AllerSurPosition();
            }
            else
            {
                Debug.Log("yoyoyo");
                gameObject.transform.position =new Vector3( CalculPointOuAller().x+difEntrePlayerPosEtDif().x,CalculPointOuAller().y,gameObject.transform.position.z);
            }
        }
        else if (!surJoueur)
        {
            if(!resetT)
            {
                surPosition = false;
                t = 0;
                resetT = true;
                SavePosition();
                cameraDirection = playerDirection;
            }
           surJoueur = CeREmettreSurJoueur();
        }*/

        /*if(playerDirection2 != Vector3.zero)
        {
            if (cameraDirection != playerDirection2)
            {
                SavePosition();
                ResetT();
                cameraDirection = playerDirection2;
            }
            else
            {
                AddT();
                LerpingCam(t);
                if (gameObject.transform.position != CalculPointOuAller2())
                {
                    Debug.Log("yo");
                    gameObject.transform.position = CalculPointOuAller2();
                }
            }
        }
        else
        {
            if (cameraDirection != playerDirection2)
            {
                SavePosition();
                ResetT();
                cameraDirection = playerDirection2;

            }
            else
            {
                AddT();
                LerpingCam(t);
            }
               
           // gameObject.transform.position = CalculPointOuAller2();
        }*/


      /*  if(playerDirection != Vector3.zero)
        {
            if(Distance() > maxDist)
            {
                gameObject.transform.position = CalculPointOuAller2();
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = playerDirection * 10;
            }
        }
        else
        {
            if (Distance() < 0.1f)
            {
                gameObject.transform.position = new Vector3(poseDeBase.x + playerPosition.x, poseDeBase.y + playerPosition.y,gameObject.transform.position.z);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector2(player.transform.position.x + poseDeBase.x, player.transform.position.y + poseDeBase.y) - new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);//, new Vector3(gameObject.transform.position.x - poseDeBase.x, gameObject.transform.position.y - poseDeBase.y));
            }
       }*/
    }

    void SavePosition()
    {
        postionSave = transform.position;
    }

    bool CeREmettreSurJoueur()
    {
        if (t < 1)
        {
            t += Time.deltaTime/2;
            Vector3 tos = Vector3.Lerp(postionSave, poseDeBase + playerPosition, t);
            gameObject.transform.position = new Vector3(tos.x, tos.y,gameObject.transform.position.z);
            return false;
        }
        else
        {
            
            t = 0;
            gameObject.transform.position = poseDeBase + playerPosition;
            return true;
        }
    }

    bool AllerSurPosition()
    {
        if (t < 1)
        {
            t += Time.deltaTime/2;
          //  Debug.Log(CalculPointOuAller());
            Vector3 tos = Vector3.Lerp(postionSave, CalculPointOuAller(), t);
            gameObject.transform.position = (new Vector3(tos.x+ difEntrePlayerPosEtDif().x, tos.y, gameObject.transform.position.z));
            return false;
        }
        else
        {
            t = 0;
            gameObject.transform.position = CalculPointOuAller();
            return true;
        }
    }

    Vector3 difEntrePlayerPosEtDif ()
    {
        return playerPosition - postionSave;
    }

    Vector3 CalculPointOuAller()
    {
        Vector3 point = postionSave +cameraDirection * maxDist;
        return point;
    }

    Vector3 CalculPointOuAller2()
    {
        Vector3 point = new Vector3(poseDeBase.x+player.transform.position.x + playerDirection2.x * maxDist, poseDeBase.y+player.transform.position.y + playerDirection2.y * maxDist, gameObject.transform.position.z);

        return point;
    }

    void LerpingCam (float t)
    {
        gameObject.transform.position = Vector3.Lerp(new Vector3(postionSave.x + difEntrePlayerPosEtDif().x, postionSave.y + difEntrePlayerPosEtDif().y, gameObject.transform.position.z), CalculPointOuAller2(), t);
    }

    void AddT()
    {
        if(t<1)
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 1;
        }
    }

    void ResetT ()
    {
        t = 0;
    }

    float Distance()
    {
        return Vector2.Distance(player.transform.position,new Vector3( gameObject.transform.position.x - poseDeBase.x,gameObject.transform.position.y- poseDeBase.y));
    }
}


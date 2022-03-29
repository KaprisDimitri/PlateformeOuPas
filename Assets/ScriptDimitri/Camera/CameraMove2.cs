using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove2 : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] PlayerControllerDim playerCOntroler;
    [SerializeField] float maxDistJoueur;
    [SerializeField] float vitesse;
    [SerializeField] float vitesseRetour;

    GameObject camera;

    public Vector3 posDeBase;
    public Vector3 posDeBaseSave;
    float distDeBase;

    float t;

    Vector3 point;

    bool bouge;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        InitCameraControler();

    }

    // Update is called once per frame
    void Update()
    {
        if (!(playerCOntroler.GetComponent<PlayerMovementDim>().WillWalled()))
        {
            gameObject.transform.position = player.transform.position;
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    void InitCameraControler()
    {
        camera = gameObject.transform.GetChild(0).gameObject;
        posDeBase = camera.transform.localPosition;
        posDeBaseSave = camera.transform.localPosition;
        distDeBase = Vector3.Distance(player.transform.position, gameObject.transform.position);
        t = 0;
        point = Vector3.zero;
        bouge = false;
        pos = Vector3.zero;
    }

    void MoveCamera()
    {
        if ((Vector2)playerCOntroler.GetComponent<Rigidbody>().velocity != Vector2.zero && !(playerCOntroler.GetComponent<PlayerMovementDim>().WillWalled()))
        {

            bouge = true;


            if (CalculPointOuAller() != point)
            {
                point = CalculPointOuAller();

                
                posDeBase = camera.transform.localPosition;
                t = 0;

            }
            else
            {

            }
            t += Time.deltaTime / vitesse;
            //Debug.Log(new Vector3(point.x, point.y, posDeBaseSave.z));
            camera.transform.localPosition = Vector3.Lerp(posDeBase, new Vector3(point.x, point.y, posDeBaseSave.z), t);


        }
        else if (t > 0 && !(playerCOntroler.GetComponent<PlayerMovementDim>().WillWalled()))
        {
            if (bouge)
            {
                //posDeBase = player.transform.position;
                pos = camera.transform.localPosition;
                bouge = false;
                point = Vector3.zero;
                t = 1;
            }

            t -= Time.deltaTime / vitesseRetour;
            camera.transform.localPosition = Vector3.Lerp(pos, posDeBaseSave, 1 - t);
        }
        else if ((playerCOntroler.GetComponent<PlayerMovementDim>().WillWalled()))
        {
            t = 0;
            /*if(playerCOntroler.GetComponent<PlayerMovementDim>().wallDirection !=0 &&  camera.transform.localPosition != new Vector3(gameObject.transform.position.x - gameObject.transform.position.x, posDeBaseSave.y, posDeBaseSave.z))
            {
                camera.transform.localPosition =new Vector3(gameObject.transform.position.x- gameObject.transform.position.x, posDeBaseSave.y,posDeBaseSave.z);
            }*/
            
        }

        if (t <= 0)
        {
            t = 0;
            //Debug.Log("yoyoyo");
            //camera.transform.localPosition = posDeBaseSave;

        }
        else if (t > 1)
        {
            t = 1;
        }

        



    }

    Vector3 CalculPointOuAller()
    {
        Vector3 point = (Vector2)posDeBaseSave + ((Vector2)playerCOntroler.GetComponent<Rigidbody>().velocity).normalized * maxDistJoueur;
        return point;
    }

   


}

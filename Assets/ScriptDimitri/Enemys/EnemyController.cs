using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour , IDiferenteWorld
{
    [Header("Script A affilier")]
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] EnemyAction enemyAction;


    [Space]
    [Header("Generale")]
    [SerializeField] float vitesseDeDeplacement;

    [Space]
    [Header("Script A affilier")]
    [SerializeField] GameObject headPlacement;
    [SerializeField] GameObject headObject;


    [Space]
    [Header("Move Point A to B")]

    [SerializeField] bool lerp;
    [SerializeField] bool serlp;
    [Space]
    [SerializeField] Vector2 pointFinishLocal;


    [Space]
    [Header("Move Point Sur List")]

    [SerializeField] bool lerpList;
    [SerializeField] bool serlpList;
    [Space]
    [SerializeField] Vector2[] pointsDeCheminLocal;


    [Space]
    [Header("Move Aleatoire")]
    [SerializeField] bool aleatoire;
    [Space]
    [SerializeField] Vector2[] directions;
    [SerializeField] float tempsAction;


    [Space]
    [Header("Move Collision")]
    [SerializeField] bool colliMove;
    [Space]
    [SerializeField] int layerPourDemiTour;
    [SerializeField] Vector2 directionDeDepart;

    [SerializeField] bool ajoutervie;
    [SerializeField] int degats;
    [SerializeField] float forcerepousse;

    bool canMove;

    [Header("ChoixAction")]
    [SerializeField] public ChoixAction choixActiossn;


    // Start is called before the first frame update
    void Start()
    {
        InitEnemyController();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveEnemy();
        }
    }

    void InitEnemyController()
    {
        enemyMovement.InitEnemyMove(pointFinishLocal, vitesseDeDeplacement, directions, tempsAction, layerPourDemiTour, directionDeDepart, pointsDeCheminLocal);
        if (headObject != null)
        {
            Instantiate(headObject, headPlacement.transform.position, headPlacement.transform.rotation, headPlacement.transform);
        }

        canMove = true;
        ActionEnemy();
    }

    void MoveEnemy()
    {
        if (lerp)
        {
            enemyMovement.EnemyMoveLerp();
        }
        else if (serlp)
        {
            enemyMovement.EnemyMoveSlerp();
        }
        else if (aleatoire)
        {
            enemyMovement.EnemyMoveAleatoire();
        }
        else if (colliMove)
        {
            enemyMovement.EnemyMoveCollision();
        }
        else if (lerpList)
        {
            enemyMovement.EnemyMoveLerpList();
        }
        else if (serlpList)
        {
            enemyMovement.EnemyMoveSlerpList();
        }
    }

    private void OnDrawGizmos()
    {
        if (serlp || lerp)
        {
            Debug.DrawLine(transform.position, pointFinishLocal + (Vector2)transform.position, Color.magenta);
        }
        else if (serlpList || lerpList)
        {
            if (pointsDeCheminLocal.Length != 0)
            {
                Debug.DrawLine((Vector2)transform.position, pointsDeCheminLocal[0] + (Vector2)transform.position, Color.magenta);
                for (int i = 1; i < pointsDeCheminLocal.Length; i++)
                {

                    Debug.DrawLine(pointsDeCheminLocal[i - 1] + (Vector2)transform.position, pointsDeCheminLocal[i] + pointsDeCheminLocal[i - 1] + (Vector2)transform.position, Color.magenta);




                }
            }
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (enemyAction.canDedouble && collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(((collision.gameObject.transform.position - transform.position).normalized).x* forcerepousse, ((collision.gameObject.transform.position - transform.position).normalized).y*forcerepousse, 0);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.gameObject.transform.position - transform.position) * forcerepousse);
            collision.gameObject.GetComponent<Health>().AddRemoveHearth(degats, ajoutervie);
            StopCoroutine(CoroutineConmove());
            canMove = false;
            StartCoroutine(CoroutineConmove());
        }
    }

    void ActionEnemy ()
    {
        switch(choixActiossn)
        {
            case ChoixAction.dedoublement:
                enemyAction.dedoublement = true;
                break;
        }
    }

    IEnumerator CoroutineConmove()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

   public enum ChoixAction
        {
        aucun,
        dedoublement,
        }

    public void SetInvisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -15);

        if (gameObject.transform.childCount != 0)
        {
           // gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void SetVisible()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (gameObject.transform.childCount != 0)
        {
          //  gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

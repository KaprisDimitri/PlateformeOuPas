using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeController : MonoBehaviour
{
    [Header("Script A affilier")]
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] PlateformeAction plateformeAction;
    public  GameObject player;

    [Space]
    [Header("Generale")]
    [SerializeField] float vitesseDeDeplacement;
    bool inMove;
    [SerializeField] ChoixMouvement choixMouvement;
    [SerializeField] ChoixActioMove choixActionsMove;
    [SerializeField] ChoixActionAutre choixActionAutre;

    [Space]
    [Header("Move Point A to B")]
    [Space]
    [SerializeField] Vector2 pointFinishLocal;


    [Space]
    [Header("Move Point Sur List")]
    [Space]
    [SerializeField] Vector2[] pointsDeCheminLocal;


    [Space]
    [Header("Move Aleatoire")]
    [Space]
    [SerializeField] Vector2[] directions;
    [SerializeField] float tempsAction;


    [Space]
    [Header("Move Collision")]
    [Space]
    [SerializeField] int layerPourDemiTour;
    [SerializeField] Vector2 directionDeDepart;

    [Space]
    [Header("Joueur Reste SurP PlateForme")]
    [SerializeField] bool enfant;
    [SerializeField] int layerPourPlayer;
    bool playerSurPlateforme;

    [Space]
    [Header("Action Quand Joueur est sur PlateForme")]
    [SerializeField] bool actionSurPlateforme;

    [Space]
    [Header("(Action) Move Point A to B")]
    [Space]
    [SerializeField] Vector2 actionPointFinishLocal;


    [Space]
    [Header("(Action) Move Point Sur List")]
    [Space]
    [SerializeField] Vector2[] actionPointsDeCheminLocal;


    [Space]
    [Header("(Action) Move Aleatoire")]
    [Space]
    [SerializeField] Vector2[] actionDirections;
    [SerializeField] float actionTempsAction;


    [Space]
    [Header("(Action) Move Collision")]
    [Space]
    [SerializeField] int actionLayerPourDemiTour;
    [SerializeField] Vector2 actionDirectionDeDepart;

    [Space]
    [Header("(Action) Destroy")]
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BoxCollider col;
    [SerializeField] BoxCollider trig;
    [SerializeField] float timeBeforeDestroy;
    bool destroyed;



    // Start is called before the first frame update
    void Start()
    {
        
        
        InitPlateformeController();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (actionSurPlateforme)
        {
            if(playerSurPlateforme)
            {
                ActionSurPlateformeMove();
                if(!destroyed)
                {
                   ActionSurPlatefome();
                }
            }
            else
            {
                MovePlateforme();
            }
        }
        else
        {
            MovePlateforme();
        }
        
        
    }

    void InitPlateformeController()
    {
        this.inMove = true;
        enemyMovement.InitEnemyMove(pointFinishLocal, vitesseDeDeplacement, directions, tempsAction, layerPourDemiTour, directionDeDepart, pointsDeCheminLocal);
        
    }

    void MovePlateforme()
    {
        if (!inMove)
        {
            inMove = enemyMovement.RetrounerPointInitial();
            if(inMove)
            {
                plateformeAction.EnableView(mesh, col, trig, true);
            }

        }
        else
        {
            switch(choixMouvement)
            {
                case ChoixMouvement.lerp:
                    enemyMovement.EnemyMoveLerp();
                    break;
                case ChoixMouvement.slerp:
                    enemyMovement.EnemyMoveSlerp();
                    break;
                case ChoixMouvement.lerpList:
                    enemyMovement.EnemyMoveLerpList();
                    break;
                case ChoixMouvement.slerpList:
                    enemyMovement.EnemyMoveSlerpList();
                    break;
                case ChoixMouvement.aleatoire:
                    enemyMovement.EnemyMoveAleatoire();
                    break;
                case ChoixMouvement.collision:
                    enemyMovement.EnemyMoveCollision();
                    break;

            }
        }
        
    }

    void ActionSurPlateformeMove ()
    {
            switch (choixActionsMove)
            {
                case ChoixActioMove.actionLerp:
                    enemyMovement.EnemyMoveLerp();
                    break;
                case ChoixActioMove.actionSlerp:
                    enemyMovement.EnemyMoveSlerp();
                    break;
                case ChoixActioMove.actionLerpList:
                    enemyMovement.EnemyMoveLerpList();
                    break;
                case ChoixActioMove.actionSlerpList:
                    enemyMovement.EnemyMoveSlerpList();
                    break;
                case ChoixActioMove.actionAleatoire:
                    enemyMovement.EnemyMoveAleatoire();
                    break;
                case ChoixActioMove.actionCollision:
                    enemyMovement.EnemyMoveCollision();
                    break;
            }
    }

    void ActionSurPlatefome()
    {
        if((choixActionAutre & ChoixActionAutre.destruction) == ChoixActionAutre.destruction)
        {
            
            destroyed = plateformeAction.PlatefomeDestroy(timeBeforeDestroy, mesh, col, trig);

            if (destroyed)
            {
                
                if (enfant && playerSurPlateforme)
                {
                    player.transform.parent = null;
                }
                if (playerSurPlateforme)
                {
                    playerSurPlateforme = false;
                }
                enemyMovement.ResetEnemyMove(pointFinishLocal, vitesseDeDeplacement, directions, tempsAction, layerPourDemiTour, directionDeDepart, pointsDeCheminLocal);
                destroyed = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if ( choixMouvement == ChoixMouvement.lerp || choixMouvement == ChoixMouvement.slerp)
        {
            Debug.DrawLine(transform.position, pointFinishLocal + (Vector2)transform.position, Color.magenta);
        }
        else if (choixMouvement == ChoixMouvement.lerpList || choixMouvement == ChoixMouvement.lerpList)
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

        if (choixActionsMove == ChoixActioMove.actionLerp || choixActionsMove == ChoixActioMove.actionSlerp)
        {
            Debug.DrawLine(transform.position, actionPointFinishLocal + (Vector2)transform.position, Color.blue);
        }

        if (choixActionsMove == ChoixActioMove.actionLerpList || choixActionsMove == ChoixActioMove.actionSlerpList)
        {
            if (actionPointsDeCheminLocal.Length != 0)
            {
                Debug.DrawLine((Vector2)transform.position, actionPointsDeCheminLocal[0] + (Vector2)transform.position, Color.blue);
                for (int i = 1; i < actionPointsDeCheminLocal.Length; i++)
                {

                    Debug.DrawLine(actionPointsDeCheminLocal[i - 1] + (Vector2)transform.position, actionPointsDeCheminLocal[i] + actionPointsDeCheminLocal[i - 1] + (Vector2)transform.position, Color.blue);

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enfant)
        {
            if(other.gameObject.layer == layerPourPlayer)
            {
                player = other.gameObject;
                other.gameObject.transform.parent = gameObject.transform;
            }
        }

        if(actionSurPlateforme)
        {
            if (other.gameObject.layer == layerPourPlayer)
            {
                playerSurPlateforme = true;
                inMove = false;
                enemyMovement.ChangeEnemyMove(actionPointFinishLocal, vitesseDeDeplacement, actionDirections, actionTempsAction, actionLayerPourDemiTour, actionDirectionDeDepart, actionPointsDeCheminLocal);
            }
        }

        if (other.gameObject.layer == layerPourPlayer)
        {
           //ActionSurPlatefome();
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (enfant)
        {
            if (other.gameObject.layer == layerPourPlayer)
            {
                
                other.gameObject.transform.parent = null;
            }
        }

        if (actionSurPlateforme)
        {
            if (other.gameObject.layer == layerPourPlayer)
            {
                playerSurPlateforme = false;
                enemyMovement.ResetEnemyMove(pointFinishLocal, vitesseDeDeplacement, directions, tempsAction, layerPourDemiTour, directionDeDepart, pointsDeCheminLocal);
            }
        }
    }

    

    enum ChoixMouvement
    {
        lerp,
        slerp,
        lerpList,
        slerpList,
        aleatoire,
        collision,
        aucun,
    }

    enum ChoixActioMove
    {
        aucun,
        actionLerp,
        actionSlerp,
        actionLerpList,
        actionSlerpList,
        actionAleatoire,
        actionCollision,
    }

    [Flags]
    enum ChoixActionAutre
    {
        aucun,
        destruction,
        active,
    }
}

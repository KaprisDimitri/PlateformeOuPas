using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Generale
    float vitesseDeplacement;
    Vector2 pointInitiale;
    Vector2 pointActu;

    //Move pour Lerp et Slerp (Point A a Point B)
    Vector2 pointDepart;
    Vector2 pointFinishLocal;
    float t;
    bool augmente;

    //Move pour LerpList et SlerpList
    Vector2[] pointsDeCheminLocal;
    int prochainPoint;
    int actuelPoint;


    //Move pour Aleatoire 
    Vector2[] directions;
    Vector2 direction;
    float timeAction;
    bool finishMove;

    //Move pour Collision
    bool collisionActive;
    int layerPourDemiTour;
    Vector2 directionDeDepart;




    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitEnemyMove(Vector2 pointFinishLocal, float vitesseDeplacement, Vector2[] directions, float timeAction, int layerPourDemiTour, Vector2 directionDeDepart, Vector2[] pointsDeCheminLocal)
    {
        // generale
        this.vitesseDeplacement = vitesseDeplacement;
        this.pointInitiale = transform.position;


        //Pour lerpSlerp
        pointDepart = transform.position;
        this.pointFinishLocal = pointFinishLocal + pointDepart;
        t = 0;
        augmente = true;
        //----------

        //Move pour LerpList et SlerpList
        prochainPoint = 1;
        actuelPoint = 0;

        this.pointsDeCheminLocal = new Vector2[pointsDeCheminLocal.Length + 1];
        this.pointsDeCheminLocal[0] = transform.position;
        for (int i = 0; i < pointsDeCheminLocal.Length; i++)
        {
            this.pointsDeCheminLocal[i + 1] = pointsDeCheminLocal[i] + this.pointsDeCheminLocal[i];
        }
        //----------

        //Pour Aleatoire
        this.directions = directions;
        finishMove = true;
        this.timeAction = timeAction;
        //----------

        //Pour Collision
        this.directionDeDepart = directionDeDepart;
        this.layerPourDemiTour = layerPourDemiTour;

    }

    public void ResetEnemyMove(Vector2 pointFinishLocal, float vitesseDeplacement, Vector2[] directions, float timeAction, int layerPourDemiTour, Vector2 directionDeDepart, Vector2[] pointsDeCheminLocal)
    {
        pointActu = transform.position;
        //Pour lerpSlerp
        pointDepart = pointInitiale;
        this.pointFinishLocal = pointFinishLocal + pointDepart;
        t = 0;
        augmente = true;
        //----------

        //Move pour LerpList et SlerpList
        prochainPoint = 1;
        actuelPoint = 0;

        this.pointsDeCheminLocal = new Vector2[pointsDeCheminLocal.Length + 1];
        this.pointsDeCheminLocal[0] = pointDepart;
        for (int i = 0; i < pointsDeCheminLocal.Length; i++)
        {
            this.pointsDeCheminLocal[i + 1] = pointsDeCheminLocal[i] + this.pointsDeCheminLocal[i];
        }
        //----------

        //Pour Aleatoire
        this.directions = directions;
        finishMove = true;
        this.timeAction = timeAction;
        //----------

        //Pour Collision
        this.directionDeDepart = directionDeDepart;
        this.layerPourDemiTour = layerPourDemiTour;
    }

    public void ChangeEnemyMove(Vector2 pointFinishLocal, float vitesseDeplacement, Vector2[] directions, float timeAction, int layerPourDemiTour, Vector2 directionDeDepart, Vector2[] pointsDeCheminLocal)
    {




        //Pour lerpSlerp
        pointDepart = transform.position;
        this.pointFinishLocal = pointFinishLocal + pointDepart;
        t = 0;
        augmente = true;
        //----------

        //Move pour LerpList et SlerpList
        prochainPoint = 1;
        actuelPoint = 0;

        this.pointsDeCheminLocal = new Vector2[pointsDeCheminLocal.Length + 1];
        this.pointsDeCheminLocal[0] = transform.position;
        for (int i = 0; i < pointsDeCheminLocal.Length; i++)
        {
            this.pointsDeCheminLocal[i + 1] = pointsDeCheminLocal[i] + this.pointsDeCheminLocal[i];
        }
        //----------

        //Pour Aleatoire
        this.directions = directions;
        finishMove = true;
        this.timeAction = timeAction;
        //----------

        //Pour Collision
        this.directionDeDepart = directionDeDepart;
        this.layerPourDemiTour = layerPourDemiTour;

    }

    public bool RetrounerPointInitial()
    {
        t = ChangeTValue(t, true, vitesseDeplacement);

        MoveLerp(pointActu, pointInitiale, t);
        if (t == 1)
        {
            t = 0;
            return true;
        }
        return false;
    }

    // Fonction Pour Le Movement Ler Slerp

    public void EnemyMoveLerp()
    {
        t = ChangeTValue(t, augmente, vitesseDeplacement);
        augmente = ChangeWay(augmente, t);
        MoveLerp(pointDepart, pointFinishLocal, t);
    }

    public void EnemyMoveSlerp()
    {
        t = ChangeTValue(t, augmente, vitesseDeplacement);
        augmente = ChangeWay(augmente, t);
        MoveSlerp(pointDepart, pointFinishLocal, t);
    }

    public void EnemyMoveLerpList()
    {
        t = ChangeTValue(t, augmente, ChangeVitesseDeplacement(Vector2.Distance(pointsDeCheminLocal[0], pointsDeCheminLocal[1]), pointsDeCheminLocal[actuelPoint], pointsDeCheminLocal[prochainPoint], vitesseDeplacement));
        prochainPoint = ChangeProchainPoint(t, prochainPoint);
        MoveLerp(pointsDeCheminLocal[actuelPoint], pointsDeCheminLocal[prochainPoint], t);
    }

    public void EnemyMoveSlerpList()
    {
        t = ChangeTValue(t, augmente, ChangeVitesseDeplacement(Vector2.Distance(pointsDeCheminLocal[0], pointsDeCheminLocal[1]), pointsDeCheminLocal[actuelPoint], pointsDeCheminLocal[prochainPoint], vitesseDeplacement));
        prochainPoint = ChangeProchainPoint(t, prochainPoint);
        MoveSlerp(pointsDeCheminLocal[actuelPoint], pointsDeCheminLocal[prochainPoint], t);
    }

    bool ChangeWay(bool augmente, float t)
    {
        if (t == 0 && !augmente)
        {
            return true;
        }
        else if (t == 1 && augmente)
        {
            return false;
        }

        return augmente;
    }

    float ChangeTValue(float t, bool augmente, float vitesseEnSeconde)
    {
        if (augmente)
        {
            t += Time.deltaTime / vitesseEnSeconde;
        }
        else
        {
            t -= Time.deltaTime / vitesseEnSeconde;
        }

        if (t < 0)
        {
            return 0;
        }
        else if (t > 1)
        {
            return 1;
        }

        return t;
    }

    int ChangeProchainPoint(float t, int prochainPoint)
    {
        if (t >= 1)
        {
            actuelPoint = prochainPoint;
            prochainPoint++;
            this.t = 0;
        }
        if (prochainPoint >= pointsDeCheminLocal.Length)
        {
            prochainPoint = 0;
        }
        return prochainPoint;
    }

    float ChangeVitesseDeplacement(float distanceRef, Vector2 pointDeChemainActuel, Vector2 pointDeChemainProchain, float vitesseDeplacement)
    {
        float distComparer = Vector2.Distance(pointDeChemainActuel, pointDeChemainProchain);
        return vitesseDeplacement / (distanceRef / distComparer);
    }


    void MoveLerp(Vector2 pointDepart, Vector2 pointFinish, float t)
    {
        gameObject.transform.position = Vector2.Lerp(pointDepart, pointFinish, t);
    }

    void MoveSlerp(Vector2 pointDepart, Vector2 pointFinish, float t)
    {
        gameObject.transform.position = Vector3.Slerp(pointDepart, pointFinish, t);

    }



    Vector2 DirectionMoveEnemySlerpLerp(Vector2 pointDepart, Vector2 pointFinish)
    {
        return pointFinish - pointDepart;
    }



    // Fonction Pour Le Movement Aleatoire

    public void EnemyMoveAleatoire()
    {
        collisionActive = true;
        if (finishMove)
        {
            StopCoroutine(CoroutineMoveAleatoire(timeAction));
            direction = ChoixDirectionAleatoire(directions);
            finishMove = false;
            StartCoroutine(CoroutineMoveAleatoire(timeAction));
        }
        else
        {

            MoveTranslate(direction, vitesseDeplacement);
        }


    }

    void MoveTranslate(Vector2 direction, float vitesseDeplacement)
    {
        gameObject.transform.Translate(direction * vitesseDeplacement * Time.deltaTime);
    }

    Vector2 ChoixDirectionAleatoire(Vector2[] direction)
    {
        int rand = Random.Range(0, direction.Length + 1);
        if (rand == direction.Length)
        {
            return Vector2.zero;
        }
        return direction[rand];
    }

    IEnumerator CoroutineMoveAleatoire(float timeAction)
    {
        yield return new WaitForSeconds(timeAction);
        finishMove = true;
    }

    // Fonction Pour Le Movement Collision Inverse

    public void EnemyMoveCollision()
    {
        collisionActive = true;
        MoveTranslate(directionDeDepart, vitesseDeplacement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionActive)
        {


            if (collision.gameObject.layer == layerPourDemiTour || collision.gameObject.layer == gameObject.layer || collision.gameObject.layer == 7)
            {
                directionDeDepart *= -1;
                direction *= -1;
            }
        }
    }
}
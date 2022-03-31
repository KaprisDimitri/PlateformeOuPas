using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] PlayerMovementDim playerMovement;
    public static int health = 9;
    public int numberOfHearths;

    public Image[] hearths;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    public LevelManager levelManager;

    private void Start()
    {
        AddRemoveHearth(0, true);
    }
    public void Update()
    {
       
    }

    public void AddRemoveHearth (int vie, bool active)
    {
        if(active)
        {
            health += vie;
        }
        else
        {
            StopCoroutine(CoroutineConMove());
            playerMovement.canMove = false;
            health -= vie;
            StartCoroutine(CoroutineConMove());
        }
        if (health > numberOfHearths)
        {
            
            health = numberOfHearths;
            
        }

        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < health)
            {
                hearths[i].sprite = fullHearth;
            }
            else
            {
                
                hearths[i].sprite = emptyHearth;
            }

            if (i < numberOfHearths)
            {
                hearths[i].enabled = true;
            }
            else
            {
                hearths[i].enabled = false;
            }
        }

        if(health == 0)
        {
            Dead();
        }
    }

    IEnumerator CoroutineConMove ()
    {
        yield return new WaitForSeconds(0.5f);
        playerMovement.canMove = true;
    }

    void Dead()
    {
        levelManager.PutPlayerAtLastCheckPoint(gameObject);
    }
}
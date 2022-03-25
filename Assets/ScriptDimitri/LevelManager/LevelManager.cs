using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject playerSpawn;
    [SerializeField] GameObject finish;

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] checkPoints;
    GameObject playerInGame;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Health>().levelManager = GetComponent<LevelManager>();
        //InitLevel();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerFinish(finish.transform.position);
    }

    void InitLevel()
    {
        playerInGame = SpawnPlayer(player);
    }

    GameObject SpawnPlayer(GameObject player)
    {
        return Instantiate(player, playerSpawn.transform.position, Quaternion.identity, playerSpawn.transform);
    }

    private void OnDrawGizmos()
    {
        if (playerSpawn != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerSpawn.transform.position, 1);
        }
        if (finish != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(finish.transform.position, 1);
        }
    }



    void CheckIfPlayerFinish(Vector2 center)
    {
        Collider[] col = Physics.OverlapSphere(center, 1);
        if (col.Length > 0)
        {

            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].gameObject.layer == 7)
                {

                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                }
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            PutPlayerAtLastCheckPoint(other.gameObject);
        }
    }

    public void PutPlayerAtLastCheckPoint(GameObject other)
    {
        int index = FintTheGoodCheckPoint();
        if (index == -1)
        {
            other.transform.position = playerSpawn.transform.position;
        }
        else
        {
            other.transform.position = checkPoints[index].transform.position;
        }
        player.GetComponent<Health>().AddRemoveHearth(1000, true);
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    int FintTheGoodCheckPoint()
    {
        int t = -1;
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (!(checkPoints[i].GetComponent<CheckPointController>().check))
            {
                break;
            }
            else if (i == checkPoints.Length - 1 && checkPoints[i].GetComponent<CheckPointController>().check)
            {
                return i;
            }
            else if (checkPoints[i].GetComponent<CheckPointController>().check && !(checkPoints[i + 1].GetComponent<CheckPointController>().check))
            {
                return i;
            }
        }

        return t;
    }
}
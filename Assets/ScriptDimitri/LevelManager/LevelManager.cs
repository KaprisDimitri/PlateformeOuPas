using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class LevelManager : MonoBehaviour
{
    [SerializeField] WorldController worldA;
    [SerializeField] WorldController worldB;
    [SerializeField] float vistesseChangement;
    [SerializeField]PostProcessVolume colorGrading;
    
    int startingWorld;

    [SerializeField] GameObject playerSpawn;
    [SerializeField] GameObject finish;

    [SerializeField] GameObject player;
    [SerializeField] GameObject Cam;
    [SerializeField] GameObject[] checkPoints;
    GameObject playerInGame;


    float couleurInt;
    bool swipe;
    bool cantswipe;

    private void Awake()
    {
        InitLevel();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Health>().levelManager = GetComponent<LevelManager>();
        couleurInt = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !swipe)
        {
            swipe = true;
            
        }

        if(swipe)
        {
            ChangeSaturation();
        }

        CheckIfPlayerFinish(finish.transform.position);
    }

    void InitLevel()
    {
        startingWorld = 2;
        ChangeWorld();
        //worldB.SetAllInvisible();
        //worldA.SetAllVisible();
       // playerInGame = SpawnPlayer(player);
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
        Debug.Log(center);
        Collider[] col = Physics.OverlapSphere(center , 1);
        if (col.Length > 0)
        {
           
            for (int i = 0; i < col.Length; i++)
            {
                Debug.Log(col[i].gameObject.name);
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
        //Debug.Log(playerSpawn.transform.position);
        int index = FintTheGoodCheckPoint();
        if(index == -1)
        {
            other.transform.position = playerSpawn.transform.position;
        }
        else
        {
            other.transform.position = checkPoints[index].transform.position;
        }
        player.GetComponent<Health>().AddRemoveHearth(1000, true);
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Cam.transform.position = player.transform.position;
        Debug.Log(other.transform.position);
        Debug.Log(other.name);

    }

    int FintTheGoodCheckPoint()
    {
        int t = -1;
        for(int i = 0; i<checkPoints.Length;i++)
        {
            if(!(checkPoints[i].GetComponent<CheckPointController>().check))
            {
                break;
            }
            else if(i == checkPoints.Length-1 && checkPoints[i].GetComponent<CheckPointController>().check)
            {
                return i;
            }
            else if(checkPoints[i].GetComponent<CheckPointController>().check && !(checkPoints[i+1].GetComponent<CheckPointController>().check))
            {
                return i;
            }
        }

        return t;
    }

    bool CheckIfCanSwipe ()
    {
        bool active = true;
        if (startingWorld == 1)
        {
            for (int i = 0; i < worldB.decor.transform.childCount;i++)
            {
                active = worldB.decor.transform.GetChild(i).GetComponent<GroundController>().CheckIfCanSwipe();
                Debug.Log(active + "active  worldB" + worldB.decor.transform.GetChild(i).name);
                if(!active)
                {
                    break;
                }
            }
            return active;
        }
        else
        {
            for (int i = 0; i < worldA.decor.transform.childCount; i++)
            {
                active = worldA.decor.transform.GetChild(i).GetComponent<GroundController>().CheckIfCanSwipe();
                Debug.Log(active + "active  worldB");
                if (!active)
                {
                    break;
                }
            }
            return active;
        }
    }

    void ChangeSaturation ()
    {
       // Debug.Log(couleurInt);
        
        if (startingWorld == 1)
        {
            if (colorGrading.profile.TryGetSettings<ColorGrading>(out var bloom) )
            {
                if (!cantswipe)
                {
                    couleurInt -= (Time.deltaTime / vistesseChangement)*100;
                    if (couleurInt <= -50)
                    {
                        couleurInt = -50;

                        if (CheckIfCanSwipe())
                        {
                            ChangeWorld();
                            swipe = false;
                        }
                        else
                        {
                            Debug.Log("yoyoyooyo");
                            cantswipe = true;
                        }

                    }
                }
                else
                {
                    couleurInt += ((Time.deltaTime / vistesseChangement)*100)*2;
                    if (couleurInt >= 0)
                    {
                        couleurInt = 0;
                        swipe = false;
                        cantswipe = false;
                    }
                }
                    bloom.saturation.value = couleurInt;

            }
        }
        else
        {
            if (colorGrading.profile.TryGetSettings<ColorGrading>(out var bloom))
            {
                if (!cantswipe)
                {
                    couleurInt += (Time.deltaTime / vistesseChangement)*100;
                    if (couleurInt >= 0)
                    {
                        couleurInt = 0;
                        if (CheckIfCanSwipe())
                        {
                            ChangeWorld();
                            swipe = false;
                        }
                        else
                        {
                            cantswipe = true;
                        }

                    }
                }
                else
                {
                    couleurInt -= ((Time.deltaTime / vistesseChangement)*100)*2;
                    if (couleurInt <= -50)
                    {
                        couleurInt = -50;
                        swipe = false;
                        cantswipe = false;
                    }
                }
                bloom.saturation.value = couleurInt;

            }
        }
    }

    void ChangeWorld ()
    {
        if(startingWorld == 1)
        {
            startingWorld = 2;

            
            worldB.SetAllVisible();
            worldA.SetAllInvisible();
        }
        else
        {
            startingWorld = 1;
            if (colorGrading.profile.TryGetSettings<ColorGrading>(out var bloom))
            {
                bloom.saturation.value = 0;

            }
            worldA.SetAllVisible();
            worldB.SetAllInvisible();
        }


    }
}
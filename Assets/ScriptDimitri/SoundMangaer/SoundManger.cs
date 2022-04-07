using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    [SerializeField] List<AudioClip> listSon; // A changer en scriptable
    // il faudra des scriptable pour les reglage

    List<AudioSource> listAudioSource;
    List<AudioSource> listAudioSourceEnCours;

    public delegate void PlayerSound(int i, Vector3 pos);
    public static PlayerSound playSound;
    private void Awake()
    {
        Debug.Log("SoundManagerCeReveille");
    }
    // Start is called before the first frame update
    void Start()
    {
        InitSoundManger();


    }
    
    void InitSoundManger ()
    {
        playSound += PlaySound;
        listAudioSourceEnCours = new List<AudioSource>();
        listAudioSource = new List<AudioSource>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            listAudioSource.Add(transform.GetChild(i).GetComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChexkIsPlaying();
    }

    void ChexkIsPlaying ()
    {
        for(int i = 0; i< listAudioSourceEnCours.Count;i++)
        {
            if(!(listAudioSourceEnCours[i].isPlaying))
            {
                listAudioSource.Add(listAudioSourceEnCours[i]);
                putToOtherList(i);
            }
        }
    }

    void putToOtherList (int index)
    {
        List<AudioSource> listAudioSourceSave = listAudioSourceEnCours;
        listAudioSourceEnCours = new List<AudioSource>();
        for (int i = 0; i < listAudioSourceSave.Count; i++)
        {
            if (i != index)
            {
                listAudioSourceEnCours.Add(listAudioSourceSave[i]);
            }
        }
    }

    void PlaySound (int index, Vector3 pos)
    {
        if(listAudioSource.Count !=0)
        {
            listAudioSource[listAudioSource.Count - 1].gameObject.transform.position = pos;
            listAudioSource[listAudioSource.Count - 1].clip = listSon[index];
            listAudioSource[listAudioSource.Count - 1].Play();
            listAudioSourceEnCours.Add(listAudioSource[listAudioSource.Count - 1]);
            List<AudioSource> listAudioSourceSave = listAudioSource;
            listAudioSource = new List<AudioSource>();
            for (int i = 0; i < listAudioSourceSave.Count-1; i++)
            {

                listAudioSource.Add(listAudioSourceSave[i]);
                
            }
        }
        else
        {
            if(listAudioSourceEnCours.Count !=0)
            {
                GameObject source = Instantiate(listAudioSourceEnCours[0].gameObject, gameObject.transform.position, Quaternion.identity, transform);
                listAudioSource.Add(source.GetComponent<AudioSource>());
                listAudioSource[listAudioSource.Count - 1].gameObject.transform.position = pos;
                listAudioSource[listAudioSource.Count - 1].clip = listSon[index];
                listAudioSource[listAudioSource.Count - 1].Play();
                listAudioSourceEnCours.Add(listAudioSource[listAudioSource.Count - 1]);
                List<AudioSource> listAudioSourceSave = listAudioSource;
                listAudioSource = new List<AudioSource>();
                for (int i = 0; i < listAudioSourceSave.Count - 1; i++)
                {

                    listAudioSource.Add(listAudioSourceSave[i]);

                }
            }
        }
    }
}

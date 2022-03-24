using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 initPlace;
    // Start is called before the first frame update
    void Start()
    {
        initPlace = gameObject.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x + initPlace.x, player.transform.position.y + initPlace.y, initPlace.z);
    }
}

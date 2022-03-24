using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingHearth : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject hearthPrefab;
    void Update()
    {
        /*hearthPrefab.SetActive(true);
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            Instantiate(hearthPrefab, shootingPoint.position, transform.rotation);
            Health.health--;
        }*/
    }
}
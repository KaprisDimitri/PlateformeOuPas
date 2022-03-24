using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHearth : MonoBehaviour
{
    float pvTime = 1;
    public float speed = 10;
    private Rigidbody2D _rb;
    Health health;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.position * speed;
    }

    void Update()
    {
        pvTime -= Time.deltaTime;
        if (pvTime <= 0)
        {
            Debug.Log("PD");
            this.gameObject.SetActive(false);
        }
    }
}
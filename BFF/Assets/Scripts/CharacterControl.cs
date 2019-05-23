﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    [Tooltip("How fast should the player move('units'/second)")]
    [Range(1, 100)]
    public int playerSpeed = 10;
    
    private bool playerDead = false;
    public bool PlayerDead { get; }
    

    public Image HealthGauge;
    public float playerHealth = 100;

    public GameObject DeathParticleSys;

    // Start is called before the first frame update
    void Start()
    {
        HealthGauge = HealthGauge.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDead)
        {
            //Debug.Log(playerHealth);
            HealthGauge.fillAmount = playerHealth * 0.01f;

            //playerHealth -= 5 * Time.deltaTime;
            if (HealthGauge.fillAmount <= 0) playerDead = true;
        }
        else
        {
            Instantiate(DeathParticleSys, transform.position, Quaternion.identity);
            //Pass info to end game screen

            Destroy(this);
        }
        Vector3 prevPos = transform.position;
        playerMove();

        Vector3 aimDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(aimDir.x, aimDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        if (transform.position != prevPos)
        {
            GameObject cam = GameObject.Find("Main Camera");
            Vector3 temp = cam.transform.position;
            temp.x = transform.position.x;
            cam.transform.position = temp;
            HealthGauge.transform.position -= prevPos - transform.position;
        }



    }

    void playerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
    }
    

}

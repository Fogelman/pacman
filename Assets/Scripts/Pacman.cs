﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{

    public float speed = 8;
    private bool energized;
    public float delay = 3;
    // Start is called before the first frame update
    void Start()
    {
        energized = false;
    }

    void Update()
    {
        // Get user input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.Rotate(0, x * speed * 10 * Time.deltaTime, 0);

        Vector3 move = transform.forward * z;
        gameObject.GetComponent<CharacterController>().Move(move * Time.deltaTime * speed);

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pellet")
        {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Energizer")
        {
            Destroy(other.gameObject);
            StartCoroutine(Energizer());
        }
        else if(other.gameObject.tag == "Ghost")
        {
            if (energized)
                Destroy(other.gameObject);
            else
                Destroy(gameObject);

        }
    }

    IEnumerator Energizer()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        energized = true;
        foreach (GameObject ghost in ghosts)
        {
            ghost.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        yield return new WaitForSeconds(delay);
        energized = false;
        foreach (GameObject ghost in ghosts)
        {
            ghost.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }


    }
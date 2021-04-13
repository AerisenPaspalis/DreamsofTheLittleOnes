﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    public GameObject[] pickUps;
    public int index;
    public float waitTime = 2f;

    public Animator hammerAnimations;
    public GameObject Hammer;

    CharacterMechanics character;



   
    
    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        character = Player.GetComponent<CharacterMechanics>();
    }

    private void Update()
    {
        index = Random.Range(0, pickUps.Length);

    

    }
    //private void OnMouseDown()
    //{


    //    GameObject randomPickup = Instantiate(pickUps[index], transform.position, transform.rotation);
    //    GameObject brokenVersion = Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);
    //    brokenVersion.transform.localScale = new Vector3(2, 2, 2);
    //    Object.Destroy(brokenVersion, 5f);

    //    Destroy(gameObject);

    //    randomPickup.transform.localScale = new Vector3(1, 1, 1);

    //    Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);

    //}

    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Hammer")
        //{
        //    if (character.isAttacking)
        //    {
        //        Debug.Log("Working Hammer");
        //        GameObject randomPickup = Instantiate(pickUps[index], transform.position, transform.rotation);
        //        GameObject brokenVersion = Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);
        //        brokenVersion.transform.localScale = new Vector3(2, 2, 2);
        //        Object.Destroy(brokenVersion, 5f);

        //        Destroy(gameObject);

        //        //randomPickup.transform.localScale = new Vector3(1, 1, 1);

        //        // Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);
        //    }
        //}

      


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HammerSmashAOE")
        {
            GameObject randomPickup = Instantiate(pickUps[index], transform.position, transform.rotation);
            GameObject brokenVersion = Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);
            brokenVersion.transform.localScale = new Vector3(2, 2, 2);
            Object.Destroy(brokenVersion, 5f);

            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "WhirlwindAOE")
        {
            GameObject randomPickup = Instantiate(pickUps[index], transform.position, transform.rotation);
            GameObject brokenVersion = Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);
            brokenVersion.transform.localScale = new Vector3(2, 2, 2);
            Object.Destroy(brokenVersion, 5f);

            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Hammer")
        {
            if (character.isAttacking)
            {
                Debug.Log("Working Hammer");
                GameObject randomPickup = Instantiate(pickUps[index], transform.position, transform.rotation);
                GameObject brokenVersion = Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);
                brokenVersion.transform.localScale = new Vector3(2, 2, 2);
                Object.Destroy(brokenVersion, 5f);

                Destroy(gameObject);

                //randomPickup.transform.localScale = new Vector3(1, 1, 1);

                // Instantiate(destroyedVersion, transform.position, transform.rotation, destroyedVersion.transform.parent);
            }
        }
    }

}

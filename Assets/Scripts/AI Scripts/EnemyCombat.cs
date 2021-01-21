﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

//Combat Script
public class EnemyCombat : MonoBehaviour
{
    //Connect EnemyAI1 script with EnemyCombat script
    EnemyAI1 EnemyAI1Script;

    [SerializeField] int hp = 5;
    private int maxHP;
    public Rigidbody rb;
    public Transform target;
    [SerializeField] float knockDistanceModifier;
    [SerializeField] float knockHeightModifier;
    [SerializeField] float knockDuration;
    [SerializeField] float knockPause;

    NavMeshAgent agent;

    [SerializeField] private Image hpBar;

    //Death
    public bool death = false;

    CharacterMechanics cm;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAI1Script = gameObject.GetComponent<EnemyAI1>();
        
        rb = GetComponent<Rigidbody>();

        //sets maxHP to beginning hp in order to get the correct fill amount for hpbar
        int maxHP = hp;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;

        cm = GameObject.Find("Player").GetComponent<CharacterMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets hp text to change based on players perspective
        //So it's not backwards to the player
        Vector3 textDirection = transform.position - target.transform.position;

        if (agent.enabled)
        {
            //hpBar.transform.rotation = Quaternion.LookRotation(textDirection);
            //transform.rotation = Quaternion.LookRotation(textDirection);
        }
        
        //Detect when there is no HP to kill enemy and play death animation
        if (hp <= 0)
        {
            death = true;
            Debug.Log("Enemy has been killed");
            agent.isStopped = true;
            AgentStop();
        }

        //Used for testing enemy death
        if (Input.GetKeyDown("t"))
        {
            Debug.Log("Enemy has lost 1 hp");
            hp -= 1;
        }
    }
    public void takeDamage(int dmg)
    {
        //Debug.Log(dmg + "Damage Taken");
        agent.isStopped = true;
        hp -= dmg;
        if(hp <= 0)
        {
            death = true;
            //Destroy(gameObject);   Destroy object is called in EnemyAI1 when the death animation is played
        }
        hpBar.fillAmount = (float)(hp * 0.2);

        //KNOCKBACK
        // Gets the difference between enemy and player position
        // To knockback enemy away from player
        rb.isKinematic = false;
        //agent.enabled = false;
        rb.AddForce(-transform.forward * knockDistanceModifier);
        //rb.AddForce(transform.up * knockHeightModifier);
        
        Debug.Log("Knockback");
        //Invokes once enemy is no longer being knocked back and pauses movement
        Invoke("AgentStop", knockDuration);
    }
    private void AgentStop()
    {
        //Enemy briefly pauses after being knocked back
        //Where it's velocity is 0
        Invoke("AgentStart", knockPause);
    }
    private void AgentStart()
    {
        rb.isKinematic = true;
        //Enemy continues moving
        //agent.enabled = true;
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector3.zero;
            agent.isStopped = true;
            AgentStop();
        }
    }
}
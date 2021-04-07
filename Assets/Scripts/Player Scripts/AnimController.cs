﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    GameObject Player;

    CharacterMechanics cm;

    Animator animator;

    AnimatorClipInfo[] currentClipInfo;

    private string animName;

    [SerializeField] bool animDebug;

    bool isFalling;

    bool isJumping;

    bool isGrounded;

    float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        cm = Player.GetComponent<CharacterMechanics>();

        animator = Player.GetComponent<Animator>();

        int idleId = Animator.StringToHash("Idle");

        int runId = Animator.StringToHash("Run");

        int attack1Id = Animator.StringToHash("Attack 1");

        int attack2Id = Animator.StringToHash("Attack 2");

        int attack3Id = Animator.StringToHash("Attack 3");

        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //Automatically disables Root Motion (to avoid adding motion twice)
        animator.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        cm.updateValues();

        updateParameters();

        currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);

        //currentAnimLength = currentClipInfo[0].clip.length;

        animName = currentClipInfo[0].clip.name;

        if (animName == "Male Attack 1" && cm.actionAllowed || animName == "Male Attack 2" && cm.actionAllowed || animName == "Male Attack 3" && cm.actionAllowed)
        {
            cm.comboCount = 0;

            Debug.Log("Animation System: comboCount reset by update");
        }

        #region Debug Log

        if (animDebug)
        {
            Debug.Log("Animator System: Anim Name" + animName);

            //Debug.Log("Animator System: Anim Length" + currentAnimLength);
        }

        #endregion
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void updateValues(bool grounded, bool jumping, bool falling, float speed)
    {
        Speed = speed;

        isGrounded = grounded;

        isJumping = jumping;

        isFalling = falling;
    }

    public void hitGround()
    {
        animator.SetBool("isGrounded", isGrounded);

        animator.SetBool("isFalling", isFalling);

        if (isJumping)
        {
            isJumping = false;

            animator.SetBool("isJumping", isJumping);
        }
    }

    private void updateParameters()
    {
        animator.SetFloat("Speed", Speed);

        animator.SetBool("isGrounded", isGrounded);

        animator.SetBool("isJumping", isJumping);

        animator.SetBool("isFalling", isFalling);
    }
    public void takeDamage()
    {
        animator.SetInteger("Counter", cm.comboCount);

        if (cm.actionAllowed)
            animator.SetTrigger("Got Hit");
    }

    public void jump(bool _isGrounded, bool _isJumping, bool _isFalling)
    {
        animator.SetTrigger("Jump");

        animator.SetBool("isJumping", _isJumping);

        animator.SetBool("isGrounded", _isGrounded);

        animator.SetBool("isFalling", _isFalling);
    }

    public void attack(int _comboCount)
    {
        animator.SetInteger("Counter", _comboCount);

        animator.SetTrigger("Attack");
    }

    public void setComboCount(int _comboCount)
    {
        animator.SetInteger("Counter", _comboCount);
    }

    public void dash()
    {
        resetCounter();

        animator.SetTrigger("Dash");
    }

    public void smash()
    {
        resetCounter();

        animator.SetTrigger("Hammer Smash");
    }

    public void spin()
    {
        resetCounter();

        animator.SetTrigger("Spin");
    }

    public void throw_()
    {
        resetCounter();

        animator.SetTrigger("Throw");
    }

    public void respawn()
    {
        animator.SetTrigger("Respawn");
    }


    public void resetCounter()
    {
        animator.SetInteger("Counter", 0);
    }

}
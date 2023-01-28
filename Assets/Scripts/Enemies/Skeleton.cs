using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Skeleton : Enemy
{
    private Animator animator;
    public int lives = 2;

   // public override void Init()
    //{
        //animator = GetComponentInChildren<Animator>();
        //base.Init();
    //}
    /*
    public override void TakeDamage(int amountDamage)
    {
        health -= amountDamage;
        if (health <= 0)
        {
            onDeath.Invoke();
            col.enabled = false;
            lives--;

            if (lives <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(TurnIntoBones());
            }
        }
        else
        {
            colorTween.Restart();
        }
    }

    private IEnumerator TurnIntoBones()
    {
        animator.SetBool("Bones", true);
        yield return new WaitForSeconds(5f);
        animator.SetBool("Bones", false);
        yield return new WaitForSeconds(2f);
        col.enabled = true;
        health = maxHealth;
    }
    */
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slime : Enemy
{
    private Animator animator;

    //public override void Init()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //    base.Init();
    //}

    /*
    public override IEnumerator Die(float deathTime = 0)
    {
        col.enabled = false;

        animator.SetTrigger("Die");
        yield return new WaitForSeconds(deathTime);

        if(transform.localScale.x > 0.5f)
        {
            Slime Clone1 = Instantiate(gameObject).GetComponent<Slime>();
            Slime Clone2 = Instantiate(gameObject).GetComponent<Slime>();

            WaveController.AddToWave(Clone1);
            WaveController.AddToWave(Clone2);

            Clone1.GetComponent<Slime>().Shrink(currentPathNumber, targetPosition, false, MoneyValue / 2, transform);
            Clone2.GetComponent<Slime>().Shrink(currentPathNumber, targetPosition, false, MoneyValue / 2, transform);
            
        }
        

        MoneyManager.instance.GainMoney(MoneyValue);
        onDeath.Invoke();

        yield return new WaitForSeconds(deathTime);
        OnObjectDespawn();
    }
    

    public void Shrink(int newPathNumber, Vector3 newTargetPosition, bool isDead, int newMoneyValue, Transform ourTransform)
    {
        MoneyValue = newMoneyValue;
        currentPathNumber = newPathNumber;
        targetPosition = newTargetPosition;
        dead = isDead;
        transform.localScale = ourTransform.localScale * 0.5f;
        transform.position = ourTransform.position - Vector3.right * Random.Range(-0.3f,0.3f) - Vector3.up * Random.Range(-0.3f, 0.3f);
    }

    public override void TakeDamage(int amountDamage)
    {
        health -= amountDamage;
        if (health <= 0)
        {
            if (!dead)
            {
                dead = true;
                StartCoroutine(Die(0.7f));
            }
        }
        else
        {
            colorTween.Restart();
            animator.SetTrigger("Damage");
        }
    }
    */
}

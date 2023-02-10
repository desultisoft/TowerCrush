using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IPooledObject
{

    [Header("Stats")]
    public float speed;
    public float health;

    [Header("Needed Components")]
    public SpriteRenderer rend;
    public Collider2D col;
    public AudioSource deathSound;
    public Animator anim;
    public AngelDetector detector;
    public HealthController healthController { private set; get; }
    public FollowPath pathController { private set; get; }
    public delegate IEnumerator DeathHandler();

    public void Awake()
    {
        col = GetComponent<Collider2D>();

        pathController = new FollowPath(transform, speed);
        healthController = new HealthController(health);

        rend.material = new Material(rend.material);
    }

    public void OnEnable()
    {
        healthController.onLifeStatusChange += HandleStatusChange;
    }

    public void OnDisable()
    {
        healthController.onLifeStatusChange -= HandleStatusChange;
    }

    public void HandleStatusChange(bool isAlive)
    {
        if (!isAlive)
        {
            DeathHandler bookSelect = HandleDeath;
            if (bookSelect != null)
                StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        anim.speed = 1;
        deathSound.Play();
        anim.SetBool("Alive", false);

        yield return new WaitForSeconds(1);

        WaveController.instance.OnObjectDespawn(this);
    }

    public void OnObjectSpawn()
    {
        col.enabled = true;

        gameObject.SetActive(true);

        pathController.Reset();
        healthController.Reset();
        anim.SetBool("Alive", true);
    }

    public void OnObjectDespawn()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!healthController.isAlive) return;

        if (detector.target)
        {
            anim.SetBool("HasTarget", true);
        }
        else
        {
            anim.SetBool("HasTarget", false);
            pathController.Tick();
        }
    }
}

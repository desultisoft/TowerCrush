using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public int damage;
    public ShooterTower owner;
    public GameObject bulletBlueprint;
    public Transform[] bulletPoints;
    public AudioSource soundguy;

    public void Shoot()
    {
        soundguy.Play();
        foreach (Transform muzzle in bulletPoints)
        {
            GameObject newBulletGameObject = Instantiate(bulletBlueprint);
            newBulletGameObject.transform.position = muzzle.transform.position;
            Bullet newBulletComponent = newBulletGameObject.GetComponent<Bullet>();
            newBulletComponent.transform.rotation = transform.rotation;
            newBulletComponent.damage = damage;
            newBulletComponent.SetTarget(owner.eDetector.targetEnemy);
        }
    }
}

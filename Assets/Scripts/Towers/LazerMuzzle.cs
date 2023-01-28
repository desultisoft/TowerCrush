using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LazerMuzzle : MonoBehaviour
{
    public int damage;
    public ShooterTower owner;
    public GameObject bulletBlueprint;
    public Transform[] bulletPoints;
    public AudioSource soundguy;

    public void Shoot()
    {
        if (!owner.eDetector.targetEnemy)
            return;

        soundguy.Play();
        foreach (Transform muzzle in bulletPoints)
        {
            GameObject newBulletGameObject = Instantiate(bulletBlueprint);
            newBulletGameObject.transform.position = muzzle.transform.position;
            Bullet newBulletComponent = newBulletGameObject.GetComponent<Bullet>();
            newBulletComponent.damage = damage;

            newBulletComponent.SetDir(muzzle.transform.position - transform.position);
        }
    }
}

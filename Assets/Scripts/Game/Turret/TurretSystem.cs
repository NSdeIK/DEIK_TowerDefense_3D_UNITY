using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TurretSystem : MonoBehaviour
{

    public enum TurretTypes
    {
        ALAP,
        SHOTGUN,
        MINIGUN
    }

    public TurretTypes turretType;


    public string enemyTag = "Enemy";

    public float range;
    public float rangeRadius = 2f;
    public Transform target;

    public Transform partToRotate;
    public Transform partToRotate_2;


    public float rotateSpeed = 10f;

    public float fireRate = 1f;
    public float fireSpeed = 1f;
    public GameObject bulletPrefab;
    public Transform[] bulletShotPositions;
    private float fireCountdown = 0f;

    void Start()
    {
        range = transform.localPosition.y * rangeRadius;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null) return;

        Vector3 targetDirection = target.position - partToRotate_2.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        partToRotate.rotation = Quaternion.Euler(new Vector3(0f, partToRotate.rotation.eulerAngles.y, 0f));

        partToRotate_2.rotation = Quaternion.Slerp(partToRotate_2.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        partToRotate_2.rotation = Quaternion.Euler(new Vector3(partToRotate_2.rotation.eulerAngles.x, partToRotate_2.rotation.eulerAngles.y, partToRotate_2.rotation.eulerAngles.z));

        Debug.Log(fireCountdown);
        if (fireCountdown <= 0f)
        {
            if (turretType == TurretTypes.ALAP)
            {
                DefaultShot();
            }
            else if (turretType == TurretTypes.MINIGUN || turretType == TurretTypes.SHOTGUN)
            {
                DoubleShot();
            }

            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime * fireSpeed;

        Debug.DrawRay(partToRotate.position, targetDirection, Color.red);
    }

    void DefaultShot()
    {
        if(target != null)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, bulletShotPositions[0].position, bulletPrefab.transform.rotation);
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.SetTarget(target);
            }
        }
    }

    void DoubleShot()
    {
        if(bulletShotPositions.Length > 1)
        {
            GameObject leftShot = Instantiate(bulletPrefab, bulletShotPositions[0].position, bulletShotPositions[0].rotation);
            GameObject rightShot = Instantiate(bulletPrefab, bulletShotPositions[1].position, bulletShotPositions[1].rotation);
            Bullet Leftbullet = leftShot.GetComponent<Bullet>();
            Bullet Rightbullet = rightShot.GetComponent<Bullet>();

            if (Leftbullet != null && Rightbullet != null)
            {
                Leftbullet.SetTarget(target);
                Rightbullet.SetTarget(target);
            }
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    
}

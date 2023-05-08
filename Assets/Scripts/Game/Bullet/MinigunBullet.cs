using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBullet : MonoBehaviour
{
    public float speed;
    private Transform target;
    public GameObject hitEffect;

    private bool BulletSpread = true;
    private Vector3 BulletSpreadVariance = new Vector3(200, 200, 200);

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            Debug.Log("Nem talál");
            return;
        }

        //Vector3 direction = GetDirection();
        //Vector3 randomPos = direction + target.position;
        //StartCoroutine(RandomShot(randomPos));


        Vector3 enemyPos = new Vector3(target.position.x, target.position.y - (target.localScale.y / 2), target.position.z) + GetDirection();
        Vector3 dir = enemyPos - transform.position;
        float distanceFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceFrame, Space.World);



        //Debug.Log(direction);
    }

    void HitTarget()
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
        //Destroy(effect, 2f);
        Destroy(gameObject);
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = target.transform.forward;

        if (BulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }
}

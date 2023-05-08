using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Transform target;
    public GameObject hitEffect;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 enemyPos = new Vector3(target.position.x, target.position.y , target.position.z);
        Vector3 dir = enemyPos - transform.position;

        transform.rotation = Quaternion.LookRotation(dir);
        transform.position += dir.normalized * speed * Time.deltaTime;

        if (dir.magnitude <= 1f)
        {
            HitTarget();
            return;
        }



    }

    void HitTarget()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}

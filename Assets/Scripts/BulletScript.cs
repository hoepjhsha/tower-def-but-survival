using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Transform target;
    private Vector3 turretPosition;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int bulletDamage;

    public void Shoot(int attackDamage)
    {
        target = GameObject.Find("Base Tower").transform;
        bulletDamage = attackDamage;
    }

    private void Start()
    {
        turretPosition = transform.position;
    }

    void Update()
    { 
        Vector3 dir = new Vector3(turretPosition.x - target.position.x, 0f, turretPosition.z - target.position.z);
        dir.Normalize();
        //dir.y = turretPosition.y;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        CheckBulletDespawn();
    }

    private void CheckBulletDespawn()
    {
        if (Vector3.Magnitude(transform.position - turretPosition) >= 30)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Eminem eminem = collision.gameObject.GetComponent<Eminem>();
            if (eminem != null)
            {
                eminem.TakeDamage(bulletDamage);
            }

            Destroy(gameObject);
        }
    }
}

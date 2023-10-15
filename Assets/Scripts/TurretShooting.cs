using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [SerializeField]
    private int attackDamage = 50;
    [SerializeField]
    private float attackRange = 30f;
    [SerializeField]
    private float attackCooldown = 1f;
    private float currentCooldown = 0f;

    public GameObject bulletPrefab;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Attack(attackDamage);
                break;
            }
        }
    }

    void Attack(int attackDamage)
    {
        if (currentCooldown <= 0f)
        {
            Vector3 bulletPosition = transform.position;
            bulletPosition.y = 1f;
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            if (bulletScript != null)
            {
                bulletScript.Shoot(attackDamage);
            }
            currentCooldown = attackCooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

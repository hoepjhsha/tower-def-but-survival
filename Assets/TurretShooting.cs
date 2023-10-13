using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [SerializeField]
    private int attackDamage = 50;
    [SerializeField]
    private float attackRange = 20f;
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
                Attack(collider.gameObject, attackDamage);
                break;
            }
        }
        //Attack(FindNearestEnemy());
    }

    GameObject FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }
        }

        return nearestEnemy;
    }

    void Attack(GameObject target, int attackDamage)
    {
        if (currentCooldown <= 0f)
        {
            Vector3 bulletPosition = transform.position;
            bulletPosition.y = 1f;
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            if (bulletScript != null)
            {
                bulletScript.SetTarget(target, attackDamage);
            }
            currentCooldown = attackCooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}

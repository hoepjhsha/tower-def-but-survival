using UnityEngine;

public class TempBulletScript : MonoBehaviour
{
    private GameObject target;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int bulletDamage;

    public void SetTarget(GameObject newTarget, int attackDamage)
    {
        target = newTarget;
        bulletDamage = attackDamage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget(GameObject target)
    {
        Eminem eminem = target.GetComponent<Eminem>();
        if (eminem != null)
        {
            eminem.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}

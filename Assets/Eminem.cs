using UnityEngine;

public class Eminem : MonoBehaviour
{
    public Transform mainTarget;
    private Rigidbody rb;

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private float speed = 2f;

    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainTarget = GameObject.Find("Base Tower").transform;

        currentHealth = maxHealth;
    }

    void Update()
    {
        velocity = mainTarget.position - transform.position;
        velocity.Normalize();
        velocity.y = transform.position.y;

        rb.velocity = velocity * speed;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;

    public HealthBar healthBar;

    void Start()
    {
        healthBar?.SetMaxHealth(maxHealth);
    }

    void Awake()
    {
        currentHealth = maxHealth;        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar?.SetHealth(currentHealth);
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(5);
            Destroy(other.gameObject);
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}

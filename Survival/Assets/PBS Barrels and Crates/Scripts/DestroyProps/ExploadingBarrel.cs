using UnityEngine;

public class ExploadingBarrel : MonoBehaviour, IDamageable
{
    private float health = 1f;

    public GameObject explodedPrefab;
    public float explosionForce = 2.0f;
    public float explosionRadius = 5.0f;
    public float upForceMin = 0.0f;
    public float upForceMax = 0.5f;
    public bool autoDestroy = true;
    public float lifeTime = 5.0f;
    public float explosionDamageEnemies = 200f;
    public float explosionDamagePlayer = 10f;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        // Instantiate the exploded version of the object
        if (explodedPrefab != null)
        {
            GameObject go = Instantiate(explodedPrefab, transform.position, transform.rotation);

            // Optional: Destroy the exploded object after some time
            if (autoDestroy)
            {
                Destroy(go, lifeTime);
            }
        }

        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in colliders)
        {
            // Check if the collider has a tag of "Enemy" or "Player"
            if (hitCollider.CompareTag("Enemy"))
            {
                // Attempt to get an IDamageable component on the collider's GameObject
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    // Apply damage to the entity
                    damageable.Damage(explosionDamageEnemies);
                }
            }
            else if (hitCollider.CompareTag("Player"))
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    // Apply damage to the entity
                    damageable.Damage(explosionDamagePlayer);
                }
            }
        }
    }
}

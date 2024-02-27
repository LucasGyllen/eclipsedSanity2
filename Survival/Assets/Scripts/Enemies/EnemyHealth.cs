using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 200f;

    public void Damage(float damage)
    {
        Debug.Log($"Enemy took {damage} damage.");
        health -= damage;
        Debug.Log("health is now: " + health);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Hit();
        }
    }

    // Function to handle the enemy's death.
    private void Die()
    {
        // Perform any death-related actions here, such as playing death animations, spawning effects, or removing the enemy from the scene.
        // You can customize this method based on your game's requirements.

        // For example, you might destroy the enemy GameObject:
        gameObject.GetComponent<Animator>().SetBool("Death", true);

        
        /*yield return new WaitForSeconds(deathAnimationDuration);

        GameObject go = (GameObject)Instantiate(
            deathEffectPrefab,
            gameObject.transform.position,
            gameObject.transform.rotation
        );

        EnemyDeathEffects enemyDeath = go.GetComponent<EnemyDeathEffects>();

        enemyDeath.explosionForce = explosionForce;
        enemyDeath.explosionRadius = explosionRadius;
        enemyDeath.upForceMin = upForceMin;
        enemyDeath.upForceMax = upForceMax;

        enemyDeath.Explode();

        Destroy(gameObject);*/
    }

    private void Hit()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("IsHit", true);
        // Optionally, start a coroutine or set a timer to reset IsHit after a delay
        StartCoroutine(ResetHit());
    }

    private IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.24f); // Adjust the delay to match your hit animation length
        GetComponent<Animator>().SetBool("IsHit", false);
    }
}

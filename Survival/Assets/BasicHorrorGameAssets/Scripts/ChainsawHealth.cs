using UnityEngine;

public class ChainsawHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 200f;
    public GameObject keyPosition;
    public GameObject key;
    public Collider triggerCollider;

    public AudioClip deathClip;

    public void Damage(float damage)
    {
        Debug.Log($"Enemy took {damage} damage.");
        health -= damage;
        Debug.Log("health is now: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        triggerCollider.enabled = false;

        gameObject.GetComponent<Animator>().SetBool("Death", true);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);

        Invoke(nameof(MoveItemToPositionWithOffset), 3.2f);
    }

    private void MoveItemToPositionWithOffset()
    {
        Vector3 newPosition = keyPosition.transform.position + new Vector3(0, -0.1f, 0); 
        key.transform.position = newPosition;
    }
}

using UnityEngine;
using System.Collections;

public class DestroyOnShot : MonoBehaviour, IDamageable
{

    public GameObject explodedPrefab;

    public float explosionForce = 2.0f;
    public float explosionRadius = 5.0f;
    public float upForceMin = 0.0f;
    public float upForceMax = 0.5f;

    public bool autoDestroy = false;
    public float lifeTime = 5.0f;

    [SerializeField] private float health = 1f;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject go = (GameObject)Instantiate(
            explodedPrefab,
            gameObject.transform.position,
            gameObject.transform.rotation
            );

            // get the explosion component on the new object
            ExplodeBarrel explodeComp = go.GetComponent<ExplodeBarrel>();

            // set desired properties
            explodeComp.explosionForce = explosionForce;
            explodeComp.explosionRadius = explosionRadius;
            explodeComp.upForceMin = upForceMin;
            explodeComp.upForceMax = upForceMax;
            explodeComp.autoDestroy = autoDestroy;
            explodeComp.lifeTime = lifeTime;

            // make the barrel explode
            explodeComp.Explode();

            // destroy the nice barrel
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class DestroyOnHit : MonoBehaviour 
{

	public GameObject explodedPrefab;

	public float explosionForce = 2.0f;
	public float explosionRadius = 5.0f;
	public float upForceMin = 0.0f;
	public float upForceMax = 0.5f;

	public bool autoDestroy = true;
	public float lifeTime = 5.0f;
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col)
	{
		if (!col.rigidbody) 
			return;

		// instantiate the exploding barrel
		GameObject go = (GameObject) Instantiate(
			explodedPrefab, 
			gameObject.transform.position, 
			gameObject.transform.rotation
		);

        /*MaterialType materialTypeScript = go.GetComponent<MaterialType>();

        if (materialTypeScript != null)
        {
            // Set the TypeOfMaterial to Wood or any other type you need
            materialTypeScript.TypeOfMaterial = MaterialType.MaterialTypeEnum.Wood;
            Debug.Log("YO");
        }
        else
        {
            // Optionally handle the case where the MaterialType script is not found on the instantiated GameObject
            Debug.LogWarning("MaterialType script not found on the instantiated GameObject.");
        }*/

        // get the explosion component on the new object
        ExplodeBarrel explodeComp = go.GetComponent<ExplodeBarrel> ();

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
		Destroy (gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlatform : MonoBehaviour
{
    public float explosionRadius = 5.0f;
    public float explosionDuration = 2.0f;
    public float explosionForce = 10.0f;
    public GameObject explosionEffect;

    private bool isExploding = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isExploding && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        isExploding = true;

        // Instantiate the explosion effect at the center of the platform
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.zero;

        // Gradually increase the size and intensity of the explosion over time
        for (float t = 0.0f; t < explosionDuration; t += Time.deltaTime)
        {
            float scale = Mathf.Lerp(0, explosionRadius, t / explosionDuration);
            explosion.transform.localScale = new Vector3(scale, scale, 1.0f);
            yield return null;
        }

       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
                Vector2 direction = collider.gameObject.transform.position - transform.position;
                direction.Normalize();
                rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }
        }

        // Destroy the explosion effect and the platform
        Destroy(explosion);
        Destroy(gameObject);
        
    }
}


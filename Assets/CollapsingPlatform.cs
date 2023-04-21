using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public float collapseDelay = 2.0f;
    public float collapseTime = 2.0f;

    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CollapsePlatform());
        }
    }

    private IEnumerator CollapsePlatform()
    {
        yield return new WaitForSeconds(collapseDelay);
        anim.SetBool("IsCollapsing", true);
        
        yield return new WaitForSeconds(collapseTime);
        rb.isKinematic = false; 
        //gameObject.SetActive(false);
    }
}

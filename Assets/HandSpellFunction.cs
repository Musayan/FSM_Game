using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpellFunction : MonoBehaviour
{
    [SerializeField] private Transform attackDetection;
    [SerializeField] private float Damage;
    [SerializeField] private float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandAttack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(attackDetection.position, new Vector2 (1, 2),0f,Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<PlayerHealth>().DamagePlayer(10);
            }
            else
                return;
        }
    }


    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Vector2 size = new Vector2(1, 2);
        Vector2 direction = Vector2.zero;

        // Draw the BoxCast using Gizmos
        Gizmos.color = Color.red; // Choose the color you want
        Gizmos.DrawWireCube(attackDetection.position, size);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyFunction : MonoBehaviour
{
    [SerializeField] Transform FollowPlayer;
    private bool isKeyGet;
    private PolygonCollider2D keyCollider;

    private void Start()
    {
        FollowPlayer = GameObject.FindGameObjectWithTag("KeyPosFollow").transform;
        isKeyGet = false;

        keyCollider = GetComponent<PolygonCollider2D>();
        
    }

    private void Update()
    {
        if (isKeyGet)
        {
            transform.position = Vector2.Lerp(transform.position, FollowPlayer.position, Time.deltaTime * 2);
            keyCollider.excludeLayers = LayerMask.GetMask("Player");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isKeyGet = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    [SerializeField] Transform FollowPlayer;
    private bool isCrownGet;
    private PolygonCollider2D CrownCollider;

    private void Start()
    {
        FollowPlayer = GameObject.FindGameObjectWithTag("KeyPosFollow").transform;
        isCrownGet = false;

        CrownCollider = GetComponent<PolygonCollider2D>();

    }

    private void Update()
    {
        if (isCrownGet)
        {
            transform.position = Vector2.Lerp(transform.position, FollowPlayer.position, Time.deltaTime * 2);
            CrownCollider.excludeLayers = LayerMask.GetMask("Player");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCrownGet = true;
        }
    }
}

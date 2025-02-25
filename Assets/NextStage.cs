using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    private bool _isOpen;
    [SerializeField] private LayerMask _keyLayer;

    private SceneLoader _sceneLoader;
    void Start()
    {
        _sceneLoader = GameObject.FindAnyObjectByType<SceneLoader>();
        _isOpen = false;
    }


    private void Update()
    {
        DoorUnlock();
    }

    private void DoorUnlock()
    {
        Vector2 origin = transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(1, 2), 0f, Vector2.zero, 1f, _keyLayer);

        if (hit.collider != null )
        {
            _isOpen = true;
            Destroy( hit.collider.gameObject );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player") && _isOpen)
        {
            // Next Stage
            _sceneLoader.LoadNextScene();
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;

        // Direction of the box cast (you can use any direction you want)
        Vector2 direction = Vector2.zero;

        // Draw the box cast gizmo
        Gizmos.DrawWireCube(origin + direction * (1 * 0.5f), new Vector3(1, 1, 1));
    }
}

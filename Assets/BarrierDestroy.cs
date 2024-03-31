using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarrierDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _lifeUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        int spawn;
        spawn = 1 /*Random.Range(0, 1)*/;

        if (spawn == 1)
        {
            GameObject health = Instantiate(_lifeUp, transform.position, Quaternion.identity);
            health.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1f);
        }
    }
}

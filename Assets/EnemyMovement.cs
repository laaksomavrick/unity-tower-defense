using System;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{

    [Header("References")] [SerializeField]
    private Rigidbody2D rb;
    
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    
    void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
        }

        if (pathIndex == LevelManager.main.path.Length)
        {
           Destroy(gameObject);
           return;
        }
        
        target = LevelManager.main.path[pathIndex];
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        
        rb.linearVelocity = direction * moveSpeed;
    }
}

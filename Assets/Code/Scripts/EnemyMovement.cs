using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Rigidbody2D _rb;

    [Header("Attributes")] [SerializeField]
    private float moveSpeed = 2f;

    private Transform target;
    private Transform[] _path;
    private int pathIndex = 0;

    void Start()
    {
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();

        _path = levelManager.path;
        target = _path[pathIndex];
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
        }

        if (pathIndex == _path.Length)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
            return;
        }

        target = _path[pathIndex];
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        _rb.linearVelocity = direction * moveSpeed;
    }
}
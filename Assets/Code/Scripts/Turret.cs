using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

public class Turret : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform turretRotationPoint;

    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")] [SerializeField]
    private float targetingRange = 3f;
    [SerializeField]
    private float rotationSpeed = 200f;
    
    private Transform target;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (CheckIfTargetIsInRange() == false)
        {
            target = null; 
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,  transform.position, 0f, enemyMask);

        if (hits.Length == 0)
        {
            return;
        }

        target = hits[0].transform;
    }

    private bool CheckIfTargetIsInRange()
    {
       return Vector2.Distance(target.position, transform.position) <= targetingRange; 
    }
    

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
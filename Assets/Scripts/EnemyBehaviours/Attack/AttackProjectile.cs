using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : AttackBehaviour
{
    [SerializeField] private float radius = 5;
    
    [Header("Projectile")]
    [SerializeField] private int numberOfProjectiles = 1;
    [SerializeField] private float speed = 5;
    [SerializeField] private float duration = 6;
    [SerializeField] private EnemyPool projectileObject;

    private ObjectPooler pooler;

    private void Awake()
    {
        pooler = ObjectPooler.Instance;
    }

    public override bool AllowedToAttack()
    {
        return !isOnCooldown &&
            Vector3.Distance(enemy.Target.position, transform.position) <= radius;
    }

    public override IEnumerator Routine()
    {
        IsAttacking = true;
        
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 target = enemy.Target.position;
            Vector3 direction = (target - transform.position).normalized;
            enemy.Animator.Play(enemy.animations.attackAnimation);

            GameObject projectile = pooler.SpawnFromPool(projectileObject.tag, transform.parent, transform.position + direction, transform.rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Launch(direction, speed, duration);

            yield return new WaitForSecondsRealtime(enemy.Clip.length);
        }
        isOnCooldown = true;
        IsAttacking = false;
        enemy.ForceAggro();
    }

    private void OnDrawGizmos()
    {
        Vector3 lastPosition = transform.position + transform.right * radius;
        for (var i = 10; i <= 360; i += 10)
        {
            Vector3 nextPosition = transform.position + Quaternion.Euler(0, 0, i) * transform.right * radius;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(lastPosition, nextPosition);
            lastPosition = nextPosition;
        }

    }
}

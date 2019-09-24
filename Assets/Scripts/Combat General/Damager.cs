using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{

    [SerializeField]
    [EnumFlag]
    private DamageSource damageSource;

    [SerializeField]
    private int baseDamage;

    [SerializeField]
    private List<Collider2D> collisions;

    public void AttackEnemy()
    {
        if (collisions.Count > 0)
        {
            foreach (Collider2D collision in collisions)
            {
                Destroyable destroyable = collision.GetComponent<Destroyable>();
                destroyable.Damage(baseDamage, this.transform, damageSource);
            }
        }
    }

    public void OnEnter(Collider2D collision)
    {
        if (collision.CompareTag(NYRA.Tag.Playable))
        {
            collisions.Add(collision);
        }
    }

    public void OnExit(Collider2D collision)
    {
        collisions.Remove(collision);
    }
}

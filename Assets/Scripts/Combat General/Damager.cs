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

    private List<Collider2D> collisions;

    private void Awake()
    {
        collisions = new List<Collider2D>();
    }

    public void OnEnter(Collider2D collision)
    {
        Destroyable destroyable = collision.GetComponent<Destroyable>();
        destroyable.Damage(baseDamage, this.transform, damageSource);
    }

    public void DamageCollisions()
    {
        foreach (Collider2D col in collisions)
        {
            OnEnter(col);
        }
    }

    public void AddCollision(Collider2D collision)
    {
        collisions.Add(collision);
        Debug.Log("Col with " + collision.name);
    }

    public void RemoveCollision(Collider2D collision)
    {
        collisions.Remove(collision);
        Debug.Log("removed" + collision.name);
    }
}

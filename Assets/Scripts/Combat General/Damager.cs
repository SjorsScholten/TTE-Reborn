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

    public void OnEnter(Collider2D collision)
    {
        Destroyable destroyable = collision.GetComponent<Destroyable>();
        destroyable.Damage(baseDamage, this.transform, damageSource);
    }
}

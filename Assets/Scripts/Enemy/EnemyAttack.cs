using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject hitboxObject;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    [Tooltip("Time that the attack hitbox will be enabled.")]
    private float attackTime;

    private Direction side;

    private bool canAttack = true;

    private Damager damager;

    private void Awake()
    {
        damager = hitboxObject.GetComponent<Damager>();
    }

    public void GetDirection(Vector2 direction)
    {
        if (canAttack)
        {
            float horizontal = direction.x;
            float vertical = direction.y;

            if (vertical > 0)
            {
                side = Direction.North;
            }
            else
            {
                side = Direction.South;
            }

            if (side == Direction.North)
            {
                CheckSide(horizontal, vertical);
            }
            else
            {
                float absVertical = Mathf.Abs(vertical);
                CheckSide(horizontal, absVertical);
            }

            StartCoroutine(Attack(side));
        }
    }

    private void CheckSide(float horizontal, float vertical)
    {
        float absHorizontal = Mathf.Abs(horizontal);
        if (absHorizontal >= vertical)
        {
            if (horizontal > 0)
            {
                side = Direction.East;
            }
            else
            {
                side = Direction.West;
            }
        }
    }

    private IEnumerator Attack(Direction direction)
    {
        float rotation = 0;
        switch (direction)
        {
            case Direction.North:
                rotation = -90;
                break;
            case Direction.East:
                rotation = 180;
                break;
            case Direction.South:
                rotation = 90;
                break;
            case Direction.West:
                rotation = 0;
                break;
        }

        hitboxObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
        canAttack = false;
        //todo kijk of tie aanvalt
        yield return new WaitForSeconds(attackTime);
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

    /// <summary>
    /// Converts a Vector2 to a Direction Enum.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Direction ConvertVector2ToDirection(Vector2 vector) {
        Direction side;

        if (vector.y > 0) {
            side = Direction.North;
        } else {
            side = Direction.South;
        }

        if (side == Direction.North) {
            side = CheckSide(vector, side);
        } else {
            float absVertical = Mathf.Abs(vector.y);
            vector = new Vector2(vector.x, absVertical);
            side = CheckSide(vector, side);
        }

        return side;
    }

    private static Direction CheckSide(Vector2 vector, Direction side) {
        float absHorizontal = Mathf.Abs(vector.x);
        if (absHorizontal >= vector.y) {
            if (vector.x > 0) {
                return Direction.East;
            } else {
                return Direction.West;
            }
        } else {
            return side;
        }
    }

    /// <summary>
    /// Returns the base damage output based on the source of the damage.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="stats"></param>
    /// <returns></returns>
    public static int GetDamage(DamageSource source, BaseStats stats) {
        if ((source & DamageSource.Magic) != 0) return stats.resistance;
        else return stats.defense;
    }

}

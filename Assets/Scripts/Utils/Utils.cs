using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Utils {

    public static Direction Vector2ToDirection(Vector2 vector) {
        Direction dir =
            vector.x == 0f && vector.y > 0f ? Direction.North :
            vector.x == 0f && vector.y < 0f ? Direction.South :
            vector.x > 0f && vector.y == 0f ? Direction.East :
            Direction.West;


        return dir;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions {

    /// <summary>
    /// Set new index of Tab list.
    /// </summary>
    /// <param name="index">current index</param>
    /// <param name="next">-1 or +1</param>
    /// <param name="listCount">the return value of the Count method of your list</param>
    public static int GetIndex(int index, int next, int listCount) {
        index += next;
        if (index < 0) {
            index = listCount - 1;
        }
        else if (index == listCount) {
            index = 0;
        }
        return index;
    }
}

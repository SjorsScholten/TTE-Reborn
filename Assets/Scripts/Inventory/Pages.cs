using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Inventory pages")]
    private List<GameObject> pages;

    private int index = 0;

    /// <summary>
    /// Setup pages.
    /// </summary>
    public void Setup() {
        while (index != 0) {
            NextPage(1);
        }
    } 

    /// <summary>
    /// Go to the next page
    /// </summary>
    /// <param name="next">1 is forward -1 is backwards</param>
    public void NextPage(int next) {
        index = HelperFunctions.GetIndex(index, next, pages.Count);
        pages[index].transform.SetAsLastSibling();
    }
}

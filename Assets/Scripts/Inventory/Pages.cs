using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Inventory pages")]
    private List<GameObject> pages;

    private int index = 0;

    public void Setup() {
        while (index != 0) {
            NextPage(1);
        }
    } 

    public void NextPage(int next) {
        index = HelperFunctions.GetIndex(index, next, pages.Count);
        pages[index].transform.SetAsLastSibling();
    }
}

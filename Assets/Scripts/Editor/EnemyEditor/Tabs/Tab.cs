using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tab
{
    protected string tabName = "";
    protected Rect windowSize;

    public abstract void DisplayTab();

    public abstract void OnSelectionChanged(GameObject selection);

    public string TabName {
        get { return tabName; }
    }

    public Rect WindowSize {
        set { windowSize = value; }
    }
}
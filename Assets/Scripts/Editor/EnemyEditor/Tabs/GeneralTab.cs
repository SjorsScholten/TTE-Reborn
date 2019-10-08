using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GeneralTab : Tab
{
    string objectName = "";

    public GeneralTab()
    {
        tabName = "General";
    }

    public override void DisplayTab()
    {
        //UI Code here
        objectName = EditorGUILayout.TextField("Name of Object", objectName);

        if (GUILayout.Button("Change name"))
        {
            ChangeName();
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Save Prefab"))
        {
            SavePrefab();
        }
    }

    public override void OnSelectionChanged(GameObject selection)
    {
        objectName = selection.name;
    }

    /// <summary>
    /// Changes the name of the object in the scene.
    /// Does not apply it to the prefab.
    /// </summary>
    private void ChangeName()
    {
        try
        {
            GameObject obj = Selection.activeObject as GameObject;
            obj.name = objectName;
        }
        catch (NullReferenceException)
        {
            Debug.Log("No object has been selected");
        }
        catch (Exception)
        {
            Debug.Log("Name is empty");
        }
    }

    /// <summary>
    /// Saves the prefab.
    /// </summary>
    private void SavePrefab()
    {
        try
        {
            GameObject obj = Selection.activeObject as GameObject;

            string path2 = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(obj);

            PrefabUtility.SaveAsPrefabAsset(obj, path2);
            AssetDatabase.RenameAsset(path2, objectName);
        }
        catch (Exception)
        {
            Debug.Log("Geen prefab gevonden.");
        }
    }
}

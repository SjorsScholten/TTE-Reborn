using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GeneralTab : Tab
{
    private string objectName = "";
    private string path = "Assets/Prefabs/Enemies/";

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
        GameObject obj = Selection.activeObject as GameObject;
        try
        {
            if (obj != null)
            {
                string path2 = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(obj);
                Debug.Log(path2);
                PrefabUtility.SaveAsPrefabAsset(obj, path2);
                AssetDatabase.RenameAsset(path2, objectName);
                Debug.Log("Prefab Saved");
            }
        }
        catch (Exception)
        {
            Debug.Log("Nieuwe Prefab wordt gemaakt.");
            try
            {
                path += obj.name + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(obj, path);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GeneralTab : Tab
{
    private string objectName = "";
    private string path = "Assets/Prefabs/Enemies/";
    private GameObject selection;

    private EnemyAnimations animations;
    private EnemyController controller;

    public GeneralTab()
    {
        tabName = "General";
    }

    public override void DisplayTab()
    {
        //UI Code here
        objectName = EditorGUILayout.TextField("Name of Object", objectName);
        ChangeName();

        GUILayout.Space(5);

        EditorGUILayout.LabelField("Animations", EditorStyles.boldLabel);
        animations = EditorGUILayout.ObjectField("Animations", animations, typeof(EnemyAnimations), false) as EnemyAnimations;
        ChangeAnimation();

        GUILayout.Space(5);

        if (GUILayout.Button("Save Prefab"))
        {
            SavePrefab();
        }
    }

    public override void OnSelectionChanged(GameObject selection)
    {
        objectName = selection.name;
        this.selection = selection;
        controller = selection.GetComponent<EnemyController>();
        if (controller != null)
        {
            animations = controller.animations;
        }
    }

    /// <summary>
    /// Changes the name of the object in the scene.
    /// Does not apply it to the prefab.
    /// </summary>
    private void ChangeName()
    {
        if (selection != null)
        {
            selection.name = objectName;
        }
    }

    private void ChangeAnimation()
    {
        if (selection != null && controller != null)
        {
            controller.animations = animations;
        }
    }

    /// <summary>
    /// Saves and creates the prefab.
    /// </summary>
    private void SavePrefab()
    {
        GameObject obj = Selection.activeObject as GameObject;
        GameObject prefab = null;

        if (obj != null)
        {
            try
            {
                //Save existing prefab and rename it.
                string path2 = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(obj);
                prefab = PrefabUtility.SaveAsPrefabAsset(obj, path2);
                AssetDatabase.RenameAsset(path2, objectName);
            }
            catch (Exception)
            {
                //Create new prefab
                try
                {
                    prefab = PrefabUtility.SaveAsPrefabAsset(obj, path + obj.name + ".prefab");
                    Debug.Log("Gemaakt");

                    //Replace old object with prefab and select it
                    GameObject select = PrefabUtility.InstantiatePrefab(prefab, obj.transform.parent) as GameObject;
                    GameObject.DestroyImmediate(obj, true);
                    Selection.SetActiveObjectWithContext(select, null);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        }
    }
}

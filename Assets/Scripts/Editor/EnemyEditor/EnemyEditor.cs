using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EnemyEditor : EditorWindow
{
    private int currentTab;
    private Tab[] tabs;
    private string[] tabNames;
    private GameObject currentSelected;
    private GameObject selected;

    private string path = "Assets/Prefabs/Enemies/";

    private void OnEnable()
    {
        //Gets tabs
        tabs = new Tab[3] { new GeneralTab() ,new StatsTab(), new BehaviourTab() };

        //Gets the tabs names
        tabNames = new string[tabs.Length];

        for (int i = 0; i < tabNames.Length; i++)
        {
            tabNames[i] = tabs[i].TabName;
        }

        Selection.selectionChanged += SelectionChanged;
    }

    [MenuItem("TTE/Enemy Editor")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow(typeof(EnemyEditor));
        window.titleContent.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/kratos.png");
        window.titleContent.text = "Enemy Editor";
    }

    private void OnGUI()
    {
        GUILayout.Label("Enemy Editor", EditorStyles.toolbarButton);

        int previousTab = currentTab;
        //Creates and updates tabs
        currentTab = GUILayout.Toolbar(currentTab, tabNames);

        //Fill in current tab
        if (CheckIfEnemy())
        {
            tabs[currentTab].OnSelectionChanged(selected);
        }

        switch (currentTab)
        {
            //Each case draws the tab and then sends it the size of the window
            case 0:
                tabs[0].DisplayTab();
                tabs[0].WindowSize = position;
                break;
            case 1:
                tabs[1].DisplayTab();
                tabs[1].WindowSize = position;
                break;
            case 2:
                tabs[2].DisplayTab();
                tabs[2].WindowSize = position;
                break;
        }

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Save Prefab"))
        {
            SavePrefab();
        }
        GUILayout.Space(10);

        //Checks if a tab is changed
        if (previousTab != currentTab)
        {
            //Used to prevent text fields from having the same displayed value when switching tabs
            GUI.FocusControl("");
        }
    }

    private void SelectionChanged()
    {
        if (CheckIfEnemy())
        {
            Debug.Log("Repaint");
            for (int i = 0; i <= tabs.Length - 1; i++)
            {
                Debug.Log(tabs[i].TabName);
                tabs[i].OnSelectionChanged(selected);
            }
            Repaint();
        }
    }

    private bool CheckIfEnemy()
    {
        selected = Selection.activeGameObject;
        if (selected != null && selected != currentSelected && selected.CompareTag(NYRA.Tag.Enemies))
        {
            currentSelected = selected;
            return true;
        }
        return false;
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
                AssetDatabase.RenameAsset(path2, obj.name);
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

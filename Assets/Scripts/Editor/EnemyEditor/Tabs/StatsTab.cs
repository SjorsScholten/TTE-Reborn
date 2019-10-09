using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StatsTab : Tab
{
    private BaseStats stats;
    private Destroyable destroyable;

    public StatsTab()
    {
        tabName = "Stats";
    }

    public override void DisplayTab()
    {
        if (stats != null)
        {
            //UI Code here
            stats = EditorGUILayout.ObjectField("Stats Object", stats, typeof(BaseStats), true) as BaseStats;

            GUILayout.Space(5);

            stats.vitality = EditorGUILayout.IntField("Vitality", stats.vitality);
            stats.wisdom = EditorGUILayout.IntField("Wisdom", stats.wisdom);
            stats.strength = EditorGUILayout.IntField("Strength", stats.strength);
            stats.defense = EditorGUILayout.IntField("Defense", stats.defense);
            stats.magic = EditorGUILayout.IntField("Magic", stats.magic);
            stats.resistance = EditorGUILayout.IntField("Resistance", stats.resistance);
            stats.speed = EditorGUILayout.IntField("Speed", stats.speed);
            stats.luck = EditorGUILayout.IntField("Luck", stats.luck);

            if (GUILayout.Button("Save Stats to Object"))
            {
                SaveStats();
            }
        }
    }

    public override void OnSelectionChanged(GameObject selection)
    {
        try
        {
            destroyable = selection.GetComponent<Destroyable>();
            stats = destroyable.Stats;
        }
        catch (System.Exception)
        {
            Debug.Log("No destoyable script has been found on the selected object.", selection);
        }
    }

    private void SaveStats()
    {
        destroyable.Stats = stats;
    }
}
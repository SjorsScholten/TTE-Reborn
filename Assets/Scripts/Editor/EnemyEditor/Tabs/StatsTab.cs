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
        stats = EditorGUILayout.ObjectField("Stats Object", stats, typeof(BaseStats), true) as BaseStats;
        SaveStats();
        GUILayout.Space(5);
        if (stats != null)
        {     
            stats.vitality = EditorGUILayout.IntField("Vitality", stats.vitality);
            stats.wisdom = EditorGUILayout.IntField("Wisdom", stats.wisdom);
            stats.strength = EditorGUILayout.IntField("Strength", stats.strength);
            stats.defense = EditorGUILayout.IntField("Defense", stats.defense);
            stats.magic = EditorGUILayout.IntField("Magic", stats.magic);
            stats.resistance = EditorGUILayout.IntField("Resistance", stats.resistance);
            stats.speed = EditorGUILayout.IntField("Speed", stats.speed);
            stats.luck = EditorGUILayout.IntField("Luck", stats.luck);
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
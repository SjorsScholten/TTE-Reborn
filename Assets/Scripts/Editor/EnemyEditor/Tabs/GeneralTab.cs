﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GeneralTab : Tab
{
    private string objectName = "";
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

    
}

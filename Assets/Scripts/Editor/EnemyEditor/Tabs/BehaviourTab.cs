using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class Behaviour
{
    public Dictionary<string, Type> behaviours = new Dictionary<string, Type>();
    public string name;
    public Type current;
    public int currentIndex;

    public Behaviour(string name, Type type)
    {
        this.name = name;
        GetBehaviours(type);
    }

    private void GetBehaviours(Type superClass)
    {
        Type[] typesInAssembly = superClass.Assembly.GetTypes();
        foreach (Type t in typesInAssembly)
        {
            if (t.IsSubclassOf(superClass))
            {
                // Found a subclass, add to list
                behaviours.Add(t.Name, t);
            }
        }
    }

    public void SetCurrentSelected(Type current)
    {
        if (current == null)
        {
            this.current = behaviours.Values.ToList()[0];
            currentIndex = 0;
        }
        else
        {
            this.current = current;
            currentIndex = behaviours.Values.ToList().IndexOf(current);
        }
    }

    public void SetCurrentSelected(int index)
    {
        current = behaviours.Values.ToList()[index];
        currentIndex = index;
    }
}

public class BehaviourTab : Tab
{
    private List<Behaviour> behaviours;
    private EnemyController controller;

    /// <summary>
    /// Set up tab and behaviours
    /// </summary>
    public BehaviourTab()
    {
        tabName = "Behaviour";
        behaviours = new List<Behaviour>
        {
            new Behaviour("Idle", typeof(IdleBehaviour)),
            new Behaviour("Movement", typeof(MovementBehaviour)),
            new Behaviour("Attack", typeof(AttackBehaviour)),
            new Behaviour("Aggro", typeof(AggroBehaviour))
        };
    }

    /// <summary>
    /// Display a Dropdown for each Behaviour.
    /// </summary>
    public override void DisplayTab()
    {
        //UI Code here
        for (int i = 0; i < behaviours.Count; i++)
        {
            Behaviour behaviour = behaviours[i];
            EditorGUILayout.LabelField(behaviour.name + " Behaviour", EditorStyles.boldLabel);
            int current = EditorGUILayout.Popup(behaviour.name, behaviour.currentIndex, GetNames(ref behaviour.behaviours));            

            GUILayout.Space(5);

            //Set selected
            if (current != behaviour.currentIndex)
            {
                DestroyScript(i);
                behaviour.SetCurrentSelected(current);
                controller.gameObject.AddComponent(behaviour.current);
                SetRefference(i);
            }
        }
        
    }

    /// <summary>
    /// Get scripts.
    /// </summary>
    /// <param name="selection">Current selected GameObject</param>
    public override void OnSelectionChanged(GameObject selection)
    {
        controller = selection.GetComponent<EnemyController>();

        if (controller != null)
        {
            int behaviourIndex = 0;
            //Set pre selected
            AddScript(controller.idle, behaviourIndex);
            behaviourIndex++;
            AddScript(controller.movement, behaviourIndex);
            behaviourIndex++;
            AddScript(controller.attack ,behaviourIndex);
            behaviourIndex++;
            AddScript(controller.aggro ,behaviourIndex);
        }
    }

    /// <summary>
    /// Get names of the scripts
    /// </summary>
    /// <param name="list">List to get the names from</param>
    /// <returns>String array of names</returns>
    private string[] GetNames(ref Dictionary<string, Type> list)
    {
        return list.Keys.ToArray();
    }

    /// <summary>
    /// Set References in the Controllers
    /// </summary>
    /// <param name="behaviourIndex">index of behaviour</param>
    private void SetRefference(int behaviourIndex)
    {
        switch (behaviourIndex)
        {
            case 0:
                controller.idle = controller.GetComponent<IdleBehaviour>();
                break;
            case 1:
                controller.movement = controller.GetComponent<MovementBehaviour>();
                break;
            case 2:
                controller.attack = controller.GetComponent<AttackBehaviour>();
                break;
            case 3:
                controller.aggro = controller.GetComponent<AggroBehaviour>();
                break;
        }
    }

    /// <summary>
    /// Destory script based on the behaviourIndex
    /// </summary>
    /// <param name="behaviourIndex">index of behaviour</param>
    private void DestroyScript(int behaviourIndex)
    {
         switch (behaviourIndex)
        {
            case 0:
                UnityEngine.Object.DestroyImmediate(controller.GetComponent<IdleBehaviour>());
                break;
            case 1:
                UnityEngine.Object.DestroyImmediate(controller.GetComponent<MovementBehaviour>());
                break;
            case 2:
                UnityEngine.Object.DestroyImmediate(controller.GetComponent<AttackBehaviour>());
                break;
            case 3:
                UnityEngine.Object.DestroyImmediate(controller.GetComponent<AggroBehaviour>());
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">Current Behaviour found on the controller</param>
    /// <param name="behaviourIndex">index of behaviour</param>
    private void AddScript<T>(T type, int behaviourIndex)
    {
        if (type != null)
        {
            behaviours[behaviourIndex].SetCurrentSelected(type.GetType());
        }
        else
        {
            behaviours[behaviourIndex].SetCurrentSelected(0);
            Type t = behaviours[behaviourIndex].current;
            controller.gameObject.AddComponent(t);
            SetRefference(behaviourIndex);
        }
    }
}

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
                // found a subclass, add to list or something
                behaviours.Add(t.Name, t);
                Debug.Log("Super: " + superClass.Name + ", Add: " + t.Name);
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
        Debug.Log(index + " " + current);
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
                DestoryScript(i);
                behaviour.SetCurrentSelected(current);
                controller.gameObject.AddComponent(behaviour.current);
                SetRefference(i);
            }
        }
        
    }

    /// <summary>
    /// Get scripts.
    /// </summary>
    /// <param name="selection"></param>
    public override void OnSelectionChanged(GameObject selection)
    {
        controller = selection.GetComponent<EnemyController>();

        if (controller != null)
        {
            //Set pre selected
            #region Idle
            IdleBehaviour idle = controller.idle;
            if (idle != null)
            {
                Debug.Log("Idle is not null");
                behaviours[0].SetCurrentSelected(idle.GetType());
            }
            else
            {
                Debug.Log("Idle is null");
                behaviours[0].SetCurrentSelected(0);
                Type t = behaviours[0].current;
                controller.gameObject.AddComponent(t);
                SetRefference(0);
            }
            #endregion

            #region Movement
            MovementBehaviour movement = controller.movement;
            if (movement != null)
            {
                behaviours[1].SetCurrentSelected(movement.GetType());
            }
            else
            {
                behaviours[1].SetCurrentSelected(null);
                Type t = behaviours[1].current;
                controller.gameObject.AddComponent(t);
                SetRefference(1);
            }
            #endregion

            #region Attack
            AttackBehaviour attack = controller.attack;
            if (attack != null)
            {
                behaviours[2].SetCurrentSelected(attack.GetType());
            }
            else
            {
                behaviours[2].SetCurrentSelected(null);
                Type t = behaviours[2].current;
                controller.gameObject.AddComponent(t);
                SetRefference(2);
            }
            #endregion

            #region Aggro
            AggroBehaviour aggro = controller.aggro;
            if (attack != null)
            {
                behaviours[3].SetCurrentSelected(aggro.GetType());
            }
            else
            {
                behaviours[3].SetCurrentSelected(null);
                Type t = behaviours[3].current;
                controller.gameObject.AddComponent(t);
                SetRefference(3);
            }
            #endregion
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

    private void DestoryScript(int behaviourIndex)
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
}

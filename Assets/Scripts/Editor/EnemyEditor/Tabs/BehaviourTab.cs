using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class Behaviour
{
    public Dictionary<string, Object> behaviours = new Dictionary<string, Object>();
    public string folder;
    public Object current;
    public int currentIndex;

    public Behaviour(string folder)
    {
        this.folder = folder;
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
        behaviours = new List<Behaviour>();
        behaviours.Add(new Behaviour("Idle"));
        behaviours.Add(new Behaviour("Movement"));
        behaviours.Add(new Behaviour("Attack"));
        behaviours.Add(new Behaviour("Aggro"));
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
            EditorGUILayout.LabelField(behaviour.folder + " Behaviour", EditorStyles.boldLabel);
            int current = EditorGUILayout.Popup(behaviour.folder, behaviour.currentIndex, GetNames(ref behaviour.behaviours));

            GUILayout.Space(5);

            //Set new scripts
            if (controller != null && behaviour.behaviours.Count > 0 && current != behaviour.currentIndex)
            {
                behaviour.current = DestoryAndAddScript(behaviour, current, i);
                behaviour.currentIndex = current;

                //Ugly Hardcode to add the references
                AddReferences(i);
            }
        }  
    }

    /// <summary>
    /// Get scripts.
    /// This is called in the OnSelectionChanged so the script lists stay up to date.
    /// </summary>
    /// <param name="selection"></param>
    public override void OnSelectionChanged(GameObject selection)
    {
        //Get Scripts
        foreach (Behaviour behaviour in behaviours)
        {
            GetScripts(behaviour.folder, ref behaviour.behaviours);
        }

        controller = selection.GetComponent<EnemyController>();

        if (controller != null)
        {
            //Set pre selected
            behaviours[0].current = controller.idle;
            behaviours[1].current = controller.movement;
            behaviours[2].current = controller.attack;
            behaviours[3].current = controller.aggro;

            //
            for (int i = 0; i < behaviours.Count; i++)
            {
                Behaviour behaviour = behaviours[i];

                int index = 0;               
                Object current = behaviour.current;
                Debug.Log("On selection current: " + current);
                //If current is not null select it
                if (current != null)
                {
                    List<string> names = behaviour.behaviours.Keys.ToList();
                    System.Type type = current.GetType();
                    index = names.IndexOf(behaviour.current.GetType().ToString());
                }
                //If current is empty add a default script.
                else
                {
                    behaviour.current = behaviour.behaviours.Values.ToList()[0];
                    DestoryAndAddScript(behaviour, 0, i);
                }
                behaviour.currentIndex = index;
            }
        }

       
    }

    /// <summary>
    /// Get Scripts found in the given folder.
    /// Folder and scripts have to exist in the Resources folder.
    /// </summary>
    /// <param name="folder">Resources folder</param>
    /// <param name="list">List to fill with the found scripts</param>
    private void GetScripts(string folder, ref Dictionary<string, Object> list)
    {
        list.Clear();
        Object[] scripts = Resources.LoadAll("Scripts/EnemyBehaviours/" + folder, typeof(MonoScript));
        foreach (Object script in scripts)
        {
            list.Add(script.name, script);
        }
    }

    /// <summary>
    /// Get names of the scripts
    /// </summary>
    /// <param name="list">List to get the names from</param>
    /// <returns>String array of names</returns>
    private string[] GetNames(ref Dictionary<string, Object> list)
    {
        return list.Keys.ToArray();
    }

    /// <summary>
    /// Destroy old script and Add a new One.
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="current"></param>
    /// <returns></returns>
    private Object DestoryAndAddScript(Behaviour behaviour, int current, int indexBehaviour)
    {
        Debug.Log(behaviour.current.GetType());
        bool destroy = false;
        switch (indexBehaviour)
        {
            case 0:
                destroy = controller.idle != null ? true : false;
                break;
            case 1:
                destroy = controller.movement != null ? true : false;
                break;
            case 2:
                destroy = controller.attack != null ? true : false;
                break;
            case 3:
                destroy = controller.aggro != null ? true : false;
                break;
        }
        if(destroy)
        {
            Object.DestroyImmediate(behaviour.current);
        }
        
        Object newScript = behaviour.behaviours.Values.ToList()[current];
        System.Type type = newScript.GetType();
        Debug.Log(type);
        //controller.gameObject.AddComponent((MonoBehaviour)newScript.GetType());
        controller.gameObject.AddComponent(type);
        return newScript;
    }

    /// <summary>
    /// Add references to the controller.
    /// The ugly hardcoded way.
    /// </summary>
    /// <param name="indexBehaviour">index of the behaviour</param>
    private void AddReferences(int indexBehaviour)
    {
        switch (indexBehaviour)
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
}

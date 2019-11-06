using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

[InitializeOnLoad]
public class EditorPrefabSpawner : Editor {

    static EditorPrefabSpawner() {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    private class PrefabSpawn {
        public string Path;
        public string ParentObjectPath;
        public bool CanSpawnOutsideRoom;
        public bool AllowedToSpawn;
    }

    private static Event _lastEvent;

    static void OnSceneGUI(SceneView sceneView) {
        if (Event.current.button == 1 && Event.current.type == EventType.MouseDown) {
            _lastEvent = Event.current;

            var menu = new GenericMenu();

            foreach (var guid in AssetDatabase.FindAssets("", new[] { EditorContextPaths.SpawnersPath })) {
                var prefabPath = AssetDatabase.GUIDToAssetPath(guid);
                var content = "Spawners" + prefabPath.Substring(EditorContextPaths.SpawnersPath.Length);
                if (!content.Contains('.')) continue;
                content = content.Substring(0, content.IndexOf('.'));
                menu.AddItem(new GUIContent(content), false, Spawn, new PrefabSpawn {
                    Path = prefabPath,
                    ParentObjectPath = EditorContextPaths.EnemiesObjectPath,
                    CanSpawnOutsideRoom = false
                });
            }

            menu.ShowAsContext();
        }
    }

    static Transform GetParent(PrefabSpawn spawn) {
        var selectedObject = Selection.activeGameObject;

        if (selectedObject == null) return null;

        var room = selectedObject.GetComponent<Room>();
        if (room == null && selectedObject.transform.parent.name == spawn.ParentObjectPath) {
            spawn.AllowedToSpawn = true;
            return selectedObject.transform.parent;
        }

        if (room != null) {
            var children = room.transform.GetComponentsInChildren<Transform>();
            Transform parent = null;
            foreach (var child in children) {
                if (child.name == spawn.ParentObjectPath) {
                    parent = child;
                    break;
                }
            }
            if (!parent) spawn.AllowedToSpawn = false;
            else spawn.AllowedToSpawn = true;
            return parent;
        } else {
            spawn.AllowedToSpawn = false;
            return null;
        }
    }

    static void Spawn(object obj) {
        var prefabToSpawn = obj as PrefabSpawn;
        var parentObject = GetParent(prefabToSpawn);

        if (prefabToSpawn.AllowedToSpawn == false) {
            Debug.LogError("Object may not be placed outside of rooms.\nPlease select a room to place this object.");
            return;
        }

        var go = PrefabUtility.InstantiatePrefab((GameObject)AssetDatabase.LoadAssetAtPath(prefabToSpawn.Path, typeof(GameObject))) as GameObject;

        var pos = _lastEvent.mousePosition;

        pos = Camera.main.ScreenToWorldPoint(pos);

        if (parentObject != null) {
            go.transform.parent = parentObject;

        }

        go.transform.localPosition = Vector3.zero;

        Undo.RegisterCreatedObjectUndo(go, "Undo Spawn " + Path.GetFileNameWithoutExtension(prefabToSpawn.Path));

        Selection.activeGameObject = go.gameObject;
    }

}

public class EditorContextPaths {
    public static string SpawnersPath = "Assets/Prefabs/EnemySpawners";
    public static string EnemiesObjectPath = "Enemies";
    public static string MainCamera = "Main Camera";
}

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
        public string ParentObjectName;
    }

    private static Event _lastEvent;

    static void OnSceneGUI(SceneView sceneView) {
        if (Event.current.button == 1 && Event.current.type == EventType.MouseDown) {
            _lastEvent = Event.current;

            var menu = new GenericMenu();

            foreach (var guid in AssetDatabase.FindAssets("", new[] { EditorContextPaths.TestingPath })) {
                var prefabPath = AssetDatabase.GUIDToAssetPath(guid);
                var content = "Testing" + prefabPath.Substring(EditorContextPaths.TestingPath.Length);
                if (!content.Contains('.')) continue;
                content = content.Substring(0, content.IndexOf('.'));
                menu.AddItem(new GUIContent(content), false, Spawn, new PrefabSpawn {
                    Path = prefabPath,
                    ParentObjectName = EditorContextPaths.MainCamera
                });
            }

            menu.ShowAsContext();
        }
    }

    static void Spawn(object obj) {
        var prefabToSpawn = obj as PrefabSpawn;

        var go = PrefabUtility.InstantiatePrefab((GameObject)AssetDatabase.LoadAssetAtPath(prefabToSpawn.Path, typeof(GameObject))) as GameObject;

        var pos = _lastEvent.mousePosition;

        pos = Camera.main.ScreenToWorldPoint(pos);

        if (prefabToSpawn.ParentObjectName != null) {
            var parentPath = prefabToSpawn.ParentObjectName;
            var parentObject = GameObject.Find(parentPath);

            go.transform.parent = parentObject.transform;
        }

        go.transform.position = pos;

        Undo.RegisterCreatedObjectUndo(go, "Undo Spawn " + Path.GetFileNameWithoutExtension(prefabToSpawn.Path));

        Selection.activeGameObject = go.gameObject;
    }

}

public class EditorContextPaths {
    public static string TestingPath = "Assets/Prefabs/EditorContextTesting";
    public static string MainCamera = "Main Camera";
}

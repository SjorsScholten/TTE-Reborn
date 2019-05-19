using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(Grid))]
public class TilemapSelector : Editor {

    private void OnSceneGUI() {
        var go = target as Grid;
        var assembly = Assembly.Load(new AssemblyName("UnityEditor"));
        var windowType = assembly.GetType("UnityEditor.GridPaintPaletteWindow");
        var paletteWindow = UnityEditor.EditorWindow.GetWindow(windowType, false, "Tile Palette", false);

        Handles.BeginGUI();

        GUILayout.BeginArea(new Rect(Camera.current.pixelWidth / EditorGUIUtility.pixelsPerPoint - 150, 20, 100, Camera.current.pixelHeight / EditorGUIUtility.pixelsPerPoint - 40));

        var rect = EditorGUILayout.BeginVertical();
        GUI.color = Color.gray;
        GUI.Box(rect, GUIContent.none);

        GUI.color = Color.white;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Tilemaps");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(go.name);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        foreach (Transform child in go.transform) {
            if (child.CompareTag(NYRA.Tag.RoomTilemap)) {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.backgroundColor = Color.red;

                if (GUILayout.Button(child.name, GUILayout.Width(90), GUILayout.Height(70))) {
                    Event.current.Use();

                    if (paletteWindow != null) {
                        var selectTargetMethod = windowType.GetMethod("SelectTarget", BindingFlags.Instance | BindingFlags.NonPublic);
                        selectTargetMethod.Invoke(paletteWindow, new object[] { 0, child.gameObject });
                    }
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
            }
        }

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.grey;
        if (GUILayout.Button("Deselect", GUILayout.Width(90), GUILayout.Height(70))) {
            Selection.activeGameObject = null;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();

        EditorGUILayout.EndVertical();

        GUILayout.EndArea();

        Handles.EndGUI();
    }

}

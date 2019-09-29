using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAnimations", menuName = "Enemy/Animations")]
public class EnemyAnimations : ScriptableObject {
    public GameObject enemy;
    [HideInInspector] public string idleAnimation;
    [HideInInspector] public string walkAnimation;
    [HideInInspector] public string attackAnimation;
    [HideInInspector] public string hurtAnimation;

    [HideInInspector] public int choiceIndexIdle;
    [HideInInspector] public int choiceIndexWalk;
    [HideInInspector] public int choiceIndexAttack;
    [HideInInspector] public int choiceIndexHurt;
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyAnimations))]
public class EnemyAnimationsEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EnemyAnimations script = (EnemyAnimations)target;

        if (script.enemy == null) return;

        if (script.enemy.GetComponent<Animator>()) {
            Animator animator = script.enemy.GetComponent<Animator>();
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            string[] clipsString = new string[clips.Length];

            for (int i = 0; i < clips.Length; i++) {
                clipsString[i] = clips[i].name;
            }

            GUILayout.Space(10);
            EditorGUILayout.LabelField("Idle Animation", EditorStyles.boldLabel);
            script.choiceIndexIdle = EditorGUILayout.Popup("Idle", script.choiceIndexIdle, clipsString);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Walk Animation", EditorStyles.boldLabel);
            script.choiceIndexWalk = EditorGUILayout.Popup("Walk", script.choiceIndexWalk, clipsString);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Attack Animation", EditorStyles.boldLabel);
            script.choiceIndexAttack = EditorGUILayout.Popup("Attack", script.choiceIndexAttack, clipsString);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Hurt Animation", EditorStyles.boldLabel);
            script.choiceIndexHurt = EditorGUILayout.Popup("Hurt", script.choiceIndexHurt, clipsString);

            script.idleAnimation = clipsString[script.choiceIndexIdle];
            script.walkAnimation = clipsString[script.choiceIndexWalk];
            script.attackAnimation = clipsString[script.choiceIndexAttack];
            script.hurtAnimation = clipsString[script.choiceIndexHurt];

            EditorUtility.SetDirty(target);
        }
    }
}
#endif

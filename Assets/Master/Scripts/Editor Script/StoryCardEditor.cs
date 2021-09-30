using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardDataBase))]
[CanEditMultipleObjects]
public class StoryCardEditor : Editor
{
    SerializedProperty effect;
    SerializedProperty changedStats;

    void OnEnable()
    {
        effect = serializedObject.FindProperty("CardEffect");
        changedStats = serializedObject.FindProperty("ChangedStats");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(effect);
        serializedObject.ApplyModifiedProperties();
    }
}

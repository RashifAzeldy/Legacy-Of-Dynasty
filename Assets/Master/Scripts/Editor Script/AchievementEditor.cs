using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(AchievementScriptableObj))]
public class AchievementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AchievementScriptableObj achievement = (AchievementScriptableObj)target;

        SerializedProperty achievementName = serializedObject.FindProperty("achievementName");
        SerializedProperty achievementDesc = serializedObject.FindProperty("description");
        SerializedProperty achievementType = serializedObject.FindProperty("type");
        SerializedProperty inOneRun = serializedObject.FindProperty("inOneRun");
        SerializedProperty reward = serializedObject.FindProperty("reward");
        SerializedProperty progress = serializedObject.FindProperty("playerProgress");
        SerializedProperty completeStatus = serializedObject.FindProperty("isCompleted");
        EditorGUILayout.PropertyField(achievementName, true);
        EditorGUILayout.PropertyField(achievementDesc, true);
        EditorGUILayout.PropertyField(achievementType, true);
        EditorGUILayout.PropertyField(inOneRun, true);
        EditorGUILayout.PropertyField(progress, true);
        EditorGUILayout.PropertyField(reward, true);
        EditorGUILayout.PropertyField(completeStatus, true);

        serializedObject.ApplyModifiedProperties();

        if (achievementType.enumValueIndex == 0)
        {
            SerializedProperty cardTarget = serializedObject.FindProperty("actTarget");
            EditorGUILayout.PropertyField(cardTarget, true);
            SerializedProperty collectTarget = serializedObject.FindProperty("collectTarget");
            EditorGUILayout.PropertyField(collectTarget, true);
        }
        else if (achievementType.enumValueIndex == 1)
        {
            SerializedProperty scoreTarget = serializedObject.FindProperty("scoreTarget");
            EditorGUILayout.PropertyField(scoreTarget, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AchievementScriptableObj))]
public class AchievementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AchievementScriptableObj achievement = (AchievementScriptableObj) target;

        SerializedProperty achievementName = serializedObject.FindProperty("achievementName");
        SerializedProperty achievementDesc = serializedObject.FindProperty("description");
        SerializedProperty achievementType = serializedObject.FindProperty("type");
        EditorGUILayout.PropertyField(achievementName, true);
        EditorGUILayout.PropertyField(achievementDesc, true);
        EditorGUILayout.PropertyField(achievementType, true);

        serializedObject.ApplyModifiedProperties();

        if ( achievementType.enumValueIndex == 0 )
        {
            SerializedProperty cardTarget = serializedObject.FindProperty("actTarget");
            EditorGUILayout.PropertyField(cardTarget, true);
            SerializedProperty collectTarget = serializedObject.FindProperty("collectTarget");
            EditorGUILayout.PropertyField(collectTarget, true);
        }
        else if(achievementType.enumValueIndex == 1 )
        {
            SerializedProperty scoreTarget = serializedObject.FindProperty("scoreTarget");
            EditorGUILayout.PropertyField(scoreTarget, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}

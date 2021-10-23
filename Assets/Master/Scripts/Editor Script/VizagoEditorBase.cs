using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Radiant Story Cards

[CustomEditor(typeof(RadiantActivitiesCardData))]
[CanEditMultipleObjects]
public class VizagoEditorBase : Editor
{
    public override void OnInspectorGUI()
    {
        RadiantActivitiesCardData radiantCard = (RadiantActivitiesCardData) target;

        EditorGUILayout.LabelField("Card Config : ");

        SerializedProperty cardName = serializedObject.FindProperty("cardName");
        SerializedProperty cardStory = serializedObject.FindProperty("cardStory");
        SerializedProperty cardScore = serializedObject.FindProperty("cardScoreValue");
        SerializedProperty spawnReq = serializedObject.FindProperty("spawnRequirement");
        SerializedProperty onCollected = serializedObject.FindProperty("OnCardCollected");
        SerializedProperty cardValue = serializedObject.FindProperty("cardValue");
        SerializedProperty cardEffect = serializedObject.FindProperty("effect");

        EditorGUILayout.PropertyField(cardName, true);
        EditorGUILayout.PropertyField(cardStory, true);
        EditorGUILayout.PropertyField(cardScore, true);
        EditorGUILayout.PropertyField(spawnReq, true);
        EditorGUILayout.PropertyField(onCollected, true);
        EditorGUILayout.PropertyField(cardValue, true);
        EditorGUILayout.PropertyField(cardEffect, true);

        serializedObject.ApplyModifiedProperties();

        // 1 = Changed Stats
        if ( cardEffect.enumValueIndex == 1 )
        {
            SerializedProperty changedStats = serializedObject.FindProperty("changedStats");
            EditorGUILayout.PropertyField(changedStats, true);
            SerializedProperty changeLevel = serializedObject.FindProperty("changeLevel");
            EditorGUILayout.PropertyField(changeLevel, true);
        }
        // 2 = Can't Jump
        else if ( cardEffect.enumValueIndex == 2 )
        {
            SerializedProperty delayTime = serializedObject.FindProperty("time");
            EditorGUILayout.PropertyField(delayTime, true);
        }
        // 3 = Dead
        else
        {
            SerializedProperty causingDeath = serializedObject.FindProperty("causingDeath");
            EditorGUILayout.PropertyField(causingDeath, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
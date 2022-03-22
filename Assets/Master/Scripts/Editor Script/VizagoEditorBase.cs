using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

// Radiant Story Cards

[CustomEditor(typeof(RadiantActivitiesCardData))]
[CanEditMultipleObjects]
public class VizagoEditorBase : Editor
{
    public override void OnInspectorGUI()
    {
        RadiantActivitiesCardData radiantCard = (RadiantActivitiesCardData)target;

        EditorGUILayout.LabelField("Card Config : ");

        SerializedProperty cardName = serializedObject.FindProperty("cardName");
        SerializedProperty cardStory = serializedObject.FindProperty("cardStory");
        SerializedProperty isRandomCard = serializedObject.FindProperty("random");
        SerializedProperty cardScore = serializedObject.FindProperty("cardScoreValue");
        SerializedProperty spawnReq = serializedObject.FindProperty("spawnRequirement");
        SerializedProperty scoreCondition = serializedObject.FindProperty("scoreCondition");
        SerializedProperty onCollected = serializedObject.FindProperty("OnCardCollected");
        SerializedProperty cardValue = serializedObject.FindProperty("cardValue");
        SerializedProperty cardEffect = serializedObject.FindProperty("effect");
        SerializedProperty isStackable = serializedObject.FindProperty("stackable");


        EditorGUILayout.PropertyField(cardName, true);
        EditorGUILayout.PropertyField(cardStory, true);
        EditorGUILayout.PropertyField(isRandomCard, true);
        EditorGUILayout.PropertyField(cardScore, true);
        EditorGUILayout.PropertyField(isStackable, true);
        if (isRandomCard.boolValue == true)
        {
            SerializedProperty randomCards = serializedObject.FindProperty("randomCards");
            EditorGUILayout.PropertyField(randomCards, true);
        }
        if (isStackable.boolValue == true)
        {
            SerializedProperty stackCount = serializedObject.FindProperty("stackCount");
            EditorGUILayout.PropertyField(stackCount, true);
        }
        EditorGUILayout.PropertyField(spawnReq, true);
        EditorGUILayout.PropertyField(scoreCondition, true);
        EditorGUILayout.PropertyField(onCollected, true);
        EditorGUILayout.PropertyField(cardValue, true);
        EditorGUILayout.PropertyField(cardEffect, true);
        serializedObject.ApplyModifiedProperties();

        // 1 = Changed Stats
        if (cardEffect.enumValueIndex == 1)
        {
            SerializedProperty changedStats = serializedObject.FindProperty("changedStats");
            EditorGUILayout.PropertyField(changedStats, true);
            SerializedProperty changeLevel = serializedObject.FindProperty("changeLevel");
            EditorGUILayout.PropertyField(changeLevel, true);
        }
        // 2 = Can't Jump
        else if (cardEffect.enumValueIndex == 2)
        {
            SerializedProperty delayTime = serializedObject.FindProperty("time");
            EditorGUILayout.PropertyField(delayTime, true);
        }
        // 3 = Dead
        else if (cardEffect.enumValueIndex == 3)
        {
            SerializedProperty causingDeath = serializedObject.FindProperty("causingDeath");
            EditorGUILayout.PropertyField(causingDeath, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}

#endif

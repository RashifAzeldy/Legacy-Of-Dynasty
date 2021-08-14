using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(TextMeshSortLayer))]
public class TextMeshSortLayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        TextMeshSortLayer script = (TextMeshSortLayer)target;

        script.SetSortOrder = EditorGUILayout.IntField("Sorting Layer", script.SetSortOrder);
        TextMeshProUGUI tm = script.gameObject.GetComponent<TextMeshProUGUI>();
        if (tm != null)
        {
            tm.canvas.sortingOrder = script.SetSortOrder;
        }
    }
}

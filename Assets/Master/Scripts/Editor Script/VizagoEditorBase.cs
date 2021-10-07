using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VEditor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class VizagoEditorBase : Editor
    {
        Dictionary<string, SerializedProperty> serializeProps = new Dictionary<string, SerializedProperty>();

        protected void OnEnable()
        {
            
        }
        
        public sealed override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.indentLevel = 0;

            OnInspector();
            
            serializedObject.ApplyModifiedProperties();

        }

        /// <summary>
        /// Edit inspector properties here, all change already applied automatically
        /// </summary>
        protected virtual void OnInspector() 
        { 
        
        }

        protected void Category(string categoryName)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(categoryName);
        }

        protected SerializedProperty FindProperty(string name)
        {
            SerializedProperty res;

            if (!serializeProps.TryGetValue(name, out res))
                res = serializeProps[name] = serializedObject.FindProperty(name);
            if (res == null) 
                throw new System.ArgumentException("Can't Find : " + name);

            return res;
        }
    }
}


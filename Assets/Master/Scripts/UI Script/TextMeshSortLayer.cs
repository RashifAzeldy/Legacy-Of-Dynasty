using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextMeshSortLayer : MonoBehaviour
{
    [SerializeField] int sortOrder = 0;
    public int SetSortOrder { get { return sortOrder; } set { sortOrder = value; } }
}

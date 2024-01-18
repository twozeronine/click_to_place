using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class ClickToPlace : EditorWindow
{
    public Vector2 basicPos = new Vector2(0,0);
    public float offsetX = 0f;
    public float offsetY = 0f;
    public GameObject targetPrefab;
    private List<Vector2> objects = new List<Vector2>();
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    [MenuItem("Window/Click To Place")]
    public static void ShowWindow()
    {
        GetWindow<ClickToPlace>("Click To Place");
    }

    private void OnGUI()
    {
        Color defaultContentColor = GUI.contentColor;
        Color defaultBackgroundColor = GUI.backgroundColor;

        GUILayout.Label("Click To Place", EditorStyles.boldLabel);

        basicPos = EditorGUILayout.Vector2Field("Basic Position", basicPos);
        offsetX = EditorGUILayout.FloatField("X Offset", offsetX);
        offsetY = EditorGUILayout.FloatField("Y Offset", offsetY);

        targetPrefab = EditorGUILayout.ObjectField("Target Prefab", targetPrefab, typeof(GameObject), false) as GameObject;

        CreateGUILayoutButton("Add Object", () => objects.Add(Vector2.zero), Color.white, Color.green); 
        ClearStyle(defaultContentColor, defaultBackgroundColor);

        for (int i = 0; i < objects.Count; i++)
        {
            GUILayout.BeginHorizontal();
            objects[i] = EditorGUILayout.Vector2Field($"Object Position {i}", objects[i]);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                objects.RemoveAt(i);
                i--;
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        CreateGUILayoutButton("Create", () => CreateObject(), Color.white, Color.green); 
        CreateGUILayoutButton("Clear", () => ClearObjects(), Color.white, Color.red); 
        ClearStyle(defaultContentColor, defaultBackgroundColor);
        GUILayout.EndHorizontal();
    }

    private void CreateObject()
    {
        foreach (var position in objects)
        {
            Vector3 objectPosition = new Vector3(basicPos.x + position.x * offsetX, basicPos.y + position.y * offsetY, 0);
            
            GameObject createdObject = Instantiate(targetPrefab, objectPosition, Quaternion.identity);

            instantiatedObjects.Add(createdObject);
        }
    }

    private void ClearObjects()
    {
        foreach (var createdObject in instantiatedObjects)
        {
            DestroyImmediate(createdObject);
        }
        instantiatedObjects.Clear();
    }

    private void CreateGUILayoutButton(string guiName, Action func, Color contentColor, Color BackgroundColor)
    {
        GUI.contentColor = contentColor;
        GUI.backgroundColor = BackgroundColor;
        if(GUILayout.Button(guiName))
        {
            func();
        }
    }

    private void ClearStyle(Color defaultContentColor, Color defaultBackgroundColor)
    {
        GUI.contentColor = defaultContentColor;
        GUI.backgroundColor = defaultBackgroundColor;
    }
}
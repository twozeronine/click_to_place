using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class ClickToPlace : EditorWindow
{
    public Vector3 basicPos = new Vector3(0, 0, 0);
    public Vector3 offsetPos = new Vector3(0, 0, 0);
    public GameObject targetPrefab;
    public string jsonFilePath = "";
    public string saveFileName = "";
    private List<Vector3> objects = new List<Vector3>();
    private List<GameObject> instantiatedObjects = new List<GameObject>();
    private Vector2 scrollPosition = Vector2.zero;

    [MenuItem("Window/Click To Place")]
    public static void ShowWindow()
    {
        GetWindow<ClickToPlace>("Click To Place");
    }

    private void OnGUI()
    {
        Color defaultContentColor = GUI.contentColor;
        Color defaultBackgroundColor = GUI.backgroundColor;

        // Title
        GUILayout.Label("Click To Place", EditorStyles.boldLabel);

        // Fields
        basicPos = EditorGUILayout.Vector3Field("Basic Position", basicPos);
        EditorGUILayout.Space();
        offsetPos = EditorGUILayout.Vector3Field("Offset", offsetPos);
        EditorGUILayout.Space();
        targetPrefab = EditorGUILayout.ObjectField("Target Prefab", targetPrefab, typeof(GameObject), false) as GameObject;
        EditorGUILayout.Space();
        
        // Load From JSON
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("JSON File Path: ");
        jsonFilePath = EditorGUILayout.TextField(jsonFilePath);
        EditorGUILayout.Space();
        CreateGUILayoutButton("Load From JSON", () => LoadFromJson(jsonFilePath), Color.white, Color.magenta); 
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        // Save Objects to JSON
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Save File Name: ");
        saveFileName = EditorGUILayout.TextField(saveFileName);
        EditorGUILayout.Space();
        CreateGUILayoutButton("Save to JSON", () => SaveToJSON(saveFileName), Color.white, Color.cyan); 
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        // Add Object
        CreateGUILayoutButton("Add Object", () => objects.Add(Vector3.zero), Color.white, Color.green); 
        ClearStyle(defaultContentColor, defaultBackgroundColor);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(800));
        for (int i = 0; i < objects.Count; i++)
        {
            GUILayout.BeginHorizontal();
            objects[i] = EditorGUILayout.Vector3Field($"Object Position {i}", objects[i]);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                objects.RemoveAt(i);
                i--;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

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
            Vector3 objectPosition = new Vector3(basicPos.x + position.x * offsetPos.x, basicPos.y + position.y * offsetPos.y, basicPos.z + position.z * offsetPos.z);
            
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

    private void LoadFromJson(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        
        if( textAsset == null ) return;

        objects.Clear();

        objects = JsonConvert.DeserializeObject<List<Vector3>>(textAsset.text);
    }

    private void SaveToJSON(string fileName)
    {
        var listVec3 = VectorConverter.ConvertUnityVectoVec3(objects);
        
        JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        string json = JsonConvert.SerializeObject(listVec3, Formatting.Indented, jsonSettings);

        string filePath = $"Assets/Resources/{fileName}.json";
        System.IO.File.WriteAllText(filePath, json);
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
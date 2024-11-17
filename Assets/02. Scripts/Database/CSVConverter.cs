using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class CSVConverter : EditorWindow
{
    private TextAsset selectedCSV;

    [MenuItem("Tools/CSV To ScriptableObject Converter")]
    private static void ShowWindow()
    {
        var window = GetWindow<CSVConverter>();
        window.titleContent = new GUIContent("CSV Converter");
        window.Show();
    }

    private void OnGUI()
    {
        selectedCSV = EditorGUILayout.ObjectField("Select CSV", selectedCSV, typeof(TextAsset), false) as TextAsset;

        if (GUILayout.Button("Convert") && selectedCSV != null)
        {
            try
            {
                ProcessCSVFile(selectedCSV);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog("Success", "CSV file converted successfully!", "OK");
            }
            catch (System.Exception e)
            {
                EditorUtility.DisplayDialog("Error", $"Failed to convert CSV: {e.Message}", "OK");
                Debug.LogError($"CSV Conversion Error: {e}");
            }
        }
    }

    private System.Type FindType(string typeName)
    {
        return System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(type => type.Name == typeName);
    }

    private void ProcessCSVFile(TextAsset csvFile)
    {
        string[] lines = csvFile.text.Split('\n');
        if (lines.Length < 2) return;

        string[] headers = lines[0].Trim().Split(',');

        string baseName = Path.GetFileNameWithoutExtension(csvFile.name);
        string dbPath = "Assets/05. ScriptableObjects/Databases/";
        string dataPath = "Assets/05. ScriptableObjects/Data/";

        if (!Directory.Exists(dbPath)) Directory.CreateDirectory(dbPath);
        if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);

        string databaseTypeName = baseName + "Database";
        var databaseType = FindType(databaseTypeName);

        if (databaseType == null)
            throw new System.Exception($"Could not find database type: {databaseTypeName}");

        var database = AssetDatabase.LoadAssetAtPath<ScriptableObject>($"{dbPath}{databaseTypeName}.asset");
        if (database == null)
        {
            database = ScriptableObject.CreateInstance(databaseType);
            AssetDatabase.CreateAsset(database, $"{dbPath}{databaseTypeName}.asset");
        }

        string dataTypeName = baseName + "Data";
        var dataType = FindType(dataTypeName);

        if (dataType == null)
            throw new System.Exception($"Could not find data type: {dataTypeName}");

        // Create a generic list with the correct type
        var listType = typeof(List<>).MakeGenericType(dataType);
        var list = System.Activator.CreateInstance(listType);
        var dataList = databaseType.GetField("database");
        dataList.SetValue(database, list);

        // Get the Add method of the list
        var addMethod = listType.GetMethod("Add");

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] rowData = lines[i].Trim().Split(',');
            string assetPath = $"{dataPath}{baseName}Data_{i}.asset";

            var dataObject = ScriptableObject.CreateInstance(dataType);

            for (int j = 0; j < headers.Length; j++)
            {
                if (j >= rowData.Length) break;

                var field = dataType.GetField(headers[j]);
                if (field == null) continue;

                try
                {
                    if (field.FieldType.IsEnum)
                    {
                        field.SetValue(dataObject, System.Enum.Parse(field.FieldType, rowData[j]));
                    }
                    else switch (field.FieldType.Name)
                        {
                            case "Int32": field.SetValue(dataObject, int.Parse(rowData[j])); break;
                            case "Single": field.SetValue(dataObject, float.Parse(rowData[j])); break;
                            case "String": field.SetValue(dataObject, rowData[j]); break;
                        }
                }
                catch (System.Exception e)
                {
                    throw new System.Exception($"Error parsing field {headers[j]} with value {rowData[j]}: {e.Message}");
                }
            }

            AssetDatabase.CreateAsset(dataObject, assetPath);
            addMethod.Invoke(list, new object[] { dataObject });
        }

        EditorUtility.SetDirty(database);
    }
}
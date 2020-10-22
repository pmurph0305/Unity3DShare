using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class RenameAndCreatePrefabs : EditorWindow
{

    [MenuItem("Window/RenameAndCreatePrefabs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof (RenameAndCreatePrefabs));
    }


    private Material material;
    private string textToReplace;
    private string newText;
    private string prefabLocation;

    private bool isSkinned;
    void OnGUI()
    {
        GUILayout.Label("Tools to rename a bunch of selected gameobjects, apply material, and create prefabs");

        material = (Material) EditorGUILayout.ObjectField("Material", material, typeof (Material));
        textToReplace = EditorGUILayout.TextField("Text To Replace", textToReplace);
        newText = EditorGUILayout.TextField("New Text", newText);
        prefabLocation = EditorGUILayout.TextField("PrefabFolder:", prefabLocation);
        isSkinned = EditorGUILayout.Toggle("Skinned Object?", isSkinned);
        if (GUILayout.Button("Create Prefabs"))
        {

            GameObject[] selection = Selection.gameObjects;
            foreach (GameObject gameObj in selection)
            {
                string name = gameObj.name;
                name = name.Replace(textToReplace, newText);
                gameObj.name = name;
                if (!isSkinned)
                {
                    Renderer rend = gameObj.GetComponent<Renderer>();
                    rend.material = material;
                }
                else
                {
                    SkinnedMeshRenderer skinnedMeshRenderer = gameObj.GetComponentInChildren<SkinnedMeshRenderer>();
                    if (skinnedMeshRenderer != null)
                    {
                        skinnedMeshRenderer.material = material;
                    }
                }
                UnityEngine.Object newPrefab = PrefabUtility.CreateEmptyPrefab(prefabLocation +  name + ".prefab");
                PrefabUtility.ReplacePrefab(gameObj, newPrefab,ReplacePrefabOptions.ConnectToPrefab);
            }

        }

        GUILayout.Label("Location format: Assets/Folder/Prefabs/");
    }

}

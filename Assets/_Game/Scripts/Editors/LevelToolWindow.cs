using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelToolWindow : EditorWindow
{
    static Vector2 MIN_WINDOW_SIZE = new Vector2(350, 350);

    PlatformRow rowPrefab;
    PlatformTile tilePrefab;
    int level;
    int platformWidth = 6;
    int platformHeight = 40;

    int matchBoardWidth = 6;
    int matchBoardHeight = 5;

    int zombieSmallAmount;
    int zombieMediumAmount;
    int zombieLargeAmount;

    [MenuItem("Tools/Level Tool")]
    public static void ShowWindow()
    {
        LevelToolWindow window = GetWindow<LevelToolWindow>("Level Tool");
        window.minSize = MIN_WINDOW_SIZE;
        window.Show();
    }
    void LoadResources()
    {
        tilePrefab = Resources.Load<PlatformTile>("Prefabs/PlatformTile");
        rowPrefab = Resources.Load<PlatformRow>("Prefabs/PlatformRow");
    }
    private void OnEnable()
    {
        LoadResources();
    }
    private void OnGUI()
    {
        level = EditorGUILayout.IntField("Level", level);

        EditorGUILayout.LabelField("- Platform");

        EditorGUILayout.BeginVertical();
        {
            rowPrefab = (PlatformRow)EditorGUILayout.ObjectField("Row prefab", rowPrefab, typeof(PlatformRow), false);
            tilePrefab = (PlatformTile)EditorGUILayout.ObjectField("Tile prefab", tilePrefab, typeof(PlatformTile), false);
            platformWidth = EditorGUILayout.IntField("Width", platformWidth);
            platformHeight = EditorGUILayout.IntField("Height", platformHeight);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("- Play Board");

        EditorGUILayout.BeginVertical();
        {
            matchBoardWidth = EditorGUILayout.IntField("Width", matchBoardWidth);
            matchBoardHeight = EditorGUILayout.IntField("Height", matchBoardHeight);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("- Zombies:");

        EditorGUILayout.BeginVertical();
        {
            zombieSmallAmount = EditorGUILayout.IntField("Small", zombieSmallAmount);
            zombieMediumAmount = EditorGUILayout.IntField("Medium", zombieMediumAmount);
            zombieLargeAmount = EditorGUILayout.IntField("Large", zombieLargeAmount);
        }
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create"))
        {
            CreateLevel();
        }
    }
    void CreateLevel()
    {
        GameObject newLevelPrefab = new GameObject();
        newLevelPrefab.AddComponent(typeof(Level));
        
        PlatformRow rowPrototype = CreatePlatformRowPrototype();
        GeneratePlatformRows(newLevelPrefab.GetComponent<Level>(), rowPrototype);
        string savePath = "Assets/_Game/Resources/Prefabs/Levels/" + "Level_" + level + ".prefab";
        savePath = AssetDatabase.GenerateUniqueAssetPath(savePath);
        PrefabUtility.SaveAsPrefabAsset(newLevelPrefab, savePath);

        DestroyImmediate(rowPrototype.gameObject);
        DestroyImmediate(newLevelPrefab);
    }

    PlatformRow CreatePlatformRowPrototype()
    {
        PlatformRow row = Instantiate(rowPrefab);
        PlatformTile[] tiles = new PlatformTile[platformWidth];
        for (int r = 0; r < platformWidth; r++)
        {
            tiles[r] = Instantiate(
                tilePrefab,
                row.TF.position + r * row.TF.right,
                Quaternion.identity,
                row.TF
            );
        }
        row.Tiles = tiles;
        return row;
    }

    void GeneratePlatformRows(Level level, PlatformRow rowPrototype)
    {
        LinkedList<PlatformRow> rowLinkedList = new LinkedList<PlatformRow>();
        Vector3 rowPosX = (1 - platformWidth) * 0.5f * level.TF.right;
        for (int c = 0; c < platformHeight; c++)
        {
            Vector3 rowPosZ = c * level.TF.forward;
            PlatformRow row = Instantiate(
                rowPrototype,
                level.TF.position + rowPosX + rowPosZ,
                Quaternion.identity,
                level.TF
            );
            rowLinkedList.AddLast(row);
        }
        level.OnInit(platformWidth, platformHeight, rowLinkedList, matchBoardWidth, matchBoardHeight);
    }
}

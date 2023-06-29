using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelToolWindow : EditorWindow
{
    static Vector2 MIN_WINDOW_SIZE = new Vector2(350, 350);

    LevelData levelData;

    Level levelPrefab;
    PlatformRow rowPrefab;
    PlatformTile tilePrefab;

    int level;

    //int platformWidth = 6;
    //int platformHeight = 40;

    //int matchBoardWidth = 6;
    //int matchBoardHeight = 5;

    //int zombieSmallAmount;
    //int zombieMediumAmount;
    //int zombieLargeAmount;

    [MenuItem("Tools/Level Tool")]
    public static void ShowWindow()
    {
        LevelToolWindow window = GetWindow<LevelToolWindow>("Level Tool");
        window.minSize = MIN_WINDOW_SIZE;
        window.Show();
    }
    void LoadResources()
    {
        levelPrefab = Resources.Load<Level>("Prefabs/LevelSample");
        tilePrefab = Resources.Load<PlatformTile>("Prefabs/PlatformTile");
        rowPrefab = Resources.Load<PlatformRow>("Prefabs/PlatformRow");

        level = Resources.LoadAll("Prefabs/Levels").Length + 1;
    }
    void InitData()
    {
        levelData = CreateInstance<LevelData>();
    }
    private void OnEnable()
    {
        LoadResources();
        InitData();
    }
    private void OnGUI()
    {
        level = EditorGUILayout.IntField("Level", level);

        EditorGUILayout.LabelField("- Platform");

        EditorGUILayout.BeginVertical();
        {
            levelPrefab = (Level)EditorGUILayout.ObjectField("Level prefab", levelPrefab, typeof(Level), false);
            rowPrefab = (PlatformRow)EditorGUILayout.ObjectField("Row prefab", rowPrefab, typeof(PlatformRow), false);
            tilePrefab = (PlatformTile)EditorGUILayout.ObjectField("Tile prefab", tilePrefab, typeof(PlatformTile), false);
            levelData.Width = EditorGUILayout.IntField("Width", levelData.Width);
            levelData.Height = EditorGUILayout.IntField("Height", levelData.Height);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("- Play Board");

        EditorGUILayout.BeginVertical();
        {
            levelData.MatchBoardWidth = EditorGUILayout.IntField("Width", levelData.MatchBoardWidth);
            levelData.MatchBoardHeight = EditorGUILayout.IntField("Height", levelData.MatchBoardHeight);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("- Zombies:");

        EditorGUILayout.BeginVertical();
        {
            levelData.ZombieSmallAmount = EditorGUILayout.IntField("Small", levelData.ZombieSmallAmount);
            levelData.ZombieMediumAmount = EditorGUILayout.IntField("Medium", levelData.ZombieMediumAmount);
            levelData.ZombieLargeAmount = EditorGUILayout.IntField("Large", levelData.ZombieLargeAmount);
        }
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create"))
        {
            CreateLevel();
        }
    }
    void CreateLevel()
    {
        CreateFileData();
        CreateLevelPrefab();
        level++;
        InitData();
    }
    void CreateFileData()
    {
        string savePath = "Assets/_Game/Resources/SO/Levels/" + "Level_" + level + ".asset";
        AssetDatabase.CreateAsset(levelData, savePath);
    }
    void CreateLevelPrefab()
    {
        Level newLevelPrefab = Instantiate(levelPrefab);

        PlatformRow rowPrototype = CreatePlatformRowPrototype();
        GeneratePlatformRows(newLevelPrefab, rowPrototype);
        string savePath = "Assets/_Game/Resources/Prefabs/Levels/" + "Level_" + level + ".prefab";
        savePath = AssetDatabase.GenerateUniqueAssetPath(savePath);
        PrefabUtility.SaveAsPrefabAsset(newLevelPrefab.gameObject, savePath);

        DestroyImmediate(rowPrototype.gameObject);
        DestroyImmediate(newLevelPrefab.gameObject);
    }
    PlatformRow CreatePlatformRowPrototype()
    {
        PlatformRow row = Instantiate(rowPrefab);
        PlatformTile[] tiles = new PlatformTile[levelData.Width];
        for (int r = 0; r < levelData.Width; r++)
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
        List<PlatformRow> rows = new List<PlatformRow>();
        Vector3 rowPosX = (1 - levelData.Width) * 0.5f * level.TF.right;
        for (int c = 0; c < levelData.Height; c++)
        {
            Vector3 rowPosZ = c * level.TF.forward;
            PlatformRow row = Instantiate(
                rowPrototype,
                level.TF.position + rowPosX + rowPosZ,
                Quaternion.identity,
                level.TF
            );
            rows.Add(row);
        }
        level.Rows = rows;
        level.LevelData = levelData;
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MatchUnitData
{
    public MatchType MatchType;
    public int Level;

    public MatchUnitData(MatchType matchType)
    {
        MatchType = matchType;
        Level = 1;
    }
}

// Keys
public partial class UserData
{
    class Key
    {
        internal const string IS_FIRST_LOAD = "IsFirstLoad";

        internal static readonly string[] MATCH_UNITS_IN_USE =
        {
            "Slot1",
            "Slot2",
            "Slot3",
            "Slot4"
        };
    }
}

[CreateAssetMenu(fileName = "UserData", menuName = "Scriptable Objects/User Data")]
public partial class UserData : SingletonScriptableObject<UserData>
{
    readonly MatchType[] DEFAULT_MATCH_UNITS =
    {
        MatchType.Melee1,
        MatchType.Melee2,
        MatchType.Range1,
        MatchType.Range2,
    };

    readonly MatchUnitData[] matchUnitsInUse = new MatchUnitData[Constant.SELECT_MU_SLOT];
    public MatchUnitData[] MatchUnitsInUse => matchUnitsInUse;

    void OnFirstLoad()
    {
        Debug.Log("First time load user data.");
        SetBoolData(Key.IS_FIRST_LOAD, false);

        // Unlock default heros and save their datas
        for (int i = 0; i < Constant.SELECT_MU_SLOT; i++)
        {
            string key = DEFAULT_MATCH_UNITS[i].ToString();
            MatchUnitData heroData = new MatchUnitData(DEFAULT_MATCH_UNITS[i]);
            SetDataState(key, DataState.Unlocked);
            SetObjectData(key, heroData);
            SetStringData(Key.MATCH_UNITS_IN_USE[i], key);
        }
    }

    void OnNormalLoad()
    {
        LoadMatchUnitsInUse();
    }

    void LoadMatchUnitsInUse()
    {
        for (int i = 0; i < Constant.SELECT_MU_SLOT; i++)
        {
            matchUnitsInUse[i] = GetObjectData<MatchUnitData>(PlayerPrefs.GetString(Key.MATCH_UNITS_IN_USE[i]));
        }
    }

#if UNITY_EDITOR
    [Space(10)]
    [Header("---- Editor ----")]
    public bool TestMode;
#endif

    public void OnLoadData()
    {
#if UNITY_EDITOR
        if (TestMode) return;
#endif
        bool isFirstLoad = PlayerPrefs.GetInt(Key.IS_FIRST_LOAD, 1) == 1;
        if (isFirstLoad)
        {
            OnFirstLoad();
        }
        OnNormalLoad();
    }
    public void OnSaveData()
    {

    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnLoadData();
    }
}
public enum DataState
{
    Lock = 0,
    Unlocked = 1,
    Selected = 2,
}

// Data methods
public partial class UserData
{
    const string STATE_PREFIX = "STATE_";
    public DataState GetDataState(string key, DataState state = 0) => (DataState)PlayerPrefs.GetInt(key, (int)state);
    public DataState GetDataState(string key, int ID, DataState state = DataState.Lock) => GetDataState(key + ID, (int)state);
    public T GetEnumData<T>(string key, T defaultValue) where T : System.Enum
        => (T)System.Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, (int)(object)defaultValue));

    public T GetObjectData<T>(string key, T defaultValue = null) where T : class
    {
        string json = PlayerPrefs.GetString(key, null);
        if (json != null)
        {
            return JsonUtility.FromJson<T>(json);
        }
        return defaultValue;
    }
    public void SetIntData(string key, int value) => PlayerPrefs.SetInt(key, value);
    public void SetFloatData(string key, float value) => PlayerPrefs.SetFloat(key, value);
    public void SetStringData(string key, string value) => PlayerPrefs.SetString(key, value);
    public void SetBoolData(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);
    public void SetEnumData<E>(string key, E value) => PlayerPrefs.SetInt(key, (int)(object)value);
    public void SetObjectData<T>(string key, T value) => PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
    public void SetDataState(string key, int ID, DataState state) => SetDataState(key + ID, state);
    public void SetDataState(string key, DataState state) => PlayerPrefs.SetInt(STATE_PREFIX + key, (int)state);

    public void SetIntData(string key, ref int variable, int value)
    {
        variable = value;
        SetIntData(key, value);
    }
    public void SetBoolData(string key, ref bool variable, bool value)
    {
        variable = value;
        SetBoolData(key, value);
    }

    public void SetFloatData(string key, ref float variable, float value)
    {
        variable = value;
        SetFloatData(key, value);
    }

    public void SetStringData(string key, ref string variable, string value)
    {
        variable = value;
        SetStringData(key, value);
    }

    public void SetEnumData<T>(string key, ref T variable, T value)
    {
        variable = value;
        SetEnumData(key, value);
    }
    public void SetObjectData<T>(string key, ref T variable, T value)
    {
        variable = value;
        PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
    }
}

// Editor

#if UNITY_EDITOR
[CustomEditor(typeof(UserData), true)]
public class UserDataEditor : Editor
{
    UserData userData;
    protected void OnEnable()
    {
        userData = (UserData)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Load Data"))
        {
            userData.OnLoadData();
            EditorUtility.SetDirty(userData);
        }
        if (GUILayout.Button("Clear Data"))
        {
            userData.OnResetData();
            EditorUtility.SetDirty(userData);
        }
        if (GUILayout.Button("Save Data"))
        {
            userData.OnSaveData();
            EditorUtility.SetDirty(userData);
        }
    }
}
#endif
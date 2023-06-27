using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    GamePlay = 1,
}
public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    private static GameState gameState;
    public bool openUIWhenPlay;

    protected void Awake()
    {
        GameSetup();
        LoadData();

        ChangeState(GameState.MainMenu);

        if (openUIWhenPlay)
        {
            UIManager.Ins.OpenUI<UIMainMenu>();
        }
    }

    void GameSetup()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }
    void LoadData()
    {
        //csv.OnInit();
        UserData.Ins.OnLoadData();
    }

    public static void ChangeState(GameState state)
    {
        gameState = state;
        PlayerLineOfSight.Ins.ChangeState(state);
    }

    public static bool IsState(GameState state) => gameState == state;

}

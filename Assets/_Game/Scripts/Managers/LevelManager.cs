using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] PlatformBoard platformBoard;
    [SerializeField] MatchBoard matchBoard;
    [SerializeField] SampleStatsCollection sampleStatsCollection;


    //public SampleStatsCollection SampleStatsCollection => sampleStatsCollection;

    public void OnStart()
    {
        GameManager.ChangeState(GameState.GamePlay);
        platformBoard.StopFloating();
        Invoke(nameof(OnInitLevel), 1f);
    }
    public async void OnStartSync()
    {
        GameManager.ChangeState(GameState.GamePlay);
        await Task.WhenAll(platformBoard.GetFloatingTasks());
        OnInitLevel();
    }
    void OnInitLevel()
    {
        matchBoard.OnInit(platformBoard, 6, 5);
    }
}


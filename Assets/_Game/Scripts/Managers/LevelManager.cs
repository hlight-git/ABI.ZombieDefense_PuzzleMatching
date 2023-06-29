using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] MatchBoard matchBoard;
    [SerializeField] SampleStatsCollection sampleStatsCollection;

    [SerializeField] Level[] levels;
    Level currentLevel;


    //public SampleStatsCollection SampleStatsCollection => sampleStatsCollection;

    public void OnStart()
    {
        GameManager.ChangeState(GameState.GamePlay);
        currentLevel.StopFloating();
        Invoke(nameof(OnInitLevel), 1f);
    }
    public async void OnStartSync()
    {
        GameManager.ChangeState(GameState.GamePlay);
        await Task.WhenAll(currentLevel.GetFloatingTasks());
        OnInitLevel();
    }
    void OnInitLevel()
    {
        matchBoard.OnInit(currentLevel);
    }

    void SpawnZombie(ZombieType zombieType)
    {
        
        //Vector3 position = platformBoard.RowLinkedList.Last.Value.
    }
    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[level]);
        currentLevel.StartFloating();
    }
    public void OnLoadCurrentLevel() => OnLoadLevel(UserData.Ins.CurrentLevel);
}


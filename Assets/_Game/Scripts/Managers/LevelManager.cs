using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    Level currentLevel;

    MatchBoard matchBoard => currentLevel.MatchBoard;
    private void Start()
    {
        OnLoadCurrentLevel();
    }
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
        matchBoard.OnInit(currentLevel.RowLinkedList.First);
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


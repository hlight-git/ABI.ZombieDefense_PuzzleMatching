using HlightSDK;
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
        matchBoard.OnInit(currentLevel.RowLinkedList.First, currentLevel.LevelData.MatchBoardWidth, currentLevel.LevelData.MatchBoardHeight);
    }
    void SpawnZombie()
    {

    }
    void SpawnZombie(ZombieType zombieType)
    {
        PlatformRow row = currentLevel.RowLinkedList.Last.Value;
        PlatformTile spawnTile = row.Tiles.Choice();
        SimplePool.Spawn<Zombie>((PoolType)zombieType, spawnTile.StandPosTF.position, Quaternion.Euler(0, 180, 0));
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


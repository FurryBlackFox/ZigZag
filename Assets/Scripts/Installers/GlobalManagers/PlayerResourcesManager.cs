using DefaultNamespace;
using GameStateMachine.GameStates;
using Signals;
using UnityEngine;
using Utils.SavableData;using Zenject;

public class PlayerResourcesManager
{
    public IntegerSavableData CurrentScore { get; private set; } =
        new IntegerSavableData(StaticData.CurrentScoreSaveKey, false);
    public IntegerSavableData HighScoreData { get; private set; } =
        new IntegerSavableData(StaticData.HighScoreSaveKey);
    public IntegerSavableData CollectedJewelsCount { get; private set; } =
        new IntegerSavableData(StaticData.CollectedJewelsCountSaveKey);
    
    public IntegerSavableData PlayedGamesCount { get; private set; } =
        new IntegerSavableData(StaticData.PlayedGamesCountSaveKey);
    
    private SignalBus _signalBus;

    [Inject]
    public void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        
        LoadData();
        
        _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
        _signalBus.Subscribe<OnPlayerCollectedJewel>(AppendJewel);
        _signalBus.Subscribe<OnPlayerChangedMoveDirection>(AppendScorePoint);
    }

    ~PlayerResourcesManager()
    {
        _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
        _signalBus.Unsubscribe<OnPlayerCollectedJewel>(AppendJewel);
        _signalBus.Unsubscribe<OnPlayerChangedMoveDirection>(AppendScorePoint);
        
        PlayerPrefs.Save();
    }
    
    private void LoadData()
    {
        HighScoreData.Load();
        CollectedJewelsCount.Load();
        PlayedGamesCount.Load();   
    }

    public void ClearScore()
    {
        CurrentScore.SetValue(0);
    }

    private void OnGameStateChanged(OnGameStateChanged stateChangedEvent)
    {
        var prevStateType = stateChangedEvent.prevStateType;
        switch (stateChangedEvent.currentStateType)
        {
            case GameStateType.Play when prevStateType == GameStateType.MainMenu:
                AppendPlayedGamesCount();
                break;
            case GameStateType.MainMenu:
                ClearScore();
                break;
        }
    }

    public void AppendJewels(int appendedValue)
    {
    }
    
    public void AppendScorePoint()
    {
        CurrentScore.SetValue(CurrentScore.Value + 1);
        if (CurrentScore.Value > HighScoreData.Value)
            HighScoreData.SetValue(CurrentScore.Value);
    }

    private void AppendJewel()
    {
        CollectedJewelsCount.SetValue(CollectedJewelsCount.Value + 1);
        Debug.LogError($"Jewels: {CollectedJewelsCount.Value}");
    }

    private void AppendPlayedGamesCount()
    {
        PlayedGamesCount.SetValue(PlayedGamesCount.Value + 1);
        Debug.LogError($"Played Games: {PlayedGamesCount.Value}");
    }
}

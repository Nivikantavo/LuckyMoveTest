using System;
using UnityEngine;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    [SerializeField] private UIHud _hud;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private TileSpawner _tileSpawner;
    [SerializeField] private FortuneWheelView _wheelView;
    [SerializeField] private Button _goButton;

    private StepController _controller;
    private PlayerModel _player;

    private void Start()
    {
        _player = new PlayerModel();
        var spawner = new TestSpawner();
        var deathChecker = new DeathChecker(baseChance: 0.2f, safeSteps: 3, stepDifficultySlope: 0.01f);
        var wheel = new FortuneWheelService();
        
        _controller = new StepController(_player, deathChecker, wheel);

        _tileSpawner.Init(spawner);
        var collectItemsMediator = new CollectItemMediator(_player, _tileSpawner);
        _controller.OnPlayerGoOnStep += _tileSpawner.CollectAtStep;
        _controller.OnRunEnded += result => _wheelView.Show(result, () => GetNextMove(result)?.Invoke());
        _hud.Init(_player);

        _tileSpawner.EnsureTilesUpTo(0);

        _playerView.MoveToStep(_tileSpawner.GetTileHolderPos(0).position);
        _playerView.transform.position = _tileSpawner.GetTileHolderPos(0).position;
    }

    private void OnEnable()
    {
        _goButton.onClick.AddListener(OnGoClicked);
    }

    private void OnDisable()
    {
        _goButton.onClick.RemoveListener(OnGoClicked);
    }
    private void OnGoClicked()
    {
        if (!_playerView.IsAtTargetPosition()) return;

        _controller.PerformStep();
        _playerView.MoveToStep(_tileSpawner.GetTileHolderPos(_player.Steps).position);
        _tileSpawner.EnsureTilesUpTo(_player.Steps);
    }

    private Action GetNextMove(WheelResult result)
    {
        switch (result)
        {
            case { Segment: WheelSegment.Success }:
            case { Segment: WheelSegment.Bonus }:
            case { Segment: WheelSegment.Fail }:
                return () =>
                {
                    Debug.Log("level ended, showing panel");
                    _hud.ShowEndLevelPanel();
                };

            case { Segment: WheelSegment.SecondChance }:
                return () => { };
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

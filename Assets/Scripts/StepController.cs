using System;
using UnityEngine;

public class StepController
{
    private readonly IPlayerModel _player;
    private readonly IDeathChecker _deathChecker;
    private readonly IFortuneWheelService _wheelService;

    public event Action<int> OnPlayerGoOnStep;
    public event Action OnShieldUsed;
    public event Action<WheelResult> OnRunEnded;

    public StepController(IPlayerModel player, 
        IDeathChecker deathChecker, IFortuneWheelService wheelService)
    {
        _player = player;
        _deathChecker = deathChecker;
        _wheelService = wheelService;
    }

    public void PerformStep()
    {
        _player.AddStep();
        OnPlayerGoOnStep?.Invoke(_player.Steps);

        bool isDead = _deathChecker.CheckDeath(_player.Steps, _player.Shields);
        if (!isDead)
            return;

        if (_player.ConsumeShield())
        {
            OnShieldUsed?.Invoke();
            return;
        }

        var wheelResult = _wheelService.Spin(_player.Coins);
        OnRunEnded?.Invoke(wheelResult);
    }
}

using UnityEngine;

public class DeathChecker : IDeathChecker
{
    private readonly float _baseChance;
    private readonly int _safeSteps;
    private readonly float _stepDifficultySlope;

    public DeathChecker(float baseChance, int safeSteps, float stepDifficultySlope)
    {
        _baseChance = Mathf.Clamp01(baseChance);
        _safeSteps = Mathf.Max(0, safeSteps);
        _stepDifficultySlope = Mathf.Max(0f, stepDifficultySlope);
    }

    public bool CheckDeath(int steps, int shields)
    {
        if (steps <= _safeSteps)
            return false;

        float chance = _baseChance * (1f + steps * _stepDifficultySlope);
        chance = Mathf.Clamp01(chance);
        Debug.Log($"Death chance: {chance:P2} at step {steps} with {shields} shields.");
        return UnityEngine.Random.value < chance;
    }
}


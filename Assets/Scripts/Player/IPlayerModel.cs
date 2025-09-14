using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IPlayerModel
{ 
    int Coins { get; }
    int Shields { get; }
    int Steps { get; }

    void AddCoins(int amount);
    bool TryAddShield();
    bool ConsumeShield();
    void AddStep();
    void ResetRun(); 
}

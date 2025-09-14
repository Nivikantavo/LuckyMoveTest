public class PlayerModel : IPlayerModel
{
    public int Coins { get; private set; }
    public int Shields { get; private set; }
    public int Steps { get; private set; }

    private readonly int maxShields;

    public PlayerModel(int maxShields = 3)
    {
        this.maxShields = maxShields;
    }

    public void AddCoins(int amount) => Coins += amount;
    public bool TryAddShield()
    {
        if (Shields >= maxShields) return false;
        Shields++;
        return true;
    }
    public bool ConsumeShield()
    {
        if (Shields <= 0) return false;
        Shields--;
        return true;
    }
    public void AddStep() => Steps++;
    public void ResetRun() { Coins = 0; Shields = 0; Steps = 0; }
}

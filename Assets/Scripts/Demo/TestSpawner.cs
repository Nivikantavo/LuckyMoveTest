using System;

public class TestSpawner : ISpawner
{
    private readonly Random _rng = new Random();

    public FieldItem SpawnItemAtStep(int step)
    {
        double roll = _rng.NextDouble();

        if (roll < 0.70)
            return new FieldItem(ItemType.Coin);

        if (roll < 0.75)
            return new FieldItem(ItemType.Shield);

        return null;
    }
}

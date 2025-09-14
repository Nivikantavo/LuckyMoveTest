using System;
using System.Collections.Generic;
using UnityEngine;

public enum WheelSegment
{
    Success,
    Fail,
    Bonus,
    SecondChance
}

public struct WheelResult
{
    public WheelSegment Segment;
    public int CoinsAfterResult;
}

public interface IFortuneWheelService
{
    WheelResult Spin(int coinsCollected);
}

public class FortuneWheelService : IFortuneWheelService
{
    public WheelResult Spin(int coinsCollected)
    {
        var segments = BuildWeightedSegments(coinsCollected);

        int index = UnityEngine.Random.Range(0, segments.Count);
        var segment = segments[index];

        return ApplyEffect(segment, coinsCollected);
    }

    private List<WheelSegment> BuildWeightedSegments(int coins)
    {
        var segments = new List<WheelSegment>();

        int success = 3;
        int fail = 3;
        int bonus = 2;

        int bonusSteps = coins / 2;
        success += bonusSteps;
        bonus += bonusSteps;
        fail = Math.Max(1, fail - bonusSteps);

        for (int i = 0; i < success; i++) segments.Add(WheelSegment.Success);
        for (int i = 0; i < fail; i++) segments.Add(WheelSegment.Fail);
        for (int i = 0; i < bonus; i++) segments.Add(WheelSegment.Bonus);

        if (coins >= 10)
            segments.Add(WheelSegment.SecondChance);

        return segments;
    }

    private WheelResult ApplyEffect(WheelSegment seg, int coins)
    {
        Debug.Log($"Wheel landed on: {seg}, coins before: {coins}");
        return seg switch
        {
            WheelSegment.Success => new WheelResult { Segment = seg, CoinsAfterResult = coins },
            WheelSegment.Fail => new WheelResult { Segment = seg, CoinsAfterResult = 0 },
            WheelSegment.Bonus => new WheelResult { Segment = seg, CoinsAfterResult = coins * 2 },
            WheelSegment.SecondChance => new WheelResult { Segment = seg, CoinsAfterResult = coins },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
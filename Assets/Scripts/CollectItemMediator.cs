using System;
using UnityEngine;

public class CollectItemMediator : IDisposable
{
    private PlayerModel _playerModel;
    private TileSpawner _tileSpawner;

    public CollectItemMediator(PlayerModel player, TileSpawner tileSpawner)
    {
        _playerModel = player;
        _tileSpawner = tileSpawner;

        _tileSpawner.OnItemCollected += OnItemCollected;
    }

    private void OnItemCollected(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Coin:
                _playerModel.AddCoins(1);
                break;
            case ItemType.Shield:
                _playerModel.TryAddShield();
                break;
            default:
                Debug.LogWarning($"Unhandled item type: {itemType}");
                break;
        }
    }

    public void Dispose()
    {
        _tileSpawner.OnItemCollected -= OnItemCollected;
    }
}

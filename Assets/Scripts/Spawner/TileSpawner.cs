using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public event Action<ItemType> OnItemCollected;

    [SerializeField] private TileView tilePrefab;
    [SerializeField] private List<CollectibleView> collectiblePrefabs;
    [SerializeField] private Transform _tileContainer;

    [SerializeField] private float _tileDistance = 5;

    private List<TileView> _tiles = new List<TileView>();
    private readonly Dictionary<int, CollectibleView> _collectibles = new();

    private ISpawner _logicSpawner;
    private int _lastSpawnedStep = -1;

    public void Init(ISpawner spawner)
    {
        _logicSpawner = spawner;
    }

    public void EnsureTilesUpTo(int step)
    {
        while (_lastSpawnedStep < step + 5)
        {
            _lastSpawnedStep++;
            SpawnTile(_lastSpawnedStep);
        }
    }

    private void SpawnTile(int step)
    {
        Vector3 pos = new Vector2(_tileContainer.position.x + step * _tileDistance, _tileContainer.position.y);
        TileView tile = Instantiate(tilePrefab, pos, Quaternion.identity, _tileContainer);

        _tiles.Add(tile);

        if (_tiles.Count == 1)
            return;

        var item = _logicSpawner.SpawnItemAtStep(step);
        if (item != null)
        {
            CollectibleView prefab = collectiblePrefabs.FirstOrDefault(p => p.Type == item.Type);

            if (prefab != null)
            {
                var collectible = Instantiate(prefab, tile.HolderPosition.position, Quaternion.identity, tile.HolderPosition);
                collectible.Init(step);

                _collectibles[step] = collectible;
            }
        }
    }

    public void CollectAtStep(int step)
    {
        if (_collectibles.TryGetValue(step, out var view))
        {
            view.PlayPickupEffect();
            _collectibles.Remove(step);
            OnItemCollected?.Invoke(view.Type);
        }
    }

    public Transform GetTileHolderPos(int tileIndex)
    {
        return _tiles[tileIndex].HolderPosition;
    }
}
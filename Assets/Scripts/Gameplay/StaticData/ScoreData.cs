using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StaticData
{
    [Serializable]
    public sealed class ScoreData
    {
        [SerializeField] private int _itemsCount;
        [SerializeField] private int _scoreCount;

        public int ItemsCount => _itemsCount;
        public int ScoreCount => _scoreCount;
    }
}
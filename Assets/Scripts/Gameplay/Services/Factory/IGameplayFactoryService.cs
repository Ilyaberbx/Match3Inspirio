﻿using System.Threading.Tasks;
using Inspirio.Core;
using Inspirio.Gameplay.Actors;
using UnityEngine;

namespace Inspirio.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        Task<GameBoardActor> CreateGameBoardAsync(int index);
        Task<ItemActor> CreateItemAsync(int index, Transform parent);
        Task<TileActor> CreateTileAsync(int x, int y, Transform parent);
        void Dispose<TActor>(TActor actor) where TActor : MonoActor;
    }
}
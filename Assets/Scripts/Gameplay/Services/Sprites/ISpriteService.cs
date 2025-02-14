using UnityEngine;

namespace Inspirio.Gameplay.Services.Sprites
{
    public interface ISpriteService
    {
        Sprite GetTileSprite(Vector2Int point);
        Sprite GetItemSprite(int id);
    }
}
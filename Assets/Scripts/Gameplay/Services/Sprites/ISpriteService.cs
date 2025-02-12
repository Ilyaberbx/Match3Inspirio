using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Sprites
{
    public interface ISpriteService
    {
        Sprite GetTileSprite(Vector2Int point);
        Sprite GetItemSprite(int id);
        Sprite GetStatusSprite(int index);
    }
}
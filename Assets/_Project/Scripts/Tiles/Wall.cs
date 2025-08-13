using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    [CreateAssetMenu(fileName = "New Wall", menuName = "Cookieman/Tiles/Wall")]
    public class Wall : RuleTile<Wall.Neighbor>
    {
        [SerializeField] private List<TileBase> walkableTiles;

        public class Neighbor : RuleTile.TilingRuleOutput.Neighbor
        {
            public const int Path = 3;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case Neighbor.Path: return walkableTiles.Contains(tile);
            }

            return base.RuleMatch(neighbor, tile);
        }
    }
}
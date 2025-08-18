using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid.Tiles
{
    [CreateAssetMenu(fileName = "New Path", menuName = "Cookieman/Tiles/Path")]
    public class Path : RuleTile<Path.Neighbor>
    {
        [SerializeField] private List<TileBase> wallTiles;

        public class Neighbor : RuleTile.TilingRuleOutput.Neighbor
        {
            public const int Wall = 4;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case Neighbor.Wall: return wallTiles.Contains(tile);
            }

            return base.RuleMatch(neighbor, tile);
        }
    }
}
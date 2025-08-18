using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid.Tiles
{
    [CreateAssetMenu(fileName = "New Chamber", menuName = "Cookieman/Tiles/Chamber")]
    public class Chamber: RuleTile<Chamber.Neighbor>
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
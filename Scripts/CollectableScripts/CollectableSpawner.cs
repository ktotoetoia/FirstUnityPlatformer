using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectableSpawner : MonoBehaviour
{
    private float offset = 0.5f;

    [SerializeField] private Tilemap CollectableTilemap;
    [SerializeField] private Tilemap DamagableTilemap;

    [SerializeField] private Tile cherry;
    [SerializeField] private GameObject cherryPrefab;

    [SerializeField] private Tile gem;
    [SerializeField] private GameObject gemPrefab;

    [SerializeField] private Tile Spike;
    [SerializeField] private GameObject spikePrefab;

    private Dictionary<Tile, GameObject> tiles = new Dictionary<Tile, GameObject>();

    private void Start()
    {
        tiles[cherry] = cherryPrefab;
        tiles[gem] = gemPrefab;
        tiles[Spike] = spikePrefab;

        SetAllPrefabs(CollectableTilemap);
        SetAllPrefabs(DamagableTilemap);
    }

    private void SetAllPrefabs(Tilemap tilemap)
    {
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            Tile tile = tilemap.GetTile<Tile>(position);
            if (tile == null) continue;

            if (tiles.ContainsKey(tile))
            {
                setPrefab(position, tiles[tile],tilemap);
            }
        }
    }

    private void setPrefab(Vector3Int position,GameObject objToSpawn,Tilemap tilemap)
    {
        Vector2 selfOffset = new Vector2(offset + objToSpawn.transform.position.x, offset + objToSpawn.transform.position.y);
        Vector2 vec = new Vector2(position.x, position.y);

        Instantiate(objToSpawn,vec+selfOffset,Quaternion.identity);

        tilemap.SetTile(position,null);
    }
}

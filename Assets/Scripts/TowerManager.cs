using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{

    [SerializeField] GameObject towerPrefab;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase allowed;
    List<Vector3> positionsFree;
    // Start is called before the first frame update
    void Start()
    {
        positionsFree = new List<Vector3>();
        SelectTilesOfType(allowed);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void CreateTower()
    {
        Instantiate(towerPrefab, positionsFree[0], Quaternion.identity);
        positionsFree.RemoveAt(0);
    }

    
    private void SelectTilesOfType(TileBase tileType)
    {
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile == tileType)
                {
                    // Do something with the selected tile
                    positionsFree.Add(new Vector3(x, y, 0));
                    Debug.Log("Selected tile at position: " + new Vector3(x, y, 0));
                }
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{
    [Header("Posicionamiento")]
    [SerializeField] GameObject towerPrefab;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase allowed;
    [SerializeField] Sprite spriteToDraw;
    [SerializeField] GameObject panelUI;

    ShopManager shopManager;
    List<Vector3> positionsFree;

    //Preview
    SpriteRenderer spriteRenderer;
    GameObject spriteObj;


    bool positioning = false;
    // Start is called before the first frame update
    void Start()
    {
        positionsFree = new List<Vector3>();
        SelectTilesOfType(allowed);
        shopManager = FindObjectOfType<ShopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (positioning)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3Int cellPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(mousePos));
            TileBase tile = tilemap.GetTile(cellPos);


            spriteObj.transform.position = cellPos;
            
            
            if (Input.GetMouseButtonDown(0))
            {
                if (tile == allowed)
                {
                    
                    Vector3 tilePos = tilemap.CellToWorld(cellPos);
                    if (!positionsFree.Contains(tilePos))
                    {
                        Debug.Log("Ya hay una torre ahi");
                    }
                    else
                    {
                        Instantiate(towerPrefab, tilePos, Quaternion.identity);
                        shopManager.ChangeMoney(-50);
                        
                        positionsFree.Remove(tilePos);
                    }
                }
                else
                {
                    Debug.Log("Tile not Allowed");
                }
                positioning = false;
                panelUI.SetActive(false);
                Destroy(spriteObj);

            }
        }


    }

    public void CreateTower()
    {
        if (shopManager.GetMoney() >= 50 && !positioning)
        {
            positioning = true;

            spriteObj = new GameObject();
            spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteToDraw;
            spriteRenderer.sortingLayerName = "Towers"; 
            SetAlpha(0.5f);
            panelUI.SetActive(true);
        }
        else
        {
            Debug.Log("Dinero Insuficiente");
        }

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
                }
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}

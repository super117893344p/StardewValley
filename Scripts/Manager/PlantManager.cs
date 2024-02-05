using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// using Tile = UnityEngine.WSA.Tile;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get ; private set ;}
    public Tilemap interactableMap ;
    public  Tile interactableTile ;
    public Tile   groundHoedTile ;

    void Awake()
    {
        Instance =this ;
    }

    void Start()
    {
        InitInteractableMap();
    }

    void InitInteractableMap()
    {
        foreach ( Vector3Int position in interactableMap.cellBounds.allPositionsWithin)
        {
             TileBase tile = interactableMap.GetTile(position);
             if (tile !=null )
             {
                  interactableMap.SetTile(position,interactableTile);
             }
        }
    }

    public void GroundHoe(Vector3  position)
    {
        Vector3Int tilePosition = interactableMap.WorldToCell( position) ; //Vector3转化为Vector3Int
        TileBase tile = interactableMap.GetTile(tilePosition) ;
        if (tile!=null && interactableTile.name==tile.name)
        {
             interactableMap.SetTile(tilePosition,groundHoedTile)  ;
        }
    }
}

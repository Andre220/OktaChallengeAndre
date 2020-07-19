using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="CustomColliderSquareTile", menuName = "scriptables/CustomTiles/CustomColliderSquare", order = 0)]
public class CustomTile : TileBase
{
    public Sprite sprite;
    public GameObject prefab;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        //base.GetTileData(position, tilemap, ref tileData);
        tileData.sprite = sprite;
        tileData.gameObject = prefab;
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        //return base.StartUp(position, tilemap, go);
        
        if (go != null)
        {
            go.transform.rotation = Quaternion.identity;  
        }

        return true;
    }
}

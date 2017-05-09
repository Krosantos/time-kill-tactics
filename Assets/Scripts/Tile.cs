using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

    public List<Tile> Neighbours
        {
            get{
                var result = new List<Tile>();
                if(Ul != null) result.Add(Ul);
                if(Uu != null) result.Add(Uu);
                if(Ur != null) result.Add(Ur);
                if(Dl != null) result.Add(Dl);
                if(Dd != null) result.Add(Dd);
                if(Dr != null) result.Add(Dr);
                return result;
            }
        }
    public Tile Ul
        {
            get
            {
                if (X%2 == 0 && X > 0)
                {
                    if (TileMap[X - 1, Y] != null) return TileMap[X - 1, Y];
                }
                else if (X > 0 && Y + 1 < TileMap.GetLength(1))
                {
                    if (TileMap[X - 1, Y + 1] != null) return TileMap[X - 1, Y + 1];
                }
                return null;
            }
        }
    public Tile Uu
        {
            get
            {
                if (Y + 1 < TileMap.GetLength(1))
                {
                    if (TileMap[X, Y + 1] != null) return TileMap[X, Y + 1];
                }
                return null;
            }
        }
    public Tile Ur
        {
            get
            {
                if (X%2 == 0 && X + 1 < TileMap.GetLength(0))
                {
                    if (TileMap[X + 1, Y] != null) return TileMap[X + 1, Y];
                }
                else if (X%2 != 0 && X + 1 < TileMap.GetLength(0) && Y + 1 < TileMap.GetLength(1))
                {
                    if (TileMap[X + 1, Y + 1] != null) return TileMap[X + 1, Y + 1];
                }
                return null;
            }
        }
    public Tile Dl
        {
            get
            {
                if (X%2 == 0 && X > 0 && Y > 0)
                {
                    if (TileMap[X - 1, Y - 1] != null) return TileMap[X - 1, Y - 1];
                }
                else if (X%2 != 0 && X > 0)
                {
                    if (TileMap[X - 1, Y] != null) return TileMap[X - 1, Y];
                }
                return null;
            }
        }
    public Tile Dd
        {
            get
            {
                if (Y > 0)
                {
                    if (TileMap[X, Y - 1] != null) return TileMap[X, Y - 1];
                }
                return null;
            }
        }
    public Tile Dr
        {
            get
            {
                if (X%2 == 0 && X + 1 < TileMap.GetLength(0) && Y > 0)
                {
                    if (TileMap[X + 1, Y - 1] != null) return TileMap[X + 1, Y - 1];
                }
                else if (X%2 != 0 && X + 1 < TileMap.GetLength(0))
                {
                    if (TileMap[X + 1, Y] != null) return TileMap[X + 1, Y];
                }
                return null;
            }
        }
    public bool DontDraw;
    public static List<Tile> AllTiles;
    public static Tile[,] TileMap;
    public static GameObject Filler;
    public Sprite Sprite
    {
        get
        {
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        set { gameObject.GetComponent<SpriteRenderer>().sprite = value; }
    }
    public SpriteDict[] SpriteDictionary;

    [NonSerialized]
    public Unit Unit;
    public int Height, X, Y;
    public bool Passable, Blocking;
    public Terrain Terrain;
    public delegate void OnTurn(Tile self);

    public void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Height + Y*-1;
        transform.position = getTransformFromCoords(X, Y, Height);
        DrawDown();
        UpdateSprite();
    }

    private Vector3 getTransformFromCoords(int x, int y, int z)
    {
        return new Vector3(x + (y % 2) * 0.5f, 0.75f * y + 0.35f * z);
    }

    private void DrawDown()
    {
        for(var x = 0; x < transform.childCount; x++)
        {
            Destroy(transform.GetChild(x).gameObject);
        }
        if (DontDraw) return;
        for (var z = Height; z> 0; z--)
        {
            var filler = Instantiate(Filler, getTransformFromCoords(X, Y, z-1), Quaternion.identity);
            filler.transform.parent = transform;
            filler.GetComponent<SpriteRenderer>().sortingOrder = (z-1) + Y*-1;
        }
    }

    private void UpdateSprite()
    {
        foreach(var pair in SpriteDictionary)
        {
            if(pair.Name == Terrain.ToString())
            {
                Sprite = pair.Sprite;
            }
        }
    }

}

[Serializable]
public struct SpriteDict
{
    public string Name;
    public Sprite Sprite;
}

public enum Terrain
{
    Grass,
    Dirt,
    Rock,
    Water,
    Snow,
    Ice,
    Sand,
    Lava
}

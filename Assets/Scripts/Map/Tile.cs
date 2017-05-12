using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Tile : MonoBehaviour, IPointerClickHandler {

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

    [NonSerialized]
    public Unit Unit;
    public int Height, X, Y;
    public bool Passable, Blocking;
    public Terrain Terrain;
    public delegate void OnTurn(Tile self);

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}

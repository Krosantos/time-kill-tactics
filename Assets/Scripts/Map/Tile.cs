using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Tile : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{

    public List<Tile> Neighbours
    {
        get
        {
            var result = new List<Tile>();
            if (Ul != null) result.Add(Ul);
            if (Ur != null) result.Add(Ur);
            if (Rr != null) result.Add(Rr);
            if (Dr != null) result.Add(Dr);
            if (Dl != null) result.Add(Dl);
            if (Ll != null) result.Add(Ll);
            return result;
        }
    }

    public Tile Ul
    {
        get
        {
            if (Y % 2 != 0)
            {
                if (TileMap.GetLength(1) > Y + 1) return TileMap[X, Y + 1];
            }
            else
            {
                if (X > 0 && TileMap.GetLength(1) > Y + 1) return TileMap[X - 1, Y + 1];
            }
            return null;
        }
    }

    public Tile Ur
    {
        get
        {
            if (Y % 2 != 0)
            {
                if (TileMap.GetLength(1) > Y + 1 && TileMap.GetLength(0) > X + 1) return TileMap[X + 1, Y + 1];
            }
            else
            {
                if (X > 0 && TileMap.GetLength(1) > Y + 1) return TileMap[X, Y + 1];
            }
            return null;
        }
    }

    public Tile Rr
    {
        get
        {
            if (TileMap.GetLength(0) > X + 1) return TileMap[X + 1, Y];
            return null;
        }
    }

    public Tile Dr
    {
        get
        {
            if (Y % 2 != 0)
            {
                if (Y > 0 && TileMap.GetLength(0) > X + 1) return TileMap[X + 1, Y - 1];
            }
            else
            {
                if (Y > 0) return TileMap[X, Y - 1];
            }
            return null;
        }
    }

    public Tile Dl
    {
        get
        {
            if (Y % 2 != 0)
            {
                if (Y > 0) return TileMap[X, Y - 1];
            }
            else
            {
                if (Y > 0 && X > 0) return TileMap[X - 1, Y - 1];
            }
            return null;
        }
    }

    public Tile Ll
    {
        get
        {
            if (X > 0) return TileMap[X - 1, Y];
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
    public Color Color
    {
        get
        {
            return gameObject.GetComponent<SpriteRenderer>().color;
        }
        set
        {
            gameObject.GetComponent<SpriteRenderer>().color = value;
        }
    }

    [NonSerialized]
    public Unit Unit;
    public int Height, X, Y;
    public bool Passable, Blocking;
    public Terrain Terrain;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ClickManager.Active.alreadySelecting)
        {
            if (ClickManager.EligibleToCast(this))
            {
                ClickManager.SelectedSpell.Cast(this);
                ClickManager.Clear();
            }
            else if (Unit != null) Unit.OnPointerClick(eventData);
            else if (ClickManager.EligibleToMoveTo(this))
            {
                // Moving this server-side, experimentally!
                // ClickManager.SelectedUnit.Move(ClickManager.SelectedUnit, this);
                WebClient.Active.Send(new MoveMessage(ClickManager.SelectedUnit, this));
                ClickManager.Clear();
            }
            else
            {
                ClickManager.Clear();
                ClickManager.Active.SetSelectedGameObject(gameObject);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        ClickManager.SelectedTile = this;
    }

    public void OnDeselect(BaseEventData eventData)
    {

    }
}

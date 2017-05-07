using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public List<Tile> Neighbours;
    public Tile UU, UL, DL, DD, DR, UR;

    public Sprite Sprite
    {
        get
        {
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        set { gameObject.GetComponent<SpriteRenderer>().sprite = value; }
    }

    public Unit Unit;
    public int Height;
    public bool Passable, Blocking;
    public Terrain Terrain;
    public delegate void OnTurn(Tile self);
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

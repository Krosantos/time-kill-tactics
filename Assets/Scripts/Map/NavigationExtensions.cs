using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class NavigationExtensions {

    //Awwwww yiss, A* up in this bitch.
    public static List<Tile> AStarPath(this Tile from, Tile to, Unit unit)
    {
        var openList = new List<Tile>();
        var closedList = new List<Tile>();
        var parentDict = new Dictionary<Tile, Tile>();
        var fScore = new Dictionary<Tile, int>();
        var gScore = new Dictionary<Tile, int>();
        var currentTile = from;

        gScore.Add(currentTile, 0);
        fScore.Add(currentTile, GetHeuristic(from, to));
        openList.Add(currentTile);
        while (openList.Count > 0)
        {
            currentTile = openList.OrderBy(x => fScore[x]).First();
            if (currentTile == to)
            {
                return CreatePath(from, to, parentDict);
            }
            openList.Remove(currentTile);
            closedList.Add(currentTile);
            foreach (var tile in currentTile.Neighbours)
            {
                var tempG = gScore[currentTile];// + unit.MoveCost[tile.Terrain];
                if (!openList.Contains(tile) && !closedList.Contains(tile))
                {
                    openList.Add(tile);
                    parentDict.Add(tile, currentTile);
                    gScore.Add(tile, tempG);
                    fScore.Add(tile, gScore[tile] + GetHeuristic(tile, to));
                }
                if (tempG < gScore[tile])
                {
                    //This is a better way to get to this tile, replace the entries in our dictionaries.
                    if (parentDict.ContainsKey(tile)) parentDict.Remove(tile);
                    parentDict.Add(tile, currentTile);
                    if (gScore.ContainsKey(tile)) gScore.Remove(tile);
                    gScore.Add(tile, tempG);
                    if (fScore.ContainsKey(tile)) fScore.Remove(tile);
                    fScore.Add(tile, gScore[tile] + GetHeuristic(tile, to));
                }
                //999 is a ~MAGIC NUMBER~. It's terrain cost shorthand for completely impassable.
                if (tile.IsImpassable(unit))
                {
                    openList.Remove(tile);
                    closedList.Add(tile);
                }
            }
        }
        //If you made it this far, no route exists. Sorry, breh.
        return null;
    }

    //Helper function to calculate A* heuristic.
    private static int GetHeuristic(Tile from, Tile to)
    {
        var result = 0;

        var firstPos = Mathf.Abs(to.Y - from.Y) + 1;
        var secondPos = Mathf.Abs(to.X - from.X) + 1;
        var thirdPos = Mathf.Abs(to.X - to.Y) * -1 - (from.X - from.Y) * -1 + 1;

        //Because tileagons tesselate less smoothly than squares, we take the largest of 3 possible measures.
        if (firstPos >= result) result = firstPos;
        if (secondPos >= result) result = secondPos;
        if (thirdPos >= result) result = thirdPos;

        return result;
    }

    //Helper function to unwrap the list of parents we made in A*.
    private static List<Tile> CreatePath(Tile from, Tile to, Dictionary<Tile, Tile> parents)
    {
        var result = new List<Tile> { to };
        var currentTile = to;
        while (currentTile != from)
        {
            result.Add(parents[currentTile]);
            currentTile = parents[currentTile];
        }
        //We want this in order of to --> from, without the 'to' tile itself.
        result.Reverse();
        result.RemoveAt(0);
        return result;
    }

    //Helper function to see if a tile's impassable for any reason (be that terrain, or enemy occupation).
    private static bool IsImpassable(this Tile tile, Unit unit)
    {
        /*if (unit.MoveCost[tile.Terrain] >= 999) return true;
        if (tile.OccupyUnit != null)
        {
            if (tile.OccupyUnit.PlayerId != unit.PlayerId) return true;
        }*/
        return false;
    }

    public static List<Tile> GetMovableTiles(this Unit unit)
    {
        var tile = unit.Tile;
        var openList = new List<Tile>();
        var tilesToOpen = new List<Tile>();
        var closedList = new List<Tile>();
        var result = new List<Tile>();
        var costDictionary = new Dictionary<Tile, int>();
        openList.Add(tile);
        costDictionary.Add(tile, 0);
        while (openList.Count > 0)
        {
            //While there are still potential places to move, check 'em out.
            foreach (var openTile in openList)
            {
                foreach (var neighbour in openTile.Neighbours)
                {   //If it can be moved to, and isn't in the closed list, add it to the open list.
                    if (1 + costDictionary[openTile] <= unit.Speed && !closedList.Contains(neighbour) && !openList.Contains(neighbour) && !tilesToOpen.Contains(neighbour))
                    {
                        result.Add(neighbour);
                        tilesToOpen.Add(neighbour);
                        costDictionary.Add(neighbour, 1 + costDictionary[openTile]);
                    }

                    //If it's already on the list, check to see if we're coming from a more efficient path.
                    else if ((openList.Contains(neighbour) || tilesToOpen.Contains(neighbour)) &&
                             1 + costDictionary[openTile] < costDictionary[neighbour])
                    {
                        costDictionary[neighbour] = 1 + costDictionary[openTile];
                    }
                }
                //Since we've explored all of this tile's neighbours, add it to the closed list.
                closedList.Add(openTile);
            }
            //Make the necessary adjustments to the open list.
            openList.AddRange(tilesToOpen);
            foreach (var oldTile in closedList)
            {
                openList.Remove(oldTile);
            }
        }
        return result;
    }

}


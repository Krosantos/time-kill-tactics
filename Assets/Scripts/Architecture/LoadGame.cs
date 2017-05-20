using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour {

    public GameObject UnitPrefab, EnemyPrefab;
    public MapSerializer Serializer;
    public TextAsset MapJson, PlayerOne, PlayerTwo;


    public void Awake()
    {
        Serializer.DeserializeMap(MapJson.text);
        
    }
}

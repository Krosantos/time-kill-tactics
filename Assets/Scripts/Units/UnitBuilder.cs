﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Builds units from JSON
public class UnitBuilder : MonoBehaviour {

    public Unit TestUnit;
    public GameObject UnitPrefab;

    public string SerializeUnit(Unit unit){
        return JsonUtility.ToJson(unit);
    }

    public void Awake(){
        var input = SerializeUnit(TestUnit);
        ConstructUnit(input);
    }

    // Add in delegate building bit.
    public GameObject ConstructUnit(string serializedUnit){
        var result = Instantiate(UnitPrefab, new Vector3(), Quaternion.identity);
        var unit = result.GetComponent<Unit>();
        JsonUtility.FromJsonOverwrite(serializedUnit, unit);
        unit.Move+=Move.Standard; 
        return result;
    }

}

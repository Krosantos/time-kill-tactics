using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ArmyBuilderTile : MonoBehaviour, IPointerClickHandler
{

    public int X, Y;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicky Sticky");
    }
}

using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float DragSpeed, Sensitivity;
    public static float MaxX, MinX, MaxY, MinY;
    private Vector3 _dragOrigin, _mouseDifference;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition;
            _mouseDifference = _dragOrigin;
            return;
        }

        if (!Input.GetMouseButton(0)) return;


        var delta = Camera.main.ScreenToViewportPoint(Input.mousePosition - _mouseDifference);
        _mouseDifference = Input.mousePosition;

        if (delta.magnitude >= Sensitivity) { 
        
            var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            var move = new Vector3(Mathf.Clamp(pos.x * DragSpeed, MinX, MaxX), Mathf.Clamp(pos.y * DragSpeed, MinY, MaxY), 0f);
            transform.Translate(move, Space.World);
        } else {
            _dragOrigin = Input.mousePosition;
        }
    }
}

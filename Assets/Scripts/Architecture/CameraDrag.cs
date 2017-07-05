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
            var move = new Vector3(pos.x * DragSpeed, pos.y * DragSpeed, 0f);
            transform.Translate(move, Space.World);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX), Mathf.Clamp(transform.position.y, MinY, MaxY), transform.position.z);
        } else {
            _dragOrigin = Input.mousePosition;
        }
    }
}

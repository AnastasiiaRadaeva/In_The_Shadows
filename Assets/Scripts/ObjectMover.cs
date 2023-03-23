using UnityEngine;

// Controlling of the item moving.
public class ObjectMover : MonoBehaviour
{
    public bool leftRightRotate;
    public bool upDownRotate;
    public bool move;
    
    private Vector3 _mousePosition = Vector3.zero;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find(Settings.GameManagerObjName).GetComponent<GameManager>();
    }

    private void OnMouseDrag()
    {
        if (_gameManager.victory) return;
        var currentMousePosition = Input.mousePosition;
        
        if (leftRightRotate && Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftControl) &&
            !Input.GetKey(KeyCode.LeftShift))
        {
            if (_mousePosition.x < currentMousePosition.x)
                transform.RotateAround(transform.position, Vector3.up, -Settings.RotateAngle);
            else if (_mousePosition.x > currentMousePosition.x)
                transform.RotateAround(transform.position, Vector3.up, Settings.RotateAngle);
        }

        if (upDownRotate && Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl) &&
            !Input.GetKey(KeyCode.LeftShift))
        {
            if (_mousePosition.y < currentMousePosition.y)
                transform.RotateAround(transform.position, Vector3.right, -Settings.RotateAngle);
            else if (_mousePosition.y > currentMousePosition.y)
                transform.RotateAround(transform.position, Vector3.right, Settings.RotateAngle);
        }

        if (move && Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) &&
            !Input.GetKey(KeyCode.LeftControl))
        {
            if (_mousePosition.x < currentMousePosition.x)
                transform.Translate(Vector3.left * Settings.MovementStep, Space.World);
            else if (_mousePosition.x > currentMousePosition.x)
                transform.Translate(Vector3.right * Settings.MovementStep, Space.World);
        }

        _mousePosition = currentMousePosition;
    }
}
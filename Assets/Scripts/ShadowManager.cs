using UnityEngine;

// Detecting of the right item position.
public class ShadowManager : MonoBehaviour
{
    public Quaternion targetRotation;
    public Vector3 targetPosition;
    public bool victory = false;
    
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    [SerializeField] private float w;
    
    private ObjectMover _objMover;
    
    void Start()
    {
        _objMover = GetComponent<ObjectMover>();
        targetRotation = new Quaternion(x, y, z, w);
    }
    
    void Update()
    {
        if (Quaternion.Angle(transform.rotation, targetRotation) <= Settings.AngleTolerance && 
            ((_objMover.move && AbsVal(transform.position.x, targetPosition.x) <= Settings.DistanceTolerance) 
             || !_objMover.move))
        {
            victory = true;
        }
    }

    private float AbsVal(float a, float b)
    {
        return a - b < 0 ? (a - b) * -1 : a - b;
    }
}
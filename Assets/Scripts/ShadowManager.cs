using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    [SerializeField] private float w;
    
    public Quaternion targetRotation;
    public Vector3 targerPosition;
    public bool Victory = false;

    private ObjectMover _objMover;

    // Start is called before the first frame update
    void Start()
    {
        _objMover = gameObject.GetComponent<ObjectMover>();
        targetRotation = new Quaternion(x, y, z, w);
    }

    // Update is called once per frame
    void Update()
    {
        if (Quaternion.Angle(transform.rotation, targetRotation) <= Settings.AngleTolerance && 
            ((_objMover.move && AbsVal(transform.position.x, targerPosition.x) <= Settings.DistanceTolerance) || !_objMover.move))
        {
            Victory = true;
        }
    }

    private float AbsVal(float a, float b)
    {
        return a - b < 0 ? (a - b) * -1 : a - b;
    }
}

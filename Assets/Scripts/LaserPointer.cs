using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.SetPosition(0, transform.position);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; 

        line.SetPosition(1, mousePos);
    }
}
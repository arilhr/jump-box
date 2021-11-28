using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject arrowObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngle();
    }

    private void UpdateAngle()
    {
        var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = currentMousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        arrowObject.transform.rotation = angleAxis;
    }
}
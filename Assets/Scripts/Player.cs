using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject arrowObject;
    public float power;

    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAngle();
        InputPlayer();
    }

    private void InputPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void UpdateAngle()
    {
        var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = currentMousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        arrowObject.transform.rotation = angleAxis;
    }

    private void Jump()
    {
        Vector3 direction = Quaternion.AngleAxis(arrowObject.transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.right;

        playerRb.AddForce(direction * power);
    }

    private void ChargePower()
    {

    }
}

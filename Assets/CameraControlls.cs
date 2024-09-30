using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    [SerializeField]
    private float myMoveSpeed;
    [SerializeField]
    private float myZoomSpeed;
    private void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            //transform.position += Vector3.back * myZoomSpeed * Time.deltaTime;
            Camera.main.orthographicSize += myZoomSpeed;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            //transform.position -= Vector3.back * myZoomSpeed * Time.deltaTime;
            Camera.main.orthographicSize -= myZoomSpeed;
        }
        Mathf.Clamp(transform.position.x, -500, -1);

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * myMoveSpeed * Time.deltaTime;
    }

}

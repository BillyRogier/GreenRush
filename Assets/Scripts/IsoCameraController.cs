using UnityEngine;

public class IsoCameraController : MonoBehaviour
{
    public float panSpeed = 20f; // Vitesse de déplacement
    public float zoomSpeed = 2f; // Vitesse de zoom
    public float minZoom = 5f;
    public float maxZoom = 40f;

    private Vector3 dragOrigin;

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    void HandleMovement()
{
    Vector3 move = Vector3.zero;

    if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        move += new Vector3(0, 0, 1);
    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        move += new Vector3(0, 0, -1);
    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        move += new Vector3(-1, 0, 0);
    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        move += new Vector3(1, 0, 0);

    transform.Translate(move * panSpeed * Time.deltaTime, Space.Self);

    if (Input.GetMouseButtonDown(1)) dragOrigin = Input.mousePosition;

    if (Input.GetMouseButton(1))
    {
        Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 moveMouse = new Vector3(difference.x * -panSpeed, 0, difference.y * -panSpeed);
        transform.Translate(moveMouse, Space.Self);
    }
}


    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * zoomSpeed * 100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);
        transform.position = pos;
    }
}

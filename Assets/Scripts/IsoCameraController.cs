using UnityEngine;

public class IsoCameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 40f;
    public float borderThickness = 10f;

    // Limites de déplacement
    public float minX = -15f;
    public float maxX = 15f;
    public float minZ = -10f;
    public float maxZ = 25f;

    private Vector3 dragOrigin;

    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleMouseEdgePan();
        ClampCameraPosition();
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

        Vector3 moveWorld = transform.TransformDirection(move);
        moveWorld.y = 0;

        transform.position += moveWorld * panSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
            dragOrigin = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 moveMouse = new Vector3(-difference.x, 0, -difference.y);
            Vector3 moveMouseWorld = transform.TransformDirection(moveMouse);
            moveMouseWorld.y = 0;

            transform.position += moveMouseWorld * panSpeed;
            dragOrigin = Input.mousePosition;
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

    void HandleMouseEdgePan()
    {
        Vector3 move = Vector3.zero;

        if (Input.mousePosition.x >= Screen.width - borderThickness)
            move += new Vector3(1, 0, 0);
        if (Input.mousePosition.x <= borderThickness)
            move += new Vector3(-1, 0, 0);
        if (Input.mousePosition.y >= Screen.height - borderThickness)
            move += new Vector3(0, 0, 1);
        if (Input.mousePosition.y <= borderThickness)
            move += new Vector3(0, 0, -1);

        Vector3 moveWorld = transform.TransformDirection(move);
        moveWorld.y = 0;

        transform.position += moveWorld * panSpeed * Time.deltaTime;
    }

    void ClampCameraPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}

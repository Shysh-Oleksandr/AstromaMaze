using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MouseLook : MonoBehaviour
{
    [SerializeField] [Range(10f, 300f)] private float mouseSensitivity = 100f;
    private Touch initTouch = new Touch();

    float xRotation = 0f, rotX, rotY;
    private Vector3 origRot;

    public float rotSpeed = 0.5f, direction = -1; // direction?

    public Transform playerBody;

    private void Start()
    {
        origRot = transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }

    /*void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 65f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }*/

    private void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = initTouch.position.x - touch.position.x;
                float deltaY = initTouch.position.y - touch.position.y;

                rotX -= deltaY * Time.deltaTime * rotSpeed * direction;
                rotY -= deltaX * Time.deltaTime * rotSpeed * direction;
                rotX = Mathf.Clamp(rotX, -90f, 65f);

                transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                playerBody.Rotate(Vector3.up * rotX);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
            }
        }
    }
}

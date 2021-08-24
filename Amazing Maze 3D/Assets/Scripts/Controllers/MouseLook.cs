using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] [Range(10f, 300f)] private float mouseSensitivity = 100f;

    float xRotation = 0f;

    public Transform playerBody;

    private void Awake()
    {
        GameManager.OnGameStateChanged += ChangeCursor;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= ChangeCursor;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ChangeCursor(GameState state)
    {
        if (state == GameState.Playing || state == GameState.LevelSelection)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

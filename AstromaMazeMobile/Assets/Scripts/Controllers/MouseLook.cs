using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MouseLook : MonoBehaviour
{
    // References
    [SerializeField] private Transform playerTransform;
    // Player settings

    [Range(1, 15)] [SerializeField] private float cameraSensitivity;

    // Touch detection
    private int rightFingerId;
    private float touchableScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;


    private void Start()
    {
        // id = -1 means the finger is not being tracked
        cameraSensitivity = GameManager.Instance.cameraSensitivity;
        rightFingerId = -1;

        // only calculate once
        touchableScreenWidth = Screen.width / 4;
    }

    // Update is called once per frame
    private void Update()
    {
        // Handles input
        GetTouchInput();


        if (rightFingerId != -1)
        {
            // look around if the right finger is being tracked
            LookAround();
        }
    }

    private void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x > touchableScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    private void LookAround()
    {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 65f);
        transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        playerTransform.Rotate(playerTransform.up, lookInput.x);
    }
}

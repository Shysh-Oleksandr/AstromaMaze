using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float groundDistance = 0.4f;
    private float gravity = -9.81f;
    private bool isGrounded;

    public Camera birdsEyeCam;
    public Text winnerText;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Vector3 velocity;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        PlayerMovement();

    }

    private void PlayerMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishPoint"))
        {
            Win();
        }

        if (other.CompareTag("SurprisedWall"))
        {
            Debug.Log("Surprise!");
        }
    }

    private void Win()
    {
        GameManager.isGameRunning = false;

        Debug.Log("Win!");
        winnerText.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }
}

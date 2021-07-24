using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float groundDistance = 0.4f;
    private float gravity = -9.81f;
    private bool isGrounded, isMoving;
    private Vector3 lastPosition;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public BootItem bootItem;

    public delegate void OnCoinTake(int coins);
    public event OnCoinTake OnCoinTakeEvent;
    
    public delegate void OnMovementChanged();
    public event OnMovementChanged OnMovementChangedEvent;

    private Vector3 velocity;
    private CharacterController controller;
    private AudioSource footstepSource;

    void Start()
    {
        foreach (Sound s in AudioManager.Instance.sounds)
        {
            if (s.name == "Footstep")
            {
                footstepSource = s.source;
            }
        }

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

        if(lastPosition != gameObject.transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if(isMoving)
        {
            if (!footstepSource.isPlaying)
            {
                AudioManager.Instance.Play("Footstep");
            }
        }
        else
        {
            footstepSource.Stop();
        }

        lastPosition = gameObject.transform.position;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * bootItem.speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        OnMovementChangedEvent?.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishPoint"))
        {
            GameManager.Instance.UpdateGameState("Victory");
        }

        if (other.CompareTag("SurprisedWall"))
        {
            Tweening.Instance.TweenVertically(UIManager.Instance.surpiseText.gameObject, 0.6f, 0, true, false);
        }

        if (other.GetComponent<LevelIndex>() != null)
        {
            GameManager.Instance.UpdateGameState("Playing");
            int levelIndex = other.GetComponent<LevelIndex>().levelIndex;
            SceneChanger.Instance.FadeToLevel(levelIndex);
        }

        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            AudioManager.Instance.Play("Coin");

            OnCoinTakeEvent?.Invoke(other.GetComponent<Coin>().coinValue);
        }
    }

}

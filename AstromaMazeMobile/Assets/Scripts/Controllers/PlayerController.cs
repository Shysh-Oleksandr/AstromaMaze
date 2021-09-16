using UnityEngine;
using EasyJoystick;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float groundDistance = 0.4f;
    private float gravity = -9.81f;
    private bool isGrounded, isMoving;
    private Vector3 lastPosition;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public BootItem bootItem;
    public CompassItem compassItem;
    public LevelManager levelManager;
    public Timer timer;
    public Joystick joystick;

    public bool isReplacing; // Whether the player position is replaced to checkpoint.

    public delegate void OnCoinTake(int coins);
    public event OnCoinTake OnCoinTakeEvent;
    
    public delegate void OnMovementChanged();
    public event OnMovementChanged OnMovementChangedEvent;

    public Checkpoint lastCheckpoint; 
    public Vector3 lastCheckpointPosition;
    private Vector3 velocity;
    private CharacterController controller;
    private AudioSource footstepSource;
    private Animator playerAnimator;
    private Compass compass;

    void Start()
    {
        lastCheckpointPosition = gameObject.transform.position;
        lastPosition = transform.position;
        playerAnimator = GetComponentInChildren<Animator>();
        if (GameManager.Instance.isBossLevel && compassItem.upgradingLevel == 4)
        {
            compass = FindObjectOfType<Compass>();
        }

        foreach (Sound s in AudioManager.Instance.sounds)
        {
            if (s.name == "Footstep")
            {
                footstepSource = s.source;
            }
        }

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isReplacing && !SceneChanger.Instance.isLoading)
        {
            PlayerMovement();
        }
    }

    private void PlayerMovement()
    {
        PlayFootstepSound();

        #region IsGrounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        #endregion

        #region Moving
        float x = joystick.Horizontal();
        float z = joystick.Vertical();

        if(x >= -0.2f && x <= 0.2f)
        {
            x = 0;
        }

        if(z >= -0.2f && z <= 0.2f)
        {
            z = 0;
        }


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * bootItem.speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        #endregion

        OnMovementChangedEvent?.Invoke();
    }

    private void PlayFootstepSound()
    {
        if (lastPosition != gameObject.transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            playerAnimator.SetFloat("Speed", 1);
        }
        else
        {
            playerAnimator.SetFloat("Speed", 0);
        }

        if (isMoving && (GameManager.Instance.State == GameState.Playing || GameManager.Instance.State == GameState.LevelSelection) && !SceneChanger.Instance.isLoading)
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

        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            AudioManager.Instance.Play("Coin");

            OnCoinTakeEvent?.Invoke(other.GetComponent<Coin>().coinValue);
        }

        if (other.GetComponent<LevelIndex>() != null)
        {
            if (!other.GetComponent<LevelIndex>().isLocked)
            {
                GameManager.Instance.UpdateGameState("Playing");
                int levelIndex = other.GetComponent<LevelIndex>().levelIndex;
                SceneChanger.Instance.FadeToLevel(levelIndex);
            }
            else
            {
                Tweening.Instance.TweenVertically(levelManager.lockLevelText.gameObject, 1.2f, 0, true, false);
            }
        }

        if (other.CompareTag("Checkpoint") && !other.GetComponent<Checkpoint>().isChecked)
        {
            AudioManager.Instance.Play("Checkpoint");
            lastCheckpoint = other.GetComponent<Checkpoint>();
            lastCheckpointPosition = lastCheckpoint.transform.position;
            lastCheckpoint.isChecked = true;
            if(compassItem.upgradingLevel == 4)
            {
                compass.currentExitTransform++;
            }
            timer.UpdateSubmazeTimer(lastCheckpoint.checkpointIndex);

            foreach (var checkpoint in UIManager.Instance.checkpointArrows)
            {
                if(checkpoint.checkpoint == lastCheckpoint.gameObject)
                {
                    MeshRenderer checkpointMesh = lastCheckpoint.gameObject.GetComponentInChildren<MeshRenderer>();
                    checkpointMesh.material.color = UIManager.Instance.passedCheckpointColor;
                    foreach (var arrow in checkpoint.arrows)
                    {
                        Renderer rend = arrow.GetComponent<Renderer>();
                        rend.material.color = UIManager.Instance.passedCheckpointColor;
                    }
                }
            }
        }

    }
}

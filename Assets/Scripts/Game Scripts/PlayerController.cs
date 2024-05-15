using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public Text coinCountText;
    public AudioSource coinSound;
    public AudioSource hitSound;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private bool isDashing = false;
    private int coinCount = 0;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();

        // Assign the current instance to the static property
        Instance = this;

        // Load player state if exists
        LoadPlayerState();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x != 0)
        {
            mySpriteRender.flipX = movement.x < 0;
        }
    }

    private void Dash()
{
    if (!isDashing)
    {
        isDashing = true;
        moveSpeed *= dashSpeed;
        
        // Check if myTrailRenderer is not null before accessing it
        if (myTrailRenderer != null)
        {
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
        else
        {
            // Reset isDashing flag if trail renderer is null
            isDashing = false;
        }
    }
}


    public void IncreaseCoinCount()
    {
        coinCount++;
        UpdateCoinCountDisplay();
        PlayCoinSound();
    }

    public void DecreaseCoinCount(int amount)
    {
        coinCount = Mathf.Max(0, coinCount - amount);
        PlayHitSound();
        UpdateCoinCountDisplay();
    }

    private void UpdateCoinCountDisplay()
    {
        if (coinCountText != null)
        {
            coinCountText.text = coinCount.ToString();
        }
    }

    private void PlayCoinSound()
    {
        coinSound?.Play();
    }

    private void PlayHitSound()
    {
        hitSound?.Play();
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void SetCoinCount(int amount)
    {
        coinCount = amount;
        UpdateCoinCountDisplay();
    }

    public void SetPlayerPosition(Vector3 position)
    {
        transform.position = position;
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    private void LoadPlayerState()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Vector3 playerPosition = new Vector3(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY"),
                PlayerPrefs.GetFloat("PlayerZ")
            );
            SetPlayerPosition(playerPosition);
        }

        if (PlayerPrefs.HasKey("Score"))
        {
            SetCoinCount(PlayerPrefs.GetInt("Score"));
        }
    }
}

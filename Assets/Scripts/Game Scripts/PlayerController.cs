using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; } // Singleton instance of PlayerController

    public Text coinCountText; // UI text to display coin count
    public AudioSource coinSound; // Sound to play when collecting a coin
    public AudioSource hitSound; // Sound to play when getting hit

    [SerializeField] private float moveSpeed = 1f; // Movement speed of the player
    [SerializeField] private float dashSpeed = 4f; // Dash speed multiplier
    [SerializeField] private TrailRenderer myTrailRenderer; // Trail effect for dashing

    private PlayerControls playerControls; // Input controls for the player
    private Vector2 movement; // Player movement vector
    private Rigidbody2D rb; // Rigidbody2D component for physics
    private Animator myAnimator; // Animator component for animations
    private SpriteRenderer mySpriteRender; // SpriteRenderer component for visual representation

    private bool isDashing = false; // Indicates if the player is currently dashing

    private int quizMarks; // Player's quiz marks

    private void Awake()
    {
        playerControls = new PlayerControls(); // Initialize player controls
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        myAnimator = GetComponent<Animator>(); // Get Animator component
        mySpriteRender = GetComponent<SpriteRenderer>(); // Get SpriteRenderer component

        Instance = this; // Set the singleton instance
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash(); // Assign dash action to Dash method
        SaveManager.Instance.LoadGame(); // Load saved game state

        StartCoroutine(InitializeQuizMarks()); // Initialize quiz marks
    }

    private void OnEnable()
    {
        playerControls.Enable(); // Enable player controls
    }

    private void Update()
    {
        PlayerInput(); // Handle player input
        UpdateCoinCountDisplay(); // Update the coin count display
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection(); // Adjust the player's facing direction
        Move(); // Move the player
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); // Get movement input
        myAnimator.SetFloat("moveX", movement.x); // Update animator with X movement
        myAnimator.SetFloat("moveY", movement.y); // Update animator with Y movement
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime)); // Move player based on input
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x != 0)
        {
            mySpriteRender.flipX = movement.x < 0; // Flip the sprite based on movement direction
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true; // Set dashing state to true
            moveSpeed *= dashSpeed; // Increase movement speed

            if (myTrailRenderer != null)
            {
                myTrailRenderer.emitting = true; // Enable trail effect
                StartCoroutine(EndDashRoutine()); // Start coroutine to end dash
            }
            else
            {
                isDashing = false; // Reset dashing state
            }
        }
    }

    public void IncreaseCoinCount()
    {
        CoinManager.IncreaseCoins(1); // Increase coin count
        UpdateCoinCountDisplay(); // Update coin count display
        PlayCoinSound(); // Play coin sound
    }

    public void DecreaseCoinCount(int amount)
    {
        CoinManager.DecreaseCoins(amount); // Decrease coin count
        PlayHitSound(); // Play hit sound
        UpdateCoinCountDisplay(); // Update coin count display
    }

    private void UpdateCoinCountDisplay()
    {
        if (coinCountText != null)
        {
            coinCountText.text = CoinManager.currentCoins.ToString(); // Update the UI text with the current coin count
        }
    }

    private void PlayCoinSound()
    {
        coinSound?.Play(); // Play the coin sound if it is set
    }

    private void PlayHitSound()
    {
        hitSound?.Play(); // Play the hit sound if it is set
    }

    public int GetQuizMarks()
    {
        return quizMarks; // Return the player's quiz marks
    }

    public void SetPlayerPosition(Vector3 position)
    {
        transform.position = position; // Set the player's position
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f; // Duration of the dash
        float dashCD = 0.25f; // Cooldown time after the dash
        yield return new WaitForSeconds(dashTime); // Wait for dash duration
        moveSpeed /= dashSpeed; // Reset movement speed
        myTrailRenderer.emitting = false; // Disable trail effect
        yield return new WaitForSeconds(dashCD); // Wait for cooldown duration
        isDashing = false; // Reset dashing state
    }

    private IEnumerator InitializeQuizMarks()
    {
        if (PlayerPrefs.HasKey("QuizMarks"))
        {
            quizMarks = PlayerPrefs.GetInt("QuizMarks"); // Load quiz marks from PlayerPrefs
            yield break;
        }
        else
        {
            string userId = GetMethod.userID; // Replace with actual user ID retrieval
            string jwtToken = GetMethod.jwtToken2; // Replace with actual JWT token retrieval
            string url = $"http://localhost:8080/energy-quest/user/score/{userId}";

            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Authorization", $"Bearer {jwtToken}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (int.TryParse(request.downloadHandler.text, out int fetchedQuizMarks))
                {
                    quizMarks = fetchedQuizMarks; // Set quiz marks from the server response
                    PlayerPrefs.SetInt("QuizMarks", quizMarks); // Save quiz marks in PlayerPrefs
                }
                else
                {
                    Debug.LogError("Failed to parse quiz marks from server response.");
                }
            }
            else
            {
                Debug.LogError($"Failed to fetch quiz marks: {request.error}");
            }
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    private int quizMarks;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();

        Instance = this;
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        SaveManager.Instance.LoadGame();

        StartCoroutine(InitializeQuizMarks());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        UpdateCoinCountDisplay();
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

            if (myTrailRenderer != null)
            {
                myTrailRenderer.emitting = true;
                StartCoroutine(EndDashRoutine());
            }
            else
            {
                isDashing = false;
            }
        }
    }

    public void IncreaseCoinCount()
    {
        CoinManager.IncreaseCoins(1);
        UpdateCoinCountDisplay();
        PlayCoinSound();
    }

    public void DecreaseCoinCount(int amount)
    {
        CoinManager.DecreaseCoins(amount);
        PlayHitSound();
        UpdateCoinCountDisplay();
    }

    private void UpdateCoinCountDisplay()
    {
        if (coinCountText != null)
        {
            coinCountText.text = CoinManager.currentCoins.ToString();
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

    public int GetQuizMarks()
    {
        return quizMarks;
    }

    public void SetQuizMarks(int marks)
    {
        quizMarks = marks;
        PlayerPrefs.SetInt("QuizMarks", marks);
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

    private IEnumerator InitializeQuizMarks()
    {
        if (PlayerPrefs.HasKey("QuizMarks"))
        {
            quizMarks = PlayerPrefs.GetInt("QuizMarks");
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
                    quizMarks = fetchedQuizMarks;
                    PlayerPrefs.SetInt("QuizMarks", quizMarks);
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

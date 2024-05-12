using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    // Static reference to the player instance
    public static PlayerController Instance { get; private set; }

    // Reference to the UI Text element
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

        UpdateCoinCountDisplay();
        // Assign the current instance to the static property
        Instance = this;
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
        // If there is movement in the horizontal direction
        if (movement.x != 0)
        {
            // If moving left, flip the sprite
            if (movement.x < 0)
            {
                mySpriteRender.flipX = true;
            }
            // If moving right, keep the sprite facing right
            else
            {
                mySpriteRender.flipX = false;
            }
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
            }
            else
            {
                Debug.LogWarning("TrailRenderer is null. Cannot emit trail.");
            }
            StartCoroutine(EndDashRoutine());
        }
    }

    public void IncreaseCoinCount()
    {
        coinCount++;
        // Update UI or perform any other actions related to collecting coins
        UpdateCoinCountDisplay();
        PlayCoinSound();
        
    }

    public void DecreaseCoinCount(int amount)
    {
        coinCount -= amount;
        if (coinCount < 0)
        {
            coinCount = 0;
        }
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
        if (coinSound != null && coinSound.clip != null)
        {
            coinSound.Play(); 
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && hitSound.clip != null)
        {
            hitSound.Play();
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void SetCoinCount(int amount)
    {
        coinCount = amount;
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed; ;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

}

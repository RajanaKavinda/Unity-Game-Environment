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
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    public void IncreaseCoinCount()
    {
        coinCount++;
        // Update UI or perform any other actions related to collecting coins
        UpdateCoinCountDisplay();
    }

    public void DecreaseCoinCount(int amount)
    {
        coinCount -= amount;
        if (coinCount < 0)
        {
            coinCount = 0;
        }
        UpdateCoinCountDisplay();
    }

    private void UpdateCoinCountDisplay()
    {
        if (coinCountText != null) // Check if UI Text reference is assigned
        {
            coinCountText.text = coinCount.ToString(); // Update text with coin count
        }
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

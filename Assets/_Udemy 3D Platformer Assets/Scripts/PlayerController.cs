using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public static PlayerController instance;

    //Health
    public int maxHealth = 5;

    //Movement related vairable
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 5f;
    public float bounceForce = 8f;

    //Character controls
    private Vector3 moveDirection;
    private float fallJumpCounter;
    public float fallDelay = 1f;
    public CharacterController charControl;
    
    //Camera
    public Camera playerCamera;
    public float rotateSpeed;

    //Animation
    public GameObject playerModel;
    public Animator playerAnimator;
    public GameObject playerExplosion;
    public GameObject healthParticles;

    //Post damage
    public bool isKnockedBack = false;
    public float knockbackLength = 0.5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;
    public GameObject[] playerPieces;

    //End level
    public bool isStopped = false;
    #endregion

    #region Awake
    private void Awake()
    {
        instance = this;
        playerAnimator.SetBool("IsInMainMenu", GameManager.instance.MainMenuChecker());
    }

    
    #endregion

    #region Start and Update
    void Start()
    {
    }

    void Update()
    {    
        if (isStopped)
        {
            StopPlayer();
        }
        else
        {
            if (!isKnockedBack)
            {
                CheckPlayerRun();
                CheckPlayerJump();
                PlayerMovementHandler(false);
                CheckCameraMovement();
            } else
            {
                KnockbackPlayer();
                PlayerMovementHandler(false);
            }
            CheckPlayerAnimation();
        }
    }
    #endregion

    #region Methods
    private void CheckPlayerRun()
    {
        float yStored = moveDirection.y;
        moveDirection = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = yStored;
    }

    private void CheckPlayerJump()
    {
        if (charControl.isGrounded || (!charControl.isGrounded && charControl.velocity.y < 0 && fallJumpCounter > 0))
        {
            fallJumpCounter = fallDelay;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                fallJumpCounter = 0;
            }
        } else if (!charControl.isGrounded)
        {
            fallJumpCounter -= Time.deltaTime;
        }
    }

    private void CheckCameraMovement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            //Third person shooter camera
            //transform.rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);

            //Platformer camera
            transform.rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));

            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }

    private void CheckPlayerAnimation()
    {
        if (GameManager.instance.MainMenuChecker()) playerAnimator.SetBool("IsInMainMenu", true);
        playerAnimator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        playerAnimator.SetBool("Grounded", charControl.isGrounded);
    }

    private void PlayerMovementHandler(bool IsStopped)
    {
        if (IsStopped) return;
        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        charControl.Move(moveDirection * Time.deltaTime);
    }
    
    private void KnockbackPlayer()
    {
        knockbackCounter -= Time.deltaTime;

        float yStore = moveDirection.y;
        moveDirection = playerModel.transform.forward * -knockbackPower.x;
        moveDirection.y = yStore;

        if (knockbackCounter <= 0) isKnockedBack = false;
    }

    private void StopPlayer()
    {
        moveDirection = Vector3.zero;
        charControl.Move(moveDirection);
        playerAnimator.SetFloat("Speed", 0);
    }

    public void SetKnockback(bool canKnockBack)
    {
        isKnockedBack = canKnockBack;
        knockbackCounter = knockbackLength;
        moveDirection.y = knockbackPower.y;
    }
    
    public void BouncePlayer(float bounceMultiplier)
    {
        moveDirection.y = bounceForce * bounceMultiplier;
        charControl.Move(moveDirection * Time.deltaTime);
    }

    public void PlayerFlicker(float countdown, float flickeringIntensity)
    {
        foreach (GameObject playerPiece in playerPieces)
        {
            if (Mathf.Floor(countdown * flickeringIntensity) % 2 == 0)
            {
                playerPiece.SetActive(true);
            }
            else
            {
                playerPiece.SetActive(false);
            }
            if (countdown <= 0) playerPiece.SetActive(true);
        }
    }
    
    public void PlayerExplosionAnimation()
    {
        GameObject explosion = Instantiate(playerExplosion, transform.position + new Vector3(0, 1f, 0), transform.rotation);
        Destroy(explosion, 2f);
    }

    public void PlayerHealingAnimation(bool isActive)
    {
        healthParticles.SetActive(isActive);
    }
    
    #endregion
}
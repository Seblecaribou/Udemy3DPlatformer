using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public static PlayerController instance;

    //Health
    public int maxHealth = 3;

    //Movement related vairable
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 5f;

    //Character controls
    private Vector3 moveDirection;
    public CharacterController charControl;
    
    //Camera
    public Camera playerCamera;
    public float rotateSpeed;

    //Animation
    public GameObject playerModel;
    public Animator playerAnimator;

    //Post damage
    public bool isKnockedBack = false;
    public float knockbackLength = 0.5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;
    public GameObject[] playerPieces;

    #endregion

    #region Awake
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Start and Update
    void Start()
    {

    }

    void Update()
    {
        if (!isKnockedBack)
        {
            CheckPlayerRun();
            CheckPlayerJump();
            PlayerMovementHandler();
            CheckCameraMovement();
        } else
        {
            KnockbackPlayer();
            PlayerMovementHandler();
        }
        CheckPlayerAnimation();
    }
    #endregion

    #region Methods
    private void CheckPlayerRun()
    {
        float yStored = moveDirection.y;
        moveDirection = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        //solves the speed*2 problem when pressing diagonals
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = yStored;
    }

    private void CheckPlayerJump()
    {
        if (charControl.isGrounded)
        {
            moveDirection.y = 0;
            if (Input.GetButtonDown("Jump")) moveDirection.y = jumpForce;
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
        playerAnimator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        playerAnimator.SetBool("Grounded", charControl.isGrounded);
    }

    private void PlayerMovementHandler()
    {
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

    public void SetKnockback(bool canKnockBack)
    {
        isKnockedBack = canKnockBack;
        knockbackCounter = knockbackLength;
        moveDirection.y = knockbackPower.y;
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
    #endregion
}
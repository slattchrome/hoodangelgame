using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField]
    private float speed = 12;
    [SerializeField]
    private float rotationSpeed = 720;
    [SerializeField]
    private float jumpSpeed = 7;
    [SerializeField]
    private float jumpHorizontalSpeed = 3;
    [SerializeField]
    private float jumpButtonGracePeriod = 0.2f;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private AudioClip playerDeathSound;
    [SerializeField]
    private AudioClip jumpingSound;
    [SerializeField]
    private AudioClip landingSound;
    [SerializeField]    
    private AudioClip runningSound;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private Slider healthBar;


    private CharacterController characterController;
    private Rigidbody rb;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private Animator animator;
    private bool isJumping;
    private bool isGrounded;
    private Vector3 spawnPoint;
    private float trapTimer = 0;
    private float isDeadTimer = 0;
    private bool trapEntered = false;
    private AudioSource audioSource;
    private bool cursorVisible = true;
    private bool isAlive = true;
    private int deathsCount;

    public bool aim;
    public bool shoot;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        healthBar.maxValue = maxHealth;

        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        animator = GetComponent<Animator>();
        spawnPoint = gameObject.transform.position;
        rb = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();

       
    }

    void Update()
    {
         
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitube = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        if (isAlive)
        {    ySpeed += Physics.gravity.y * Time.deltaTime;

            if (characterController.isGrounded)
            {
                lastGroundedTime = Time.time;
            }

            if (Input.GetButtonDown("Jump"))
            {
                SoundFXManager.instance.PlaySoundFXClip(jumpingSound, transform, 1f);
                jumpButtonPressedTime = Time.time;
            }

            if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
            {
                characterController.stepOffset = originalStepOffset;
                ySpeed = -0.5f;
                animator.SetBool("IsGrounded", true);
                isGrounded = true;
                animator.SetBool("IsJumping", false);
                isJumping = false;
                animator.SetBool("IsFalling", false);

                if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
                {
                    ySpeed = jumpSpeed;
                    animator.SetBool("IsJumping", true);
                    isJumping = true;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
            }
            else
            {
                characterController.stepOffset = 0;
                animator.SetBool("IsGrounded", false);
                isGrounded = false;

                if ((isJumping && ySpeed < 0) || ySpeed < -2)
                {
                    audioSource.Stop();
                    animator.SetBool("IsFalling", true);
                }
            }

            Vector3 velocity = movementDirection * magnitube;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                if (!audioSource.isPlaying) 
                {
                    audioSource.clip = runningSound;
                    audioSource.volume = 1f;
                    audioSource.Play();
                }
                animator.SetBool("IsMoving", true);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                audioSource.Stop();
                animator.SetBool("IsMoving", false);
            }

            if (isGrounded == false)
            {
                velocity *= jumpHorizontalSpeed;
                velocity.y = ySpeed;

                characterController.Move(velocity * Time.deltaTime);
            }
        }

        if (trapEntered)
        {
            speed = 0;
            trapTimer += Time.deltaTime;
            if (trapTimer > 0.5f)
            {
                //characterController.enabled = false;
                //transform.position = spawnPoint;
                //characterController.enabled = true;
                trapEntered = false;
                trapTimer = 0;
                speed = 12;
            }
        }

        if (currentHealth < 1)
        {
            animator.SetBool("isDead", true);
            isAlive = false;
            audioSource.Stop();
            isDeadTimer += Time.deltaTime;
            if (isDeadTimer > 4f)
            {
                deathsCount++;
                characterController.enabled = false;
                transform.position = spawnPoint;
                characterController.enabled = true;
                currentHealth = maxHealth;
                healthBar.value = currentHealth;
                isDeadTimer = 0;
                animator.SetBool("isDead", false);
                isAlive = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            cursorVisible = !cursorVisible;

            Cursor.visible = cursorVisible;

            Cursor.lockState = cursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }


    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SoundFXManager.instance.PlaySoundFXClip(playerDeathSound, transform, 1f);
            SendDamage(50);
            //characterController.enabled = false;
            //transform.position = spawnPoint;
            //characterController.enabled = true;
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = transform.position;
            spawnPoint.y = 0;
        }

        if (other.gameObject.CompareTag("Trap") )
        {
            SoundFXManager.instance.PlaySoundFXClip(playerDeathSound, transform, 1f);
            SendDamage(50);
            trapEntered = true;
        }
    }

    private void SendDamage(float damageValue)
    {
        currentHealth -= damageValue;
        healthBar.value = currentHealth;
    }

    public void OnAim(InputValue value)
    {
        AimInput(value.isPressed);
    }

    public void OnShoot(InputValue value)
    {
        ShootInput(value.isPressed);    
    }

    public void AimInput(bool newAimState)
    {
        aim = newAimState;
    }

    public void ShootInput(bool newShootState)
    {
        shoot = newShootState;
    }

    public void SetDeathsCount(int deathsCount)
    {
        this.deathsCount = deathsCount;
    }

    public int GetDeathsCount()
    {
        return deathsCount;
    }
}

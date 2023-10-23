using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    //Fly
    [SerializeField] private float speed, levitationSpeed;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public AudioClip walkingSound; // Assign the walking sound effect in the Inspector.
    public AudioClip runningSound; // Assign the running sound effect in the Inspector.
    private AudioSource audioSource;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    bool isWalking = false;
    bool isRunning = false;

    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Check if the player is walking (moving)
        isWalking = curSpeedX != 0 || curSpeedY != 0;

      

        // Play walking or running sounds
        if (isWalking && !isRunning)
        {
            audioSource.clip = walkingSound;
        }
        else if (isRunning)
        {
            audioSource.clip = runningSound;
        }
        else
        {
            // No movement, stop the audio
            audioSource.clip = null;
        }

        if (audioSource.clip != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If there's no audio clip assigned, stop the audio
            audioSource.Stop();
        }
    }

    private void Fly()
    {
        moveDirection = Vector3.up * levitationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Tab))
        {
            characterController.Move(moveDirection);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.Move(-moveDirection);
        }
    }
}

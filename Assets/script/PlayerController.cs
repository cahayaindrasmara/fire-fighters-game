using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform camera;

    [Header("Movement Settings")]
    [SerializeField] private float walkspeed = 5f;
    [SerializeField] private float turningspeed = 2f;
    [SerializeField] private float gravity = 9f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private GameObject apar;
    [SerializeField] private AudioSource sprayAudioSource;
    [SerializeField] private AudioClip spraySound;
    [SerializeField] private AirBarController airBarController;
    [SerializeField] private ParticleSystem airParticle;
    [SerializeField] private Collider sprayCollider;

    [SerializeField] private Animator animator;

    private float verticalVelocity;

    [Header("Input")]
    private float moveInput;
    private float turnInput;
    private bool jumpInput;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        InputManagement();
        Movement();
        UpdateAnimator();
    }

    private void InputManagement() {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
    }

    private void Movement() {
        GroundMovement();
        Turn();
    }

    private void GroundMovement() {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = transform.TransformDirection(move);
        move.y = VerticalForceCalculate();
        move *= walkspeed;
        controller.Move(move* Time.deltaTime);
    }

    private void Turn() {
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0) {
            //vector3 currentLookDirection = camera.forward;
            Vector3 currentLookDirection = controller.velocity.normalized;
            currentLookDirection.y = 0;

            //ditambah jika ingin bagian depan karakter bisa terlihat dengan putaran kamera
            currentLookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningspeed);
        }
    }

    private float VerticalForceCalculate()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            // 🆕 Jika menekan tombol lompat saat menyentuh tanah
            if (jumpInput)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        return verticalVelocity;
    }

    private void UpdateAnimator(){
    // Nilai magnitude = seberapa cepat karakter bergerak
        float speed = new Vector2(turnInput, moveInput).magnitude;
        animator.SetFloat("speed", speed);

        // Trigger animasi shoot jika tekan tombol Space
        // if (Input.GetKeyDown(KeyCode.F)) // F untuk shoot
        // {
        //     // animator.SetTrigger("shoot");
        // }

         // Gunakan GetKey agar selama F ditekan, animasi tetap aktif
        bool isShooting = Input.GetKey(KeyCode.F);
        animator.SetBool("newShoot", isShooting);
        // Debug.Log("Shoot: " + isShooting);

        if (apar != null)
        {
            apar.SetActive(isShooting);
            Debug.Log("aktif");
        }

        // Kontrol partikel air (semprotan)
        if (airParticle != null)
        {
            if (isShooting && !airBarController.IsOutOfWater)
            {
                if (!airParticle.isPlaying) airParticle.Play();
            }
            else
            {
                if (airParticle.isPlaying) airParticle.Stop();
            }
        }

        if (sprayCollider != null)
        {
            sprayCollider.enabled = isShooting && !airBarController.IsOutOfWater;
        }

        if (sprayAudioSource != null && spraySound != null)
        {
            if (isShooting && !sprayAudioSource.isPlaying && !airBarController.IsOutOfWater)
            {
                sprayAudioSource.clip = spraySound;
                sprayAudioSource.loop = true;
                sprayAudioSource.Play();
            }
            else if (!isShooting && sprayAudioSource.isPlaying && !airBarController.IsOutOfWater)
            {
                sprayAudioSource.Stop();
            }
        }
                
    }

}

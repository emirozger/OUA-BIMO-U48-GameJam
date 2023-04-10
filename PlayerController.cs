using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;



public class PlayerController : MonoBehaviour
{
    public Animator dialogueAnimator;
    private Rigidbody rigidbody;
    private float moveX, moveZ;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canDoubleJump;
    public Camera mainCamera;
    public Camera UiCamera;
    [SerializeField] private float hitDistance = 30f;
    public GameObject crosshair;
    public GameObject alarmObject;
    DialogueTrigger trigger;
    FormController formController;


    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody>();
        trigger = FindObjectOfType<DialogueTrigger>();
        formController=FindObjectOfType<FormController>();

    }

    void Update()
    {
        Inputs();
        Debug.Log(isGrounded);
        UpdateJump();

    }

    private void FixedUpdate()

    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
        PlayerMovement();
        FixedJump();

    }

    private void Inputs()
    {
        RaycastController();
        RobotChat();
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    private void PlayerMovement()
    {
        //if (!isGrounded) return;
        Vector3 moveVector = new Vector3(moveX * moveSpeed * Time.fixedDeltaTime, rigidbody.velocity.y, moveZ * moveSpeed * Time.fixedDeltaTime);
        //moveVector = transform.TransformDirection(moveVector);
        rigidbody.velocity = moveVector;

    }
    private void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
                canDoubleJump = true;
                isGrounded = false;
            }
            else if (canDoubleJump)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
                canDoubleJump = false;
            }
        }
    }
    private void FixedJump()
    {

        if (!isGrounded) return;

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
    }
    public void RaycastController()
    {      
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, hitDistance))
        {
            if (hit.collider.CompareTag("PC"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.gameObject.SetActive(false);
                    mainCamera.gameObject.SetActive(false);
                    UiCamera.gameObject.SetActive(true);
                    crosshair.SetActive(false);
                    formController.pcCanvas.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Debug.Log("Interacted with " + hit.collider.gameObject.name);
                    Debug.Log("Distance: " + hit.distance);
                }
            }
            if (hit.collider.CompareTag("alarm"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    alarmObject.GetComponent<AudioSource>().enabled = false;
                    alarmObject.GetComponent<Animator>().SetTrigger("closeAlarm");

                }
            }
            if (hit.collider.CompareTag("Kargo"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("evrene geciliyor."); //evrene geç.
                }
            }

        }

    }
    public void RobotChat()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 100))
        {
            if (hit.collider.CompareTag("Robot"))
            {
                Debug.Log("üstünde");
                dialogueAnimator.SetBool("IsOpen", true);
                trigger.TriggerDialogue();
                hit.collider.enabled = false;
            }
        }

    }
}


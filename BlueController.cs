using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BlueController : MonoBehaviour
{
    public bool isBlueButtonTaken, isGreenButtonTaken, isOrangeButtonTaken, isRedButtonTaken = false;
    public Animator dialogueAnimator;
    public Animator doorAnimator;
    public Camera wakeUpAnimCam;
    DialogueTrigger trigger;
    private Rigidbody rigidbody;
    private float moveX, moveZ;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canDoubleJump;
    public Camera mainCamera;
    public Camera UiCamera;
    public Camera finalCamera;
    [SerializeField] private float hitDistance = 3f;
    public Canvas pcCanvas;
    public GameObject crosshair;
    public bool doubleJumpPlus = false;

    //egilme
    public bool isCrouching = false; // Crouch durumu
    private CapsuleCollider playerCollider; // Karakterin Collider component'i
    private bool isMoving;
    public float yukseklik;
    public GameObject oynatilacakTas, oynatilacakTas1, oynatilacakTas2;
    public GameObject alarmObject;
    FormController formController;
    WordleController wordleController;
    LockController lockController;
    public Vector3 playerStartPosition;

    public bool isTakeGreenButton = false;
    public bool lastHaveBlueButton, lastHaveGreenButton, lastHaveRedButton, lastHaveOrangeButton;
    public GameObject maviTus, yesilTus, kirmizitus, turuncuTus;
    public GameObject sertifika;





    void Start()
    {

        this.transform.position = playerStartPosition;
        rigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        trigger = FindObjectOfType<DialogueTrigger>();
        formController = FindObjectOfType<FormController>();
        wordleController = FindObjectOfType<WordleController>();
        lockController = FindObjectOfType<LockController>();
        StartCoroutine(camAnim());

        bool haveBlueButton = PlayerPrefs.GetInt("HaveBlueBool") == 1 ? true : false;
        PlayerPrefs.Save();
        lastHaveBlueButton = haveBlueButton;

        bool haveGreenButton = PlayerPrefs.GetInt("HaveGreenBool") == 1 ? true : false;
        PlayerPrefs.Save();
        lastHaveGreenButton = haveGreenButton;

        bool haveOrangeButton = PlayerPrefs.GetInt("HaveOrangeBool") == 1 ? true : false;
        PlayerPrefs.Save();
        lastHaveOrangeButton = haveOrangeButton;

        bool haveRedButton = PlayerPrefs.GetInt("HaveRedBool") == 1 ? true : false;
        PlayerPrefs.Save();
        lastHaveRedButton = haveRedButton;

    }

    void Update()
    {
        Inputs();
        UpdateJump();
        Crouch();
        RaycastController();
        if (lastHaveBlueButton && lastHaveGreenButton && lastHaveOrangeButton && lastHaveRedButton)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, hitDistance))
            {
                if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Stand"))
                {
                    Debug.Log("animasyon");
                    StartCoroutine(finishAnimAndCertification());
                }
            }
            //tüm tuşlar alındı. en sonki kamera animasyonunu sok.,maincamera .getcom.camera=enable false yap. mouse look kapat.

        }
        if (lastHaveBlueButton)
        {
            maviTus.SetActive(true);
        }
        if (lastHaveGreenButton)
        {
            yesilTus.SetActive(true);
        }
        if (lastHaveOrangeButton)
        {
            turuncuTus.SetActive(true);
        }
        if (lastHaveRedButton)
        {
            kirmizitus.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        PlayerMovement();
        FixedJump();
        RobotChat();


    }
    private IEnumerator finishAnimAndCertification()
    {
        mainCamera.gameObject.GetComponent<Camera>().enabled = false;
        finalCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(12f);
        sertifika.gameObject.SetActive(true);
    }
    private IEnumerator camAnim()
    {
        yield return new WaitForSeconds(.001f);
        wakeUpAnimCam.gameObject.SetActive(true);
        mainCamera.GetComponent<Camera>().enabled = false;
        yield return new WaitForSeconds(9f);
        wakeUpAnimCam.gameObject.SetActive(false);
        mainCamera.GetComponent<Camera>().enabled = true;
        mainCamera.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    private void Inputs()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        isMoving = moveX != 0 || moveZ != 0;

    }

    private void PlayerMovement()
    {
        //if (!isGrounded) return;
        Vector3 moveVector = new Vector3(moveX * moveSpeed * Time.fixedDeltaTime, rigidbody.velocity.y, moveZ * moveSpeed * Time.fixedDeltaTime);
        moveVector = transform.TransformDirection(moveVector);
        rigidbody.velocity = moveVector;

    }
    private void UpdateJump()
    {
        if (isCrouching) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
                canDoubleJump = true;
                isGrounded = false;
            }
            else if (canDoubleJump && doubleJumpPlus)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
                canDoubleJump = false;
            }
        }
    }
    private void FixedJump()
    {
        if (!isGrounded) return;
        if (isCrouching) return;

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
    private void Crouch()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isCrouching)
            {
                transform.localScale = new Vector3(1, .5f, 0);
                isCrouching = true;
            }

            else
            {
                transform.localScale = new Vector3(1, 1f, 0);
                isCrouching = false;
            }
        }
    }

    private void RaycastController()

    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, hitDistance))
        {
            if (hit.collider.CompareTag("Balta"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Animator>().SetTrigger("BaltaVurus");
                    oynatilacakTas.transform.DOMove(new Vector3(-6.133893f, 41.39386f, 55.21f), 8);
                    oynatilacakTas1.transform.DOMove(new Vector3(-1.35209f, 43.57264f, 57.6f), 8);
                    oynatilacakTas2.transform.DOMove(new Vector3(3.119908f, 45.79121f, 58.27f), 8);
                }

            }
            if (hit.collider.CompareTag("MaviOdul"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isBlueButtonTaken = true;
                    PlayerPrefs.SetInt("HaveBlueBool", isBlueButtonTaken ? 1 : 0);
                    PlayerPrefs.Save();

                    StartCoroutine(fowIncrease(2));
                    Debug.Log("mavi ödül alındı. bölüm kazanıldı");
                }

            }
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
                    StartCoroutine(fowIncrease(2));
                    Debug.Log("evrene geciliyor."); //evrene geç.
                }
            }
            if (hit.collider.CompareTag("OrangeButton"))
            {
                Debug.Log("orange button algiliyor");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("e ye basılıyopr");
                    if (wordleController.dreamIsOpen && wordleController.adultIsOpen && wordleController.alarmIsOpen && wordleController.guardIsOpen && wordleController.limitIsOpen)
                    {
                        isOrangeButtonTaken = true;
                        PlayerPrefs.SetInt("HaveOrangeBool", isOrangeButtonTaken ? 1 : 0);
                        PlayerPrefs.Save();
                        StartCoroutine(fowIncrease(2));

                    }
                }

            }
            if (hit.collider.CompareTag("GreenButton"))
            {
                Debug.Log("Green button algilaniyor");
                if (Input.GetKeyDown(KeyCode.E) && lockController.canTakeGreenButton == true)
                {
                    isGreenButtonTaken = true;
                    PlayerPrefs.SetInt("HaveGreenBool", isGreenButtonTaken ? 1 : 0);
                    PlayerPrefs.Save();
                    StartCoroutine(fowIncrease(2));

                }
            }
            if (hit.collider.CompareTag("RedButton"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isRedButtonTaken = true;
                    PlayerPrefs.SetInt("HaveRedBool", isRedButtonTaken ? 1 : 0);
                    PlayerPrefs.Save();
                    StartCoroutine(fowIncrease(2));
                }
            }

        }
    }
    IEnumerator fowIncrease(int sceneIndex)
    {
        mainCamera.GetComponent<CameraFollow>().mouseSensitivity = 0;
        mainCamera.DOFieldOfView(179, 2f);

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneIndex);

    }
    IEnumerator fowDecrease(int sceneIndex)
    {
        mainCamera.GetComponent<CameraFollow>().mouseSensitivity = 0;
        mainCamera.DOFieldOfView(60, 2f);

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneIndex);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DoubleJump"))
        {
            doubleJumpPlus = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BluePortal"))
        {
            StartCoroutine(fowIncrease(3));

        }
        if (other.gameObject.CompareTag("OrangePortal"))
        {
            StartCoroutine(fowIncrease(4));
        }
        if (other.gameObject.CompareTag("RedPortal"))
        {
            StartCoroutine(fowIncrease(5));
        }
        if (other.gameObject.CompareTag("GreenPortal"))
        {
            StartCoroutine(fowIncrease(6));
        }
        if (other.gameObject.CompareTag("FallDetector"))
        {
            this.transform.position = playerStartPosition;
        }
        if (other.gameObject.CompareTag("Door"))
        {
            doorAnimator.SetTrigger("openDoor");
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("yukselti"))
        {
            rigidbody.velocity = (Vector3.up * yukseklik * Time.deltaTime);
            //rigidbody.AddForce(Vector3.up * yukseklik, ForceMode.Impulse);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Stone"))
        {
            Debug.Log("leave stone");
            StartCoroutine(StonePhysics(other.gameObject));
        }
    }
    private IEnumerator StonePhysics(GameObject stone)
    {
        Rigidbody rb = stone.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        yield return new WaitForSeconds(2f);
        rb.isKinematic = false;
        Destroy(stone.gameObject, 5f);
    }
}

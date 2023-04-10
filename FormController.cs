using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FormController : MonoBehaviour
{

    public Button showFormButton;
    public Button onaylaButton;
    public TextMeshProUGUI baslikText;
    public TextMeshProUGUI aciklamaText;
    public Toggle sozlesmeToggle;
    public Button thisPC, recycleBin, slack, form, back, chrome;
    public Image dinoImage;
    public GameObject windowsWallpaper;
    public Camera fpsCamera;
    public Camera uiCamera;
    public GameObject pcCanvas;
    public PlayerController playerController;
    public BlueController blueController;
    public GameObject kargoObject;
    public GameObject player;
    public AudioSource audioSource;
    public AudioClip zilSesiClip;
   
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        kargoObject.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
        blueController = FindObjectOfType<BlueController>();
        Debug.Log(pcCanvas);
        showFormButton.onClick.AddListener(delegate { ShowForm(); });
        onaylaButton.onClick.AddListener(delegate { Onayla(); });
        back.onClick.AddListener(delegate { Back2Homepage(); });
        onaylaButton.interactable = false;
        chrome.onClick.AddListener(delegate { DinoScreen(); });

    }
    private void Update()
    {      
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fpsCamera.gameObject.SetActive(true);
            fpsCamera.GetComponent<Camera>().enabled=true;
            player.SetActive(true);
            pcCanvas.gameObject.SetActive(false);
            uiCamera.gameObject.SetActive(false);
            blueController.crosshair.gameObject.SetActive(true);
        }
    }

    void ShowForm()
    {
        chrome.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        fpsCamera.GetComponent<Camera>().enabled=false;
        uiCamera.gameObject.SetActive(true);
        windowsWallpaper.gameObject.SetActive(false);
        form.gameObject.SetActive(false);
        thisPC.gameObject.SetActive(false);
        recycleBin.gameObject.SetActive(false);
        slack.gameObject.SetActive(false);
        baslikText.gameObject.SetActive(true);
        aciklamaText.gameObject.SetActive(true);
        sozlesmeToggle.gameObject.SetActive(true);
        onaylaButton.gameObject.SetActive(true);
        sozlesmeToggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
    }
    public void Back2Homepage()
    {
        dinoImage.gameObject.SetActive(false);
        chrome.gameObject.SetActive(true);
        sozlesmeToggle.isOn = false;
        back.gameObject.SetActive(false);
        fpsCamera.GetComponent<Camera>().enabled=false;
        uiCamera.gameObject.SetActive(true);
        windowsWallpaper.gameObject.SetActive(true);
        form.gameObject.SetActive(true);
        thisPC.gameObject.SetActive(true);
        recycleBin.gameObject.SetActive(true);
        slack.gameObject.SetActive(true);
        baslikText.gameObject.SetActive(false);
        aciklamaText.gameObject.SetActive(false);
        sozlesmeToggle.gameObject.SetActive(false);
        onaylaButton.gameObject.SetActive(false);
    }
    public void DinoScreen()
    {
        dinoImage.gameObject.SetActive(true);
        chrome.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        fpsCamera.GetComponent<Camera>().enabled=false;
        uiCamera.gameObject.SetActive(true);
        windowsWallpaper.gameObject.SetActive(false);
        form.gameObject.SetActive(false);
        thisPC.gameObject.SetActive(false);
        recycleBin.gameObject.SetActive(false);
        slack.gameObject.SetActive(false);
        baslikText.gameObject.SetActive(false);
        aciklamaText.gameObject.SetActive(false);
        sozlesmeToggle.gameObject.SetActive(false);
        onaylaButton.gameObject.SetActive(false);
    }

    void OnToggleValueChanged()
    {
        if (sozlesmeToggle.isOn)
        {
            onaylaButton.interactable = true; // butonu aktiflestir
        }
        else
        {
            onaylaButton.interactable = false; // butonu devre disi birak
        }
    }

    void Onayla()
    {
        //4 levela gidebilecegi main sahneye geçecek.
        Debug.Log("Form kabul edildi.");
        //gameObject.SetActive(false);
        //zil sesi calcak.

        //kargo spawn

        kargoObject.SetActive(true);
        audioSource.PlayOneShot(zilSesiClip);
    }
}

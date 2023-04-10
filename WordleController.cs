using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordleController : MonoBehaviour
{
    public GameObject dreamD, dreamR, dreamE, dreamA, dreamM;
    public GameObject adultA, adultD, adultU, adultL, adultT;
    public GameObject alarmA, alarmL, alarmA2, alarmR, alarmM;
    public GameObject guardG, guardU, guardA, guardR, guardD;
    public GameObject limitL, limitI, limitM, limitI2, limitT;
    public AudioSource audioSource;
    public AudioClip clip;
    public TMP_InputField tMP_InputField;
    public bool dreamIsOpen = false;
    public bool adultIsOpen = false;
    public bool alarmIsOpen = false;
    public bool guardIsOpen = false;
    public bool limitIsOpen = false;
    void Start()
    {
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;
        audioSource.playOnAwake = false;
        dreamD.SetActive(false);
        dreamR.SetActive(false);
        dreamE.SetActive(false);
        dreamA.SetActive(false);
        dreamM.SetActive(false);

        adultA.SetActive(false);
        adultD.SetActive(false);
        adultU.SetActive(false);
        adultL.SetActive(false);
        adultT.SetActive(false);

        alarmA.SetActive(false);
        alarmL.SetActive(false);
        alarmA2.SetActive(false);
        alarmR.SetActive(false);
        alarmM.SetActive(false);

        guardG.SetActive(false);
        guardU.SetActive(false);
        guardA.SetActive(false);
        guardR.SetActive(false);
        guardD.SetActive(false);

        limitL.SetActive(false);
        limitI.SetActive(false);
        limitM.SetActive(false);
        limitI2.SetActive(false);
        limitT.SetActive(false);

        tMP_InputField.characterLimit = 5;

    }


    void Update()
    {       
        StartCoroutine(DreamWord(.5f));
        StartCoroutine(AdultWord(.5f));
        StartCoroutine(AlarmWord(.5f));
        StartCoroutine(GuardWord(.5f));
        StartCoroutine(LimitWord(.5f));
    }
    private IEnumerator DreamWord(float wordTime)
    {
        tMP_InputField.text = tMP_InputField.text.ToUpper();
        string dreamWord = tMP_InputField.text;
        if (dreamWord == "DREAM")
        {
            dreamIsOpen = true;
            tMP_InputField.text = "";
            dreamD.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            dreamR.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            dreamE.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            dreamA.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            dreamM.SetActive(true);
            audioSource.PlayOneShot(clip);

        }
    }
    private IEnumerator AdultWord(float wordTime)
    {
        tMP_InputField.text = tMP_InputField.text.ToUpper();
        string adultWord = tMP_InputField.text;
        if (adultWord == "ADULT")
        {
            adultIsOpen = true;
            tMP_InputField.text = "";
            adultA.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            adultD.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            adultU.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            adultL.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            adultT.SetActive(true);
            audioSource.PlayOneShot(clip);
        }

    }
    private IEnumerator AlarmWord(float wordTime)
    {

        tMP_InputField.text = tMP_InputField.text.ToUpper();
        string alarmWord = tMP_InputField.text;
        if (alarmWord == "ALARM")
        {
            alarmIsOpen = true;
            tMP_InputField.text = "";
            alarmA.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            alarmL.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            alarmA2.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            alarmR.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            alarmM.SetActive(true);
            audioSource.PlayOneShot(clip);
        }

    }
    private IEnumerator GuardWord(float wordTime)
    {

        tMP_InputField.text = tMP_InputField.text.ToUpper();
        string guardWord = tMP_InputField.text;
        if (guardWord == "GUARD")
        {
            guardIsOpen = true;
            tMP_InputField.text = "";
            guardG.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            guardU.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            guardA.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            guardR.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            guardD.SetActive(true);
            audioSource.PlayOneShot(clip);
        }

    }
    private IEnumerator LimitWord(float wordTime)
    {

        tMP_InputField.text = tMP_InputField.text.ToUpper();
        string limitWord = tMP_InputField.text;
        if (limitWord == "LIMIT")
        {
            limitIsOpen = true;
            tMP_InputField.text = "";
            limitL.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            limitI.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            limitM.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            limitI2.SetActive(true);
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordTime);
            limitT.SetActive(true);
            audioSource.PlayOneShot(clip);
        }

    }


}

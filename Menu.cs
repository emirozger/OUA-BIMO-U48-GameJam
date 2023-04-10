using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Menu : MonoBehaviour
{
    private Vector3 scaleQuitButton;
    public GameObject scaleQuitButton2;
    private Vector3 scaleBackQuitButton;




    public void Awake()
    {
        scaleQuitButton = new Vector3(3314.139f, 3314.139f,3314.139f);
        scaleBackQuitButton = new Vector3(2881.865f, 2881.865f, 2881.865f);
        
    }


    public void OnMouseOver()
    {
        
        scaleQuitButton2.transform.DOScale(scaleQuitButton,1f);
        if (Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene(1);      
        }
    }

    public void OnMouseExit()
    {
        gameObject.transform.localScale = scaleBackQuitButton;
        scaleQuitButton2.transform.DOScale(scaleBackQuitButton,1f);

    }
}

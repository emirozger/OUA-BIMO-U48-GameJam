using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    



    public void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Application.Quit();
            Debug.Log("UygulamadanCikisYapıldı");
        }
    }

 
}

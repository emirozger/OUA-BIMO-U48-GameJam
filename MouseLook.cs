using UnityEngine;
using DG.Tweening;

public class MouseLook : MonoBehaviour
{
    public Camera camera;
    private void Start()
    {
        camera=Camera.main;
        camera.DOFieldOfView(60,1.5f);
    }
}
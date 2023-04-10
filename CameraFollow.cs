using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target; // takip edilecek hedef
    public float smoothSpeed = 0.125f; // takip hareketinin yumuşaklığı
    public Vector3 offset; // kameranın hedefe göre konumu

    public float mouseSensitivity = 100f; // fare hassasiyeti
    public Transform playerBody; // oyuncu karakteri
    float xRotation = 0f; // x ekseninde dönüş açısı
    float yRotation = 0f; // y ekseninde dönüş açısı

    void Start() {
       //!!!unutmaa //Cursor.lockState = CursorLockMode.Locked; // fareyi kitle
    }

    void LateUpdate() {
        Vector3 desiredPosition = target.position + offset; // hedefin konumunu al ve ofsetle birleştir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // kamera konumunu yumuşat
        transform.position = smoothedPosition; // kamera konumunu ayarla

        transform.LookAt(target); // hedefe doğru bak

        // fare dönüş hareketleri
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}

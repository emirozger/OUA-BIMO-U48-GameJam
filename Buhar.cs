using UnityEngine;

public class Buhar : MonoBehaviour
{
    public float amplitude = 0.5f; // Salınımın genliği
    public float frequency = 1f; // Salınımın frekansı
    public float damping = 0.1f; // Salınımın sönümleme oranı

    private Vector3 initialPosition;
    private float initialTime;

    private void Start()
    {
        initialPosition = transform.position;
        initialTime = Time.time;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 force = CalculateForce(other.transform.position);
            rb.AddForce(force, ForceMode.Acceleration);
            Debug.Log("çarptı");
        }
    }

    private Vector3 CalculateForce(Vector3 position)
    {
        float time = Time.time - initialTime;
        float displacement = Mathf.Sin(time * frequency) * amplitude;
        Vector3 direction = (position - initialPosition).normalized;
        Vector3 force = direction * displacement * damping;
        return force;
    }
}

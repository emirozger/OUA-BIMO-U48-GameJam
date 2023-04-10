using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FollowAI : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public float rotationSpeed = 2f;
    public float maxHeight = 3f;
    public float minHeight = 0f;
    public float upDownSpeed = 1f;
    public float upDownDistance = 1f;

    private bool isSwinging = false;
    public float speed = 3.5f;

    private bool isMovingUpDown = false;
    private float startY;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startY = transform.localPosition.y;
    }

    void Update()
    {
      
        agent.SetDestination(target.position);
        Vector3 direction = (target.position - transform.position).normalized;     
        direction = Quaternion.AngleAxis(-180, Vector3.up) * direction;

       
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

       
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            if (!isSwinging)
            {
                isSwinging = true;
                isMovingUpDown = true;
               //StartCoroutine(Swing());
            }
        }
        else
        {
            agent.isStopped = false;
            StopAllCoroutines();
            isSwinging = false;
            isMovingUpDown = false;
        }
    }

    IEnumerator Swing()
    {
        float height = transform.localPosition.y;
        while (height < maxHeight && isMovingUpDown)
        {
            height += Time.deltaTime * speed;
            transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
            yield return null;
        }
        
        isMovingUpDown = false;
        
        while (height > startY && !isMovingUpDown)
        {
            height -= Time.deltaTime * speed;
            transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
            yield return null;
        }

        if (height < startY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, startY, transform.localPosition.z);
        }

        isSwinging = false;
    }
}

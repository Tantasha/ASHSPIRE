using System.Collections;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public Vector2[] patrolPoints;
    public float speed = 2;

    public float pauseDuration = 1.5f;

    private bool isPaused;
    private int currentPatrolIndex;
    private Vector2 target;


    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector3)target - transform.position).normalized;
        rb.velocity = direction * speed;

        if(Vector2.Distance(transform.position, target) < .1f)
            StartCoroutine(SetPatrolPoint());
    }

    IEnumerator SetPatrolPoint()
    {
        isPaused = true;

        yield return new WaitForSeconds(pauseDuration);

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex];
        isPaused = false;
    }
}

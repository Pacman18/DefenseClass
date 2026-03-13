using UnityEngine;

public class Agent : MonoBehaviour
{
    public AgentManager.Team Team { get; set; }

    private bool isMoving = false;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Agent: " + Team + " is moving");
            isMoving = true;
        }

        if (isMoving)
        {
            animator.Play("WALK");
            Move();
        }
        
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }
}

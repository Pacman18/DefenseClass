using UnityEngine;


public class Agent : MonoBehaviour
{
    public int UID;


    private STATE _state = STATE.NONE;

    [SerializeField]
    Animator _animator;

    public STATE State
    {
        get { return _state; }
        set 
        { 
            ExitState(_state);

            _state = value; 

            EnterState(_state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move();

        switch (_state)
        {
            case STATE.NONE:
                break;
            case STATE.IDLE:
                break;
            case STATE.WALK:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DIE:
                break;
            case STATE.HITTED:
                break;
        }
    }

    private void Move()
    {
        transform.position += Vector3.left * Time.deltaTime * 3;
    }

    private void EnterState(STATE state)
    {
        switch (state)
        {
            case STATE.NONE:            
                break;
            case STATE.IDLE:
                _animator.Play("IDLE");
                break;
            case STATE.WALK:
                _animator.Play("WALK");                
                break;
            case STATE.ATTACK:
                _animator.Play("ATTACK");
                break;
            case STATE.DIE:
                break;
            case STATE.HITTED:
                break;
        }

        Debug.Log("EnterState: " + state);
        
    }

    private void ExitState(STATE state)
    {
        switch (state)
        {
            case STATE.NONE:
                break;
            case STATE.IDLE:
                break;
            case STATE.WALK:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DIE:
                break;
            case STATE.HITTED:
                break;
        }

        Debug.Log("ExitState: " + state);   
    }

}

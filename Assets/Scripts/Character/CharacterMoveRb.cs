using UnityEngine;


public class CharacterMoveRb : MonoBehaviour
{
    [Header("PLAYER SETTINGS------------------")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsuleCollider;

    [Header("MOVE SETTINGS------------------")]
    [SerializeField] private Transform _groundChechPoint;
    [SerializeField] private LayerMask _groundLayer;

    private bool isGrounded;
    private bool isDuck;

    public int currentPosition = 0;

    [Header("CHARACER------------------")]
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private CharacterAnimator _characterAnimator;
 
    private void OnEnable()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        CoreGameSignals.Instance.onRestartGame += OnRestartGame;
    }

    public void OnRestartGame()
    {
        currentPosition = 0;
    }

    private void Update()
    {
        if (_characterStats.Speed > 0)
        {
            PlayerMovement();
        }
          WayCheck();
        IsGrounded();
    }


    private void WayCheck()
    {
        switch (currentPosition)
        {
            case -1:
                ChangeWay(-3);
                break;
            case 0:

                ChangeWay(0);
                break;
            case 1:
                ChangeWay(3);
                break;
        }
    }
    private void IsGrounded()
    {
        if (transform.position.y<0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
    private void ChangeWay(int value)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(value, transform.position.y, transform.position.z), .1f);
    }
    private void PlayerMovement()
    {
        transform.Translate(0, 0, _characterStats.Speed * Time.deltaTime, Space.World);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || MouseInputManager.swipeLeft)
        {
            if (currentPosition > -1)
            {
                currentPosition -= 1;
            }      
            _characterAnimator.CrossFade("JumpLeftCreature", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || MouseInputManager.swipeRight)
        {         
            if (currentPosition <1)
            {
                currentPosition += 1;
            }         
            _characterAnimator.CrossFade("JumpRightCreature", 0.1f);
        }

        isGrounded = Physics.Raycast(_groundChechPoint.position, Vector3.down, .5f, _groundLayer);
        if ((Input.GetKeyDown(KeyCode.UpArrow) || MouseInputManager.swipeUp) && isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _characterStats.JumpForce, ForceMode.Impulse);
            _characterAnimator.CrossFade("Jump Forward", 0.1f);
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow) || MouseInputManager.swipeDown) && !isDuck)
        {
            _capsuleCollider.height/= 2;
            _capsuleCollider.center /= 2;
            _rigidbody.AddForce(Vector3.down * _characterStats.JumpForce, ForceMode.Impulse);
            isDuck = true;
            Invoke("IsDuckFalse", .5f);
            _characterAnimator.CrossFade("Roll", 0.1f);
        }
    }
    private void IsDuckFalse()
    {       
        isDuck = false;
        _capsuleCollider.height *= 2;
        _capsuleCollider.center *= 2;
    }
}

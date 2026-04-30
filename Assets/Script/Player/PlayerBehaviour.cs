using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpVelocity = 1f;

    private float _vInput;
    private float _hInput;
    private bool _isJumping;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _vInput=Input.GetAxis("Vertical")*MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        _isJumping |= Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position+transform.forward*_vInput*Time.fixedDeltaTime);
        Quaternion angelRot = Quaternion.Euler(Vector3.up * _hInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation*angelRot);

        if (_isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
            _isJumping = false; 
        }

    }
}

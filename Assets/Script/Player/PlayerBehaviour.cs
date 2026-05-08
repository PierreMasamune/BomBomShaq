using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpVelocity = 1f;
    public bool IsOnGround = true;
    public float GroundCheckRadius = 0.3f;
    public LayerMask GroundLayer;
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    

    private float _vInput;
    private float _hInput;
    private bool _isJumping;
    private Rigidbody _rb;
    private bool _isShooting;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GroundCheckRadius);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        IsOnGround = Physics.CheckSphere(transform.position, GroundCheckRadius, GroundLayer);

        _isShooting |= Input.GetMouseButtonDown(0);
        _vInput=Input.GetAxis("Vertical")*MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _isJumping = true;
        }

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

        if(_isShooting)
        {
            Vector3 spawnPos = transform.position + transform.forward * 1f;
            GameObject newBullet = Instantiate(Bullet, spawnPos, this.transform.rotation);

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.linearVelocity = this.transform.forward*BulletSpeed;
            _isShooting = false;

        }
    }
}

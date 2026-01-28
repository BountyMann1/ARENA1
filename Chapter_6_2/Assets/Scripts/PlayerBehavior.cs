using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    //1
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpVelocity = 5f;
    private bool _isJumping;
    //2
    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;
    void Start()
    {
        //3
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        _isJumping |= Input.GetKeyDown(KeyCode.J);
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
    }
    void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * _vInput * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + moveDirection);
        Vector3 rotation = Vector3.up * _hInput * Time.fixedDeltaTime;
        Quaternion angleRot = Quaternion.Euler(rotation);
        _rb.MoveRotation(_rb.rotation * angleRot);
        
        if(_isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        if(IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity,
            ForceMode.Impulse);
        }
        _isJumping = false;

        if (_isShooting)
        {
            Vector3 spawnPos = transform.position + transform.forward * 1f;
            GameObject newBullet = Instantiate(Bullet, spawnPos, this.transform.rotation);
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.linearVelocity = this.transform.forward * BulletSpeed;
        }
    }
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
                                            _col.bounds.min.y,
                                            _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center,
                                            capsuleBottom, DistanceToGround, GroundLayer, 
                                            QueryTriggerInteraction.Ignore);
        return grounded;
    }
}
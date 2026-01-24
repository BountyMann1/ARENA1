using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    //1
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    //2
    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    void Start()
    {
        //3
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _vInput = Input.GetAxis("Vartical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * Time.deltaTime);
    }
    void FixedUpdate()
    {
        //3
        Vector3 rotation = Vector3.up * _hInput;
        //4
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        //5
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        //6
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}
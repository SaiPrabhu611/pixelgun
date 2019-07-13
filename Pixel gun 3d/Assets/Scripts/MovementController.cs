using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUDrotation = 0f;
    private float CurrentCameraUpDownRotation = 0f;

    [SerializeField]
    private float looksensitivity = 3f;

    [SerializeField]
    GameObject fpsCamera;

    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //calculate movement velocity
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        //final movement velocity
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;

        //Apply movement
        Move(_movementVelocity);

        //calculate look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") * looksensitivity;

        //Apply camera rot
        RotateCamera(_cameraUpDownRotation);

        //calculate rotation as a 3d vector for turning around
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _yRotation, 0) * looksensitivity;

         //Apply Rotation
        Rotate(_rotationVector);
    }

    //runs per physical iteration
    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (fpsCamera != null)
        {
            CurrentCameraUpDownRotation -= CameraUDrotation;
            CurrentCameraUpDownRotation = Mathf.Clamp(CurrentCameraUpDownRotation, -85, 85);

            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpDownRotation, 0, 0);
        }
    }
    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    void RotateCamera(float cameraUpDownRotation)
    {
        CameraUDrotation = cameraUpDownRotation;
    }
}
 
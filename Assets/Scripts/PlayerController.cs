using UnityEngine;

// Bu kod otomatik olarak bir CharacterController ekliyor
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float walkSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Kamera Ayarlarý")]
    public float mouseSensitivity = 2f;
    public Transform playerCamera;
    public float maxLookAngle = 85f;

    [Header("Savaþ Ayarlarý")]
    public int damage = 20;

    private IWeapon myWeapon;

    private CharacterController controller;
    private float verticalRotation = 0f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Oyun baþladýðýnda fare imlecini gizle ve ekranýn ortasýna kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Oyuna girerken karakterin eline Temel Silahý veriyoruz!
        myWeapon = new BasicWeapon();
    }

    void Update()
    {
        HandleMouseLook(); 
        HandleMovement();  

        // Sol týk ile ateþ etme
        if (Input.GetMouseButtonDown(0))
        {
            myWeapon.Fire(playerCamera); // Silah karar versin nasýl ateþ edeceðine!
        }

        // DEKORATÖR TESTÝ: "E" tuþuna basýnca silaha güçlendirme tak!
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Temel silahý al, DoubleShot kýlýfýnýn (Decorator) içine koy!
            myWeapon = new DoubleShotDecorator(myWeapon);
            Debug.Log("SÝLAH GÜÇLENDÝRÝLDÝ (UPGRADE ALINDI)!");
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Karakterin tamamýný saða/sola döndür
        transform.Rotate(Vector3.up * mouseX);

        // Sadece kamerayý yukarý/aþaðý döndür
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * walkSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
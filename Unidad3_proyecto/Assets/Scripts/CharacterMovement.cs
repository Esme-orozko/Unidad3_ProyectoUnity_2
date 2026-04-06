using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CharacterMovement : MonoBehaviour
{
    public GameObject FireballPrefab;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    [SerializeField] private float velocidad = 5f;
    public float JumpForce;
    private bool Grounded;
    private float LastShoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();//Nos permite acceder a Rigidbody2D
        Animator = GetComponent<Animator>();//Nos permite acceder a Animator
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal =
            Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed ? -1 :
            Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed ? 1 : 0;

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.blue);
        if (Physics2D.Raycast(transform.position, Vector3.down, 1.2f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && Grounded)
        {
            Jump();
        }
        Animator.SetBool("grounded", Grounded);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject fireball = Instantiate(FireballPrefab, transform.position + direction * 0.6f, Quaternion.identity);

        Physics2D.IgnoreCollision(
            fireball.GetComponent<Collider2D>(),
            GetComponent<Collider2D>()
        );

        fireball.GetComponent<FireballScript>().SetDirection(direction);
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * velocidad, Rigidbody2D.linearVelocity.y);
    }
}

using UnityEngine;

public class PlayerControler : MonoBehaviour
{
      [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool mirandoDerecha = true;
    private bool enSuelo = false;

    [Header("Detección de suelo")]
    [SerializeField] private Transform sensorSuelo;   // Punto desde donde lanza el raycast
    [SerializeField] private float distanciaSensor = 0.1f; // Distancia para detectar suelo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Movimiento horizontal ---
        float movimiento = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);

        // Voltear sprite según dirección
        if (movimiento > 0 && !mirandoDerecha)
            Voltear();
        else if (movimiento < 0 && mirandoDerecha)
            Voltear();

        // --- Detección de suelo (Raycast hacia abajo) ---
        RaycastHit2D hit = Physics2D.Raycast(sensorSuelo.position, Vector2.down, distanciaSensor);

        if (hit.collider != null && hit.collider.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
        else
        {
            enSuelo = false;
        }

        // --- Salto ---
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            Saltar();
        }

        // --- Actualización de animaciones ---
        ActualizarAnimaciones(movimiento);
    }

    void Saltar()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        enSuelo = false; // Evita saltos dobles
    }

    void ActualizarAnimaciones(float movimiento)
    {
        // Movimiento lateral
        bool moviendo = movimiento != 0;
        anim.SetBool("moviendo", moviendo);

        // Saltar / Caer
        if (!enSuelo && rb.linearVelocity.y > 0.1f)
        {
            anim.SetBool("saltar", true);
            anim.SetBool("Cayendo", false);
        }
        else if (!enSuelo && rb.linearVelocity.y < -0.1f)
        {
            anim.SetBool("saltar", false);
            anim.SetBool("Cayendo", true);
        }
        else
        {
            anim.SetBool("saltar", false);
            anim.SetBool("Cayendo", false);
        }
    }

    void Voltear()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    // Muestra el rayo en la escena (solo visible en modo Scene)
    void OnDrawGizmosSelected()
    {
        if (sensorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(sensorSuelo.position, sensorSuelo.position + Vector3.down * distanciaSensor);
        }
    }
    
}

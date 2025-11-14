using UnityEngine;

public class RainbowArc : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Collider2D col;
    private bool activo = true; // Si se puede caminar sobre él
    private Vector2 direccion;
    public float velocidad = 6f;
    public float tiempoVida = 3f; 
     public void Start()
    {
        Destroy(gameObject, tiempoVida);
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Al principio el arcoíris no usa gravedad ni se cae
        rb.gravityScale = 0;
        rb.isKinematic = false;
    }

    void Update()
    {
        if (activo)
        {
            // Movimiento inicial
            rb.linearVelocity = direccion * velocidad;
        }
    }

    public void SetDireccion(Vector2 dir)
    {
        direccion = dir;
    }

    // Cuando el arcoíris choca con algo, se queda quieto y sirve como plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activo) return;

        // Al tocar pared o suelo, se fija en el aire
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.isKinematic = true;
        activo = false;
    }

    // Llamado cuando el jugador salta — hará que el arcoíris caiga
    public void ActivarCaida()
    {
        rb.isKinematic = false;
        rb.gravityScale = 2f;
        col.enabled = false; // desactiva colisiones, así cae sin estorbar
        Destroy(gameObject, 2f); // se destruye tras unos segundos
    }
}

using UnityEngine;

public class RainbowShoot : MonoBehaviour
{

    [Header("Configuración del disparo")]
    public GameObject arcoirisPrefab;  // Prefab del arcoíris
    public Transform puntoDisparo;     // Donde se crea
    public float fuerza = 10f;         // Velocidad del arcoíris
    public float impulsoPlayer = 3f;   // Fuerza del impulso al disparar

    private bool mirandoDerecha = true;
    private Rigidbody2D rb; // referencia al cuerpo del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detección de dirección
        float mov = Input.GetAxisRaw("Horizontal");
        if (mov > 0) mirandoDerecha = true;
        else if (mov < 0) mirandoDerecha = false;

        // Disparo
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        // Instancia el arcoíris
        GameObject arcoiris = Instantiate(arcoirisPrefab, puntoDisparo.position, Quaternion.identity);

        // Ajusta dirección visual
        if (!mirandoDerecha)
            arcoiris.transform.localScale = new Vector3(7, 7, 7);

        // Le da dirección de movimiento
        RainbowArc rainbow = arcoiris.GetComponent<RainbowArc>();
        if (rainbow != null)
            rainbow.SetDireccion(mirandoDerecha ? Vector2.right : Vector2.left);

        // 🔹 AÑADIMOS EL IMPULSO AL PLAYER
        Vector2 dir = mirandoDerecha ? Vector2.right : Vector2.left;
        rb.AddForce(dir * impulsoPlayer, ForceMode2D.Impulse);
    }
}

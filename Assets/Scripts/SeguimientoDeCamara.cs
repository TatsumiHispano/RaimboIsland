using UnityEngine;

public class SeguimientoDeCamara : MonoBehaviour
{
    [Header("Configuración")]
    public Transform player;     // El transform del jugador
    public float suavizado = 5f; // Qué tan suave sigue al jugador (ajusta en el inspector)

    private float posX; // Guardamos la posición fija en X

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("⚠️ Asigna el Player en el campo 'player' del script CameraFollowY.");
            enabled = false;
            return;
        }

        
        posX = transform.position.x;
    }

    void LateUpdate()
    {
        // La posición deseada mantiene X fija y sigue al player en Y
        Vector3 posicionDeseada = new Vector3(posX, player.position.y, transform.position.z);

        // Movimiento suave (interpolación)
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
    }
}


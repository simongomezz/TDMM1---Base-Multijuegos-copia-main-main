using UnityEngine;
using UnityEngine.UI;

public class TutorialUIController : MonoBehaviour
{
    [Header("Instrucciones Iniciales")]
    public Image instruccionesImagen;         // Imagen de instrucciones inicial
    public GameObject marcoInstrucciones;     // Marco de la imagen inicial
    public Sprite[] secuenciaSpritesInicial;  // Secuencia de sprites para la primera imagen
    public float tiempoCambioSpriteInicial = 0.5f; // Tiempo entre cambios de sprite para la primera imagen
    public Image imagenAdicional;             // Nueva imagen adicional
    public float pulsacionVelocidad = 1f;     // Velocidad de palpitación
    public float pulsacionAmplitud = 0.1f;    // Amplitud de la palpitación

    [Header("Segunda Imagen")]
    public Image segundaImagen;              // Imagen de la segunda fase
    public GameObject marcoSegundaImagen;    // Marco para la segunda imagen
    public Sprite[] secuenciaSpritesSegunda; // Secuencia de sprites para la segunda imagen
    public float tiempoCambioSpriteSegunda = 0.5f; // Tiempo entre cambios de sprite para la segunda imagen
    public Image imagenAdicionalSegunda;      // Nueva imagen adicional para la segunda imagen

    [Header("Imagen de Atrapado")]
    public Image atrapadoImagen;             // Imagen que aparece al ser atrapado
    public GameObject marcoAtrapado;         // Marco de la imagen de atrapado
    public Sprite[] secuenciaSpritesAtrapado; // Secuencia de sprites para el atrapado
    public float tiempoCambioSpriteAtrapado = 0.5f; // Tiempo entre cambios de sprite para el atrapado
    public Image imagenAdicionalAtrapado;     // Nueva imagen adicional para atrapado

    private bool carrilCambiado = false;      // Verifica si el jugador ha cambiado de carril
    public bool segundaImagenActiva = false;  // Verifica si la segunda imagen está activa
    private int spriteIndexInicial = 0;       // Índice actual de la secuencia inicial
    private int spriteIndexSegunda = 0;       // Índice actual de la secuencia de la segunda imagen
    private int spriteIndexAtrapado = 0;      // Índice actual de la secuencia de atrapado
    private float tiempoTranscurridoInicial = 0f; // Temporizador para la secuencia inicial
    private float tiempoTranscurridoSegunda = 0f; // Temporizador para la segunda secuencia
    private float tiempoTranscurridoAtrapado = 0f; // Temporizador para la secuencia de atrapado

    void Start()
    {
        if (instruccionesImagen != null && secuenciaSpritesInicial.Length > 0)
        {
            instruccionesImagen.sprite = secuenciaSpritesInicial[0];
            instruccionesImagen.gameObject.SetActive(true);
        }

        if (marcoInstrucciones != null)
            marcoInstrucciones.SetActive(true);

        if (imagenAdicional != null)
            imagenAdicional.gameObject.SetActive(true);

        if (segundaImagen != null && secuenciaSpritesSegunda.Length > 0)
        {
            segundaImagen.sprite = secuenciaSpritesSegunda[0];
            segundaImagen.gameObject.SetActive(false);
        }

        if (marcoSegundaImagen != null)
            marcoSegundaImagen.SetActive(false);

        if (imagenAdicionalSegunda != null)
            imagenAdicionalSegunda.gameObject.SetActive(false);

        if (atrapadoImagen != null && secuenciaSpritesAtrapado.Length > 0)
        {
            atrapadoImagen.sprite = secuenciaSpritesAtrapado[0];
            atrapadoImagen.gameObject.SetActive(false);
        }

        if (marcoAtrapado != null)
            marcoAtrapado.SetActive(false);

        if (imagenAdicionalAtrapado != null)
            imagenAdicionalAtrapado.gameObject.SetActive(false);
    }

    void Update()
    {
        // Secuencia de instrucciones inicial
        if (!carrilCambiado && instruccionesImagen != null && secuenciaSpritesInicial.Length > 1)
        {
            tiempoTranscurridoInicial += Time.deltaTime;

            if (tiempoTranscurridoInicial >= tiempoCambioSpriteInicial)
            {
                tiempoTranscurridoInicial = 0f;
                spriteIndexInicial = (spriteIndexInicial + 1) % secuenciaSpritesInicial.Length;
                instruccionesImagen.sprite = secuenciaSpritesInicial[spriteIndexInicial];
            }
        }

        // Secuencia de la segunda imagen
        if (segundaImagenActiva && segundaImagen != null && secuenciaSpritesSegunda.Length > 1)
        {
            tiempoTranscurridoSegunda += Time.deltaTime;

            if (tiempoTranscurridoSegunda >= tiempoCambioSpriteSegunda)
            {
                tiempoTranscurridoSegunda = 0f;
                spriteIndexSegunda = (spriteIndexSegunda + 1) % secuenciaSpritesSegunda.Length;
                segundaImagen.sprite = secuenciaSpritesSegunda[spriteIndexSegunda];
            }
        }

        // Secuencia de la imagen de atrapado
        if (atrapadoImagen != null && secuenciaSpritesAtrapado.Length > 1)
        {
            tiempoTranscurridoAtrapado += Time.deltaTime;

            if (tiempoTranscurridoAtrapado >= tiempoCambioSpriteAtrapado)
            {
                tiempoTranscurridoAtrapado = 0f;
                spriteIndexAtrapado = (spriteIndexAtrapado + 1) % secuenciaSpritesAtrapado.Length;
                atrapadoImagen.sprite = secuenciaSpritesAtrapado[spriteIndexAtrapado];
            }
        }

        // Aplicar efecto de palpitación a las imágenes adicionales activas
        AplicarPalpitacion(imagenAdicional);
        AplicarPalpitacion(imagenAdicionalSegunda);
        AplicarPalpitacion(imagenAdicionalAtrapado);
    }

    private void AplicarPalpitacion(Image imagen)
    {
        if (imagen != null && imagen.gameObject.activeSelf)
        {
            float escala = 1f + Mathf.Sin(Time.time * pulsacionVelocidad) * pulsacionAmplitud;
            imagen.transform.localScale = new Vector3(escala, escala, 1f);
        }
    }

    public void OcultarInstrucciones()
    {
        if (!carrilCambiado)
        {
            carrilCambiado = true;

            if (instruccionesImagen != null)
                instruccionesImagen.gameObject.SetActive(false);

            if (marcoInstrucciones != null)
                marcoInstrucciones.SetActive(false);

            if (imagenAdicional != null)
                imagenAdicional.gameObject.SetActive(false);
        }
    }

    public void MostrarSegundaImagen()
    {
        if (!segundaImagenActiva && segundaImagen != null && secuenciaSpritesSegunda.Length > 0)
        {
            segundaImagen.sprite = secuenciaSpritesSegunda[0];
            segundaImagen.gameObject.SetActive(true);
            segundaImagenActiva = true;
            tiempoTranscurridoSegunda = 0f;
            spriteIndexSegunda = 0;
        }

        if (marcoSegundaImagen != null)
            marcoSegundaImagen.SetActive(true);

        if (imagenAdicionalSegunda != null)
            imagenAdicionalSegunda.gameObject.SetActive(true);
    }

    public void OcultarSegundaImagen()
    {
        if (segundaImagen != null)
        {
            segundaImagen.gameObject.SetActive(false);
            segundaImagenActiva = false;
        }

        if (marcoSegundaImagen != null)
            marcoSegundaImagen.SetActive(false);

        if (imagenAdicionalSegunda != null)
            imagenAdicionalSegunda.gameObject.SetActive(false);
    }

    public void MostrarAtrapadoImagen()
    {
        if (atrapadoImagen != null && secuenciaSpritesAtrapado.Length > 0)
        {
            atrapadoImagen.sprite = secuenciaSpritesAtrapado[0];
            atrapadoImagen.gameObject.SetActive(true);
            tiempoTranscurridoAtrapado = 0f;
            spriteIndexAtrapado = 0;
        }

        if (marcoAtrapado != null)
            marcoAtrapado.SetActive(true);

        if (imagenAdicionalAtrapado != null)
            imagenAdicionalAtrapado.gameObject.SetActive(true);
    }

    public void OcultarAtrapadoImagen()
    {
        if (atrapadoImagen != null)
            atrapadoImagen.gameObject.SetActive(false);

        if (marcoAtrapado != null)
            marcoAtrapado.SetActive(false);

        if (imagenAdicionalAtrapado != null)
            imagenAdicionalAtrapado.gameObject.SetActive(false);
    }
}
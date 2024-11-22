using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class arduino : MonoBehaviour
{
    //En este lugar debemos poner el nombre del puerto al que esta conectado nuestro Arduino, los mas comunes son COM3
    //Lo encuentran en las opciones del codigo de arduino, en Herramientas->Puerto->"nombre del puerto conectado"
    //El segundo valor (9600) es el puerto serie al que se conecta con el arduino, deben tener el mismo valor que su codigo de arduino.
    public SerialPort serialPort = new SerialPort("COM3", 9600);
    // Start is called before the first frame update

    public GameObject player;

    void Start()
    {
        serialPort.ReadTimeout = 50;
        serialPort.Open();        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if(serialPort.IsOpen){
                //Imprimimimos el mensaje que llega
                print( serialPort.ReadLine() );
                if(ReadFromArduino()=="b"){
                    print("entro2");
                }
                serialPort.ReadByte();
                
                if(serialPort.ReadByte().ToString()=="l"){}
                //Comprobamos que el mensaje que nos llega es el caracter "b"
                if (string.Equals(serialPort.ReadLine(),"b"))
                {
                    //Realizamos las acciones en base a este mensaje. En este caso, mover a la derecha el jugador.
                    // ej: player.GetComponent<Player>().moverDerecha();
                }
                //Aca agregamos el resto de comprobaciones. Igual al ejemplo anterior.
                else if (string.Equals(serialPort.ReadLine(),"c"))
                {
                   // ej: player.GetComponent<Player>().moverIzquierda();
                }
            }
        }
        catch (System.Exception ex)
        {
            ex = new System.Exception();
        }

    }

    public string ReadFromArduino(int timeout = 0)
    {
        serialPort.ReadTimeout = timeout;
        try
        {
            return serialPort.ReadLine();
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}

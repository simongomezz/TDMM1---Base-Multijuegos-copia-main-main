using UnityEngine;
using System.Collections;

public class ReceiveAll : MonoBehaviour {

	public OSC oscReference;
    /*
    VARIABLES QUE USAMOS PARA CAPTURAR LAS POSICIONES DE LA INFO QUE RECIBIMOS DE OSC.
    ESTAS VARIABLES SE USAN CUANDO NO UTILIZAMOS PROCESSING
    */
    //Data0 es la posicion en X
    public float data0;
    //Data0 es la posicion en Y
    public float data1;
    //Data0 es la posicion en Z
    public float data2;

    /*
    Referencia del GameObjetc que vamos a acceder para pasarle la info.
    En este caso, el player
    */
	public GameObject player;

	// Use this for initialization
	void Start () {
		oscReference.SetAllMessageHandler(OnReceive);
        print(oscReference.outIP);
	}

    //Esta es la funcion que recibe el mensaje
	void OnReceive(OscMessage message){
        print( "Esta llegando el mensaje: " + message);

        /*
        El mensaje suele venir en forma de array, por eso lo separamos en los
        tres datos del giroscopio, X , Y , Z . 
        */
        if (message.GetFloat(0) != null) {
            data0 = message.GetFloat(0);
        }
        
        if (message.GetFloat(1) != null) {
            data1 = message.GetFloat(1);
        }

        if (message.GetFloat(2) != null) {
            data2 = message.GetFloat(2);
        }

        //Estos prints comentados son para ver que datos estan llegando
        print("dato X " + data0);
        print("dato Y " + data1);
        print("dato Z " + data2);


        /*
        ESTO SE UTILIZA SI UTILIZAMOS PROCESSING
        */

        /*
        Desde el processing del giroscopio mandamos una "direccion" al enviar el dato, 
        esta es la que comprobaremos para identificar que accion queremos que
        haga el personaje.
        */
        /*if ( message.address == "/saltar" ) {
            print("deberia saltar");
            //Aca deberiamos poner el metodo que hace saltar al player.
        } 
        else if ( message.address == "/posicionDerecha" ) {
            //Aca deberiamos poner el metodo que hace mover a la derecha.
            // ej: 
            //player.GetComponent<Player>().moverDerecha();
            print("deberia moverse a la derecha");
        }
        else if (message.address == "/posicionIzquierda")
        {
            //Aca deberiamos poner el metodo que hace mover a la izquierda.
            // ej: player.GetComponent<Player>().moverIzquierda();
            print("deberia moverse a la IZQUIERDA");
        }
        */
    }


	// Update is called once per frame
	void Update () {
        player.GetComponent<Player>().moveOSC(data0);
        /*
        Aca utilizamos las variables de OSC.
        En este lugar deben pasarle la informacion de las variables al metodo que
        utilicen desde el script del player para (como en el ejemplo), moverlo en X.
        */
	    // ej: player.GetComponent<Player>().moveOSC(data0);
        //player.GetComponent<Player>().moveOSC(data0);
	}
}

//Pulsadores - controla movimiento derecha e izquierda
const int pulsadorDerecha = 2;
const int pulsadorIzquierda = 3;
 
void setup() {
   Serial.begin(9600);
   
   //Pins entradas pulsadores
   pinMode (pulsadorDerecha, INPUT);
   pinMode (pulsadorIzquierda, INPUT);

   //Configuramos los pulsadores en High 
   digitalWrite(pulsadorDerecha, HIGH);
   digitalWrite(pulsadorIzquierda, HIGH);
}
 
void loop() {

  //Si el pulsador derecho es apretado, es igual a low, enviamos el mensaje a Unity
  if ( digitalRead(pulsadorDerecha) == LOW ){
    Serial.println(pulsadorDerecha); //Enviamos el msj a unity
    delay(10); //Agregamos un delay
  }
  
  if ( digitalRead(pulsadorIzquierda) == LOW ){
    Serial.println(pulsadorIzquierda);
    delay(10);
  }
}

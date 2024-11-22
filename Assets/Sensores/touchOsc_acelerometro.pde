import oscP5.*;
import netP5.*;

OscP5 oscP5;
NetAddress myRemoteLocation;

//for Accelerometer values
float degreeX = 0;    
float degreeZ = 0;    
float tdegreeX = 0;   
float tdegreeZ = 0;
float transX = 0;    
float ttransX = 0;

float curX = 0;
float curY = 0;
float curZ = 0;
float preX = 0;
float preY = 0;
float preZ = 0;
boolean isGetFirst = false;

float x_, y_, z_;

//estado del jugador 
//Creamos esta variable para saber en que posicion esta el jugador, para solo enviar si sucede que cambia el lugar
String estado = "centro";

void setup() {
    size(400, 400, P3D);

    oscP5 = new OscP5(this, 9000);
    myRemoteLocation = new NetAddress("192.168.134.199", 8000);

    oscP5.plug(this, "accxyz", "/accxyz");
    
    ttransX = width/2;
}


void draw() {
    //frameRate(30);
    background(38, 41, 44);
       
    //update rotate degree on x-axis and z-axis
    degreeX = degreeX + (tdegreeX-degreeX)/30;
    degreeZ = degreeZ + (tdegreeZ-degreeZ)/30;
    
    //println (  "DegreX "  + degreeX + " DegreZ "  + degreeZ);
    
    //update shift location on x-axis
    transX = transX + (ttransX-transX)/30;
    
    pushStyle();
    strokeWeight(2);
    stroke(255,255,255);
    line ( 200, 0, 200, 400 );
    popStyle();
    
    pushMatrix();
    translate(transX, height/2);
    rotateX(radians(degreeX));
    rotateZ(radians(degreeZ));
    drawSimplePhone();
    popMatrix();
    
       
    //CELU
    if ( (y_< 0) && z_ < 3){
      //izquierda
      sendValue(-1, "/posicionIzquierda");
      estado = "izquierda";
      println("Izquierda");
    }else if ( (y_> 0 & y_<2) && z_ < 3 ){
      //centro
      //sendValue(0);
      estado = "centro";
      println("centro");
    }else if ( (y_> 2 ) && z_ < 3 ){
      //Derecha
      sendValue(1, "/posicionDerecha");
      estado = "derecha";
      println("derecha");
    }
      
    
    if (z_ > 3 && estado != "salto" ){
      //sendValue(2, "/saltar"); // Prender esta variable si queremos que salte el personaje
      estado = "salto";
      println("saltaaaaaa");
    }  
          
          
}

void drawSimplePhone(){
    strokeWeight(1);
    stroke(0);
    fill(255);
    box(65, 10, 140);

    strokeWeight(3);
    stroke(255, 0, 0);
    line(0, -10, -30, 0, -10, -80);
    line(0, -10, -80, -10, -10, -60);
    line(0, -10, -80, 10, -10, -60);
}


public void accxyz(float x, float y, float z) {
   println("X " + x + " Y  " + y + "Z " + z  ); 
   
   x_ = x;
   y_ = y;
   z_ = z;
   
    if(!isGetFirst){
        curX = x;
        curY = y;
        curZ = z;
        isGetFirst = true;
    }else{
        preX = curX;
        preY = curY;
        preZ = curZ;
        curX = x;
        curY = y;
        curZ = z;
    }

    if(dist(curX, curY, curZ, preX, preY, preZ) < 1){
        return;    
    }

    //
    tdegreeX = constrain(y*-9, -90, 90);   

    transX = ttransX + x*-9;   
    
    if(abs(tdegreeX) < 80){    
        tdegreeZ = constrain(z*9 - 90, -180, 0);    
    }else{
        tdegreeZ = z*9;
    }
    
    
}


/* incoming osc message are forwarded to the oscEvent method. */
void oscEvent(OscMessage theOscMessage) {

    //check unknow OSC message from touchOSC app    
    if (theOscMessage.isPlugged()==false) {
        /* print the address pattern and the typetag of the received OscMessage */
        println("### received an osc message.");
        println("### addrpattern\t"+theOscMessage.addrPattern());
        println("### typetag\t"+theOscMessage.typetag());
    }
}



void sendValue(int movimiento, String tipoMensaje) {
  // Create a channel for the x coordinate
  OscMessage oscMess1 = new OscMessage(tipoMensaje);
  //oscMess1.add(movimiento);
  
  // Create a channel for the y coordinate
  //OscMessage oscMess2 = new OscMessage("/brightness");
  //oscMess2.add(y);
  
  // Send our data over to Unity!
  oscP5.send(oscMess1, myRemoteLocation); 
  println("SE ENVIA"+tipoMensaje);
  //oscP5.send(oscMess2, myRemoteLocation);
}

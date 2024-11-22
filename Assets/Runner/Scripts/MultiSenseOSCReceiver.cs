using UnityEngine;
using extOSC;

public class MultiSenseOSCReceiver : MonoBehaviour
{
    public int oscPort = 9000; // Puerto configurado en MultiSenseOSC

    public float accelX, accelY, accelZ;

    private void Start()
    {
        // Configurar el receptor OSC
        var receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = oscPort;

        // Asociar funciones a las direcciones OSC
        receiver.Bind("/multisense/accelerometer/x", OnReceiveX);
        receiver.Bind("/multisense/accelerometer/y", OnReceiveY);
        receiver.Bind("/multisense/accelerometer/z", OnReceiveZ);

        Debug.Log($"OSC Receiver configurado en el puerto {oscPort}");
    }

    private void OnReceiveX(OSCMessage message)
    {
        if (message.Values.Count > 0)
            accelX = message.Values[0].FloatValue;
    }

    private void OnReceiveY(OSCMessage message)
    {
        if (message.Values.Count > 0)
            accelY = message.Values[0].FloatValue;
    }

    private void OnReceiveZ(OSCMessage message)
    {
        if (message.Values.Count > 0)
            accelZ = message.Values[0].FloatValue;
    }

    private void OnGUI()
    {
        // Mostrar los valores en pantalla
        GUI.Label(new Rect(10, 10, 400, 20), $"Acelerómetro: X={accelX:F2}, Y={accelY:F2}, Z={accelZ:F2}");
    }
}
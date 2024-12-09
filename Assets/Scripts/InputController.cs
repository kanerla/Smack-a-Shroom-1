using UnityEngine;
using System.IO.Ports;

public class InputController : MonoBehaviour
{
    private SerialPort sp;
    [SerializeField] private GameController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = new SerialPort("COM3", 9600);
        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (sp != null && sp.IsOpen)
        {
            try
            {
                int command = sp.ReadByte();
                print("command: " + command);

                controller.mushrooms[command - 1].Smashed();
            }
            catch (System.Exception) {}
        }
    }

    void OnApplicationQuit()
    {
        if (sp != null && sp.IsOpen)
        {
            sp.Close();
        }
    }
}

using System.IO.Ports;

namespace twitch_bubble_api;

public static class SerialHelper
{
    public static bool TryWriteSeconds(string serialPortName, int seconds)
    {
        bool success = true;
        using var serialPort = new SerialPort(serialPortName, 9600)
        {
            Parity = Parity.None,
            DataBits = 8,
            StopBits = StopBits.One,
            Handshake = Handshake.None
        };
        
        try
        {
            serialPort.Open();
            int timespent = 0;
            while (timespent < 600) // buffer to allow command to fire
            {
                serialPort.WriteLine(seconds.ToString());
                timespent += 1;
            }
            

        }
        catch (Exception ex)
        {
            success = false;
        }
        finally
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }
        return success;
    }

}
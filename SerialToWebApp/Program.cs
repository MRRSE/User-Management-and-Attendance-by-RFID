using System;
using System.IO.Ports;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static SerialPort _serialPort;

    static async Task Main(string[] args)
    {
        _serialPort = new SerialPort("COM3", 9600); // COM پورت آردوینو - ممکنه متفاوت باشه!
        _serialPort.DataReceived += SerialDataReceived;
        _serialPort.Open();
        Console.WriteLine("در حال خواندن از پورت سریال...");
        Console.ReadLine(); // تا وقتی این رو نبندی برنامه باز می‌مونه
    }

    private static async void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var sp = (SerialPort)sender;
        string uid = sp.ReadLine().Trim(); // UID کارت

        Console.WriteLine("کد کارت دریافتی: " + uid);

        using var client = new HttpClient();
        var response = await client.PostAsync("http://localhost:5048/api/rfid",
            new StringContent($"\"{uid}\"", System.Text.Encoding.UTF8, "application/json"));

        Console.WriteLine($"ارسال به سرور: {response.StatusCode}");
    }
}

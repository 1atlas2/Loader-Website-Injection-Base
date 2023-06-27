using KeyAuth;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;



class Program
{


    public static api KeyAuthApp = new api(
           name: "cockhook",
           ownerid: "INfuG4HHSQ",
           secret: "0c1078065670d018e91dc5990cbd7f8f2556bd4b76679141d10092d4927b2c18",
           version: "1.0"
       );

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static void HideConsoleWindow()
    {
        IntPtr consoleWindowHandle = GetConsoleWindow();
        ShowWindow(consoleWindowHandle, SW_HIDE);
    }

    public static void ShowConsoleWindow()
    {
        IntPtr consoleWindowHandle = GetConsoleWindow();
        ShowWindow(consoleWindowHandle, SW_SHOW);
    }


    static void Main(string[] args)
    {
        

        //HideConsoleWindow();
        string filePath = @"C:\ImGuiExternal.exe";
        string previousText = string.Empty;
        string key;
        // Website-URL festlegen
        string url = "https://siresware.xyz/testing/command.php";
        string targetUrl = "https://siresware.xyz/testing/command1.php";
        string commandsFileName = "commands.txt";

        // Befehle festlegen
        string command1 = "Dx0001";
        string command2 = "Dx0002";
        string command3 = "Dx0003";
        string command4 = "Dx0004";
        string command5 = "Dx0005";
        string command6 = "Dx0006";
        string command7 = "ExFailed";
        string command8 = "0x0001";

        Console.Title = "Loader";
        Console.WriteLine("\nConnecting..");
        KeyAuthApp.init();
        if (!KeyAuthApp.response.success)
        {
            Console.WriteLine("Status: " + KeyAuthApp.response.message);
            Thread.Sleep(1500);
            Environment.Exit(0);
        }
        maincheck();

        void apilogin()
        {
            while (true)
            {
                // Den Text auf der Website überprüfen
                string currentText = GetTextFromWebsite();

                // Überprüfen, ob sich der Text geändert hat und mit "License" beginnt
                if (currentText.StartsWith("License") && currentText != previousText)
                {
                    previousText = currentText;

                    // Den vollständigen Text kopieren und "License" entfernen
                    string license = currentText.Replace("License", string.Empty).Trim();
                    LoginWithLicense(license);
                }

                // 5 Sekunden warten
                Thread.Sleep(5000);
            }
        }



        static string GetTextFromWebsite()
        {
            // URL der Website, auf der der Text liegt
            string url = "https://siresware.xyz/testing/command.php";

            using (WebClient client = new WebClient())
            {
                try
                {
                    // Den Text von der Website herunterladen
                    string text = client.DownloadString(url);
                    return text;
                }
                catch (Exception ex)
                {
                    // Fehler beim Herunterladen des Texts
                    Console.WriteLine("Error at getting the License: " + ex.Message);
                    return string.Empty;
                }
            }
        }

        void LoginWithLicense(string license)
        {

            Console.WriteLine("License: " + license);
            Console.Write("Validating the License.\n");
            key = license;
            KeyAuthApp.license(key);
            if (!KeyAuthApp.response.success)
            {
                Console.WriteLine("Status: " + KeyAuthApp.response.message);
                Thread.Sleep(2500);
                Environment.Exit(0);
            }
            Console.Write("Validated.\n");
            maincheck();

        }

       

        void maincheck()
        {
            // WebClient-Objekt erstellen
            using (WebClient client = new WebClient())
            {
                

                try
                {
                    while (true)
                    {
                        string currentText = GetTextFromWebsite();
                        bool found = false;
                        // Website-Inhalt abrufen
                        string content = client.DownloadString(url);

                        // Überprüfen, ob der Text "0x00021" vorhanden ist
                        if (content.Contains("Dx0003"))
                        {
                            // Befehl an die Ziel-Website senden und Response erhalten
                            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string response = client.UploadString(targetUrl, "text=" + command4);
                            Console.WriteLine("Sent succesfull the Command Dx0004. Game Found, Injecting. Response: " + response);
                        }

                        if (content.Contains("Dx0004"))
                        {
                            // Befehl an die Ziel-Website senden und Response erhalten
                            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string response = client.UploadString(targetUrl, "text=" + command5);
                            Console.WriteLine("Sent succesfull the Command Dx0005. Injected, closing the Connection and terminating in 30 seconds. Response: " + response);
                            Process.Start(filePath);


                        }



                        if (content.Contains("License"))
                        {
                            // Befehl an die Ziel-Website senden und Response erhalten
                            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string response = client.UploadString(targetUrl, "text=" + command6);

                            
                            
                        }

                        // Überprüfen, ob der Text "0x0002" vorhanden ist
                        if (content.Contains("Dx0006"))
                        {
                            // Befehl an die Ziel-Website senden und Response erhalten
                            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string response1 = client.UploadString(targetUrl, "text=" + command3);

                            Console.WriteLine("Sent succesfull the Command Dx0003. Starting now the Game. Response: " + response1);

                            try
                            {
                                Process.Start("notepad.exe");
                                Console.WriteLine("Started RainbowSix.");



                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error at starting the File: " + ex.Message);
                                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                string response5 = client.UploadString(targetUrl, "text=" + command7);
                            }


                        }

                        if (content.Contains("Dx0001"))
                        {
                            Console.Write("Sending a Request to the Website for Login.\n");
                            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string response1 = client.UploadString(targetUrl, "text=" + command2);
                            Console.Write("Sent the Login Request.\n");
                            apilogin();
                        }

                        // Überprüfen, ob der Text "0x0001" vorhanden ist
                        if (content.Contains("0x0001"))
                        {
                            // Befehl an die Ziel-Website senden und Response erhalten
                            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string response4 = client.UploadString(targetUrl, "text=" + command1);

                            Console.WriteLine("Connected with the Site. Response: " + response4);

                            // Einmal beepen
                            Console.Beep();
                        }

                        // Wartezeit vor der nächsten Überprüfung
                        System.Threading.Thread.Sleep(5000);
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    Console.WriteLine("Fehler beim Senden des Befehls: " + ex.Message);
                }
            }
        }

        
    }
}

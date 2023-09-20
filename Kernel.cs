using Cosmos.System.ScanMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;
using Cosmos.System;
using Cosmos.System.FileSystem;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace chal
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();
        protected override void BeforeRun()
        {
            
            SetKeyboardScanMap(new FR_Standard());
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            System.Console.Clear();
            System.Console.WriteLine("Welcome To *MeDOS* v:0.0.1 (BETA1)");
            File.WriteAllText(@"0:\login.txt", "F");

        }



        protected override void Run()
        {
            var login = File.ReadAllText(@"0:\login.txt");
            if (login == "F")
            {
                System.Console.Write("username: ");
                var username = System.Console.ReadLine();
                System.Console.Write("password: ");
                var password = System.Console.ReadLine();
                var uspas = File.ReadAllText(@"0:\logindt.txt").Split('.');
                if (username == uspas[1] && password == uspas[2])
                {
                    File.WriteAllText(@"0:\login.txt", "T");
                    System.Console.Clear();
                    System.Console.WriteLine("Welcome To *MeDOS* v:0.0.1 (BETA1)");

                }
                else System.Console.WriteLine("Wrong Password or username!!!!");
            }
            else
            {
                
                string[] list = { "help", "time", "sh", "clear", "beep", "cp", "dm", "notepad", "np", "mkd", "pc-info","df","calc" };
                System.Console.Write("- ");
                var input = System.Console.ReadLine();
                string[] substrings = input.Split(' ');
                if (list.Contains(input) || substrings[0] == "put" || substrings[0] == "mk")
                {
                    if (input == "help")
                    {
                        System.Console.WriteLine("Commands:");
                        System.Console.WriteLine("time: Get current time (h;m;s)");
                        System.Console.WriteLine("sh: Shut down the Pc");
                        System.Console.WriteLine("clear: Clear the console");
                        System.Console.WriteLine("put (your text): Print custom text on the console");
                        System.Console.WriteLine("beep: Generate a simple beep sound from the computer's speaker.");
                        System.Console.WriteLine("dm : A Local Disk Manager");
                        System.Console.WriteLine("notepad : Notepad by @MedCode");
                        System.Console.WriteLine("np : Notepad by @Invalid403");
                        System.Console.WriteLine("cp : Open Control Panel");
                        System.Console.WriteLine("df : Delete A file");
                        System.Console.WriteLine("calc : A Basic Calculator");
   
                    }
                    if (input == "calc")
                    {
                        System.Console.WriteLine("ad : addition\nsub: substraction\nmul: multiplication\ndiv : division");
                        System.Console.WriteLine("operator: ");
                        var op = System.Console.ReadLine();
                        if (op == "ad")
                        {
                            System.Console.Write("First number: ");
                            var f1 = System.Console.ReadLine();
                            System.Console.Write("Second number: ");
                            var f2 = System.Console.ReadLine();
                            System.Console.WriteLine($"Result: {float.Parse(f1) + float.Parse(f2)} ");


                        }
                        if (op == "sub")
                        {
                            System.Console.Write("First number: ");
                            var f1 = System.Console.ReadLine();
                            System.Console.Write("Second number: ");
                            var f2 = System.Console.ReadLine();
                            System.Console.WriteLine($"Result: {float.Parse(f1)- float.Parse(f2)} ");
                        }
                        if (op == "mul")
                        {
                            System.Console.Write("First number: ");
                            var f1 = System.Console.ReadLine();
                            System.Console.Write("Second number: ");
                            var f2 = System.Console.ReadLine();
                            System.Console.WriteLine($"Result: {float.Parse(f1) * float.Parse(f2)} ");
                        }
                        if (op == "div")
                        {
                            System.Console.Write("First number: ");
                            var f1 = System.Console.ReadLine();
                            System.Console.Write("Second number: ");
                            var f2 = System.Console.ReadLine();
                            System.Console.WriteLine($"Result: {float.Parse(f1) / float.Parse(f2)} ");
                        }
                    }

                    if (input == "time")
                    {
                        System.Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
                    }
                    if (input == "sh")
                    {
                        System.Console.WriteLine("shuting down....");
                        TimeSpan ts = new TimeSpan(0, 0, 1);
                        Thread.Sleep(ts);
                        Sys.Power.Shutdown();
                    }
                    if (input == "clear")
                    {
                        System.Console.Clear();
                    }
                    if (input == "beep")
                    {
                        System.Console.Beep(100, 200);
                    }
                    if (substrings[0] == "put")
                    {
                        if (substrings.Length >= 2)
                        {
                            foreach (string substring in substrings)
                            {
                                if (substring == "put")
                                {

                                }
                                else System.Console.Write(substring);
                            }
                        }
                        else System.Console.WriteLine("Error!");
                    }
                    if (input == "cp")
                    {
                        System.Console.WriteLine("Control Pannel:");
                        System.Console.WriteLine("1.change layout language\n-->(availble languges: FR-DE-US\n--->chl-(layout code)");
                        System.Console.WriteLine("2.PC INFO\n-->pc-info");
                    }
                    if (input == "dm")
                    {
                        var available_space = fs.GetAvailableFreeSpace(@"0:\");
                        System.Console.WriteLine("Available Free Space: " + available_space / 1000000 + " mb");
                        var fs_type = fs.GetFileSystemType(@"0:\");
                        System.Console.WriteLine("File System Type: " + fs_type);
                        var files_list = Directory.GetFiles(@"0:\");
                        foreach (var file in files_list)
                        {
                            System.Console.WriteLine(file);
                        }


                    }
                    if (substrings[0] == "mk" && substrings.Length == 2)
                    {
                        try
                        {
                            var file_stream = File.Create($@"0:\{substrings[1]}");

                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "notepad")
                    {
                        System.Console.ForegroundColor = ConsoleColor.Black;
                        System.Console.BackgroundColor = ConsoleColor.White;
                        System.Console.Clear();
                        System.Console.WriteLine("Welcome to M-NotePad (v: BETA-0.1)");
                        System.Console.WriteLine("commands: write, read, quit");
                        string[] notepadl = { "write", "read", "quit" };
                        while (true)
                        {
                            var ntinput = System.Console.ReadLine();
                            string[] nsubstrings = ntinput.Split(' ');
                            if (notepadl.Contains(ntinput) || nsubstrings[0] == "write" || nsubstrings[0] == "read")
                            {

                                if (nsubstrings[0] == "write" && nsubstrings.Length == 3 && nsubstrings[1] != "login.txt" && nsubstrings[1] != "logindt.txt")
                                {
                                    try
                                    {
                                        File.WriteAllText($@"0:\{nsubstrings[1]}", nsubstrings[2]);
                                    }
                                    catch (Exception e)
                                    {
                                        System.Console.WriteLine(e.ToString());
                                    }
                                }
                                if (nsubstrings[0] == "read" && nsubstrings.Length == 2 && nsubstrings[1] != "login.txt" && nsubstrings[1] != "logindt.txt")
                                {
                                    try
                                    {
                                        System.Console.WriteLine(File.ReadAllText($@"0:\{nsubstrings[1]}"));
                                    }
                                    catch (Exception e)
                                    {
                                        System.Console.WriteLine(e.ToString());
                                    }

                                }
                                if (ntinput == "quit")
                                {
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                    System.Console.BackgroundColor = ConsoleColor.Black;
                                    System.Console.Clear();
                                    System.Console.WriteLine("Welcome To *MeDOS* v:0.0.1 (BETA1)");
                                    break;
                                }
                            }
                            else System.Console.WriteLine("Unknown Command!");
                        }


                    }
                    if (input == "np")
                    {
                        System.Console.WriteLine("Welcome to NPad v0.1, here the commands : new, save, about, preview");
                        List<string> lines = new List<string>();
                        var textLine = "";
                        while (true)
                        {
                            textLine = System.Console.ReadLine();
                            string[] nsubstrings = textLine.Split(' ');
                            if (textLine == "save")
                            {
                                System.Console.ForegroundColor = ConsoleColor.Red;
                                var name = System.Console.ReadLine();
                                if (name != "login.txt" && name != "logindt.txt")
                                {
                                    var file = File.CreateText(@"0:\" + name + ".txt");
                                    for (int idx = 0; idx < lines.Count; idx++)
                                    {
                                        file.WriteLine(lines[idx]);
                                        System.Console.ForegroundColor = ConsoleColor.Green;
                                        System.Console.WriteLine($"Saved Line number : {idx}");
                                    }
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                    System.Console.WriteLine("Saved Succesfully");
                                    file.Dispose();
                                }
                                else System.Console.WriteLine("Permission not garented");

                            }
                            else if (textLine == "about")
                            {
                                System.Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine("---> NPad By @Invalid403, CloneDOS OS");
                                System.Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (textLine == "preview")
                            {
                                System.Console.ForegroundColor = ConsoleColor.Yellow;
                                System.Console.WriteLine("---> This is preview of what you typed");
                                System.Console.ForegroundColor = ConsoleColor.White;
                                foreach (string line in lines)
                                {
                                    System.Console.WriteLine(line);
                                }
                                System.Console.ForegroundColor = ConsoleColor.Yellow;
                                System.Console.WriteLine("<--- Preview Ended !");
                                System.Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (textLine == "new")
                            {
                                System.Console.ForegroundColor = ConsoleColor.Blue;
                                System.Console.WriteLine("---> New File is created !");
                                lines.Clear();
                                System.Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (textLine == "quit")
                            {
                                System.Console.Clear();
                                System.Console.WriteLine("Welcome To *MeDOS* v:0.0.1 (BETA1)");
                                break;
                            }

                            else if (nsubstrings[0] == "read" && nsubstrings[1] != "login.txt" && nsubstrings[1] != "logindt.txt")
                            {
                                try
                                {
                                    System.Console.WriteLine(File.ReadAllText($@"0:\{nsubstrings[1]}"));
                                }
                                catch (Exception e)
                                {
                                    System.Console.WriteLine(e.ToString());
                                }
                            }
                            else
                            {
                                lines.Add(textLine);
                            }

                        }
                    }
                    if (input == "pc-info")
                    {
                        System.Console.WriteLine("CPU NAME: ", Cosmos.Core.CPU.EstimateCPUSpeedFromName);
                        var available_space = fs.GetAvailableFreeSpace(@"0:\");
                        System.Console.WriteLine("Available Free Space: " + available_space);
                        var fs_type = fs.GetFileSystemType(@"0:\");
                        System.Console.WriteLine("File System Type: " + fs_type);

                    }
                    if (input == "df")
                    {
                        try
                        {
                            System.Console.Write("file path: ");
                            var pathname = System.Console.ReadLine();
                            File.Delete($@"0:\{pathname}");
                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine(e.ToString());
                        }
                    }

                }
                else System.Console.WriteLine("Unknown Command! use help for assist!!");

            }
        }


            
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Permissions;

namespace NexusCsvApp
{
    class Program
    {

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static void Main(string[] args)
        {
            Console.WriteLine("----Nexus Limited----");
            Console.WriteLine(".csv monitoring application." + Environment.NewLine + "A folder will be monitored for new csv files and any new csv file will be displayed.");
            Console.WriteLine(Environment.NewLine + "Developed by Mrs Suparna 'Sue' Debnath" + Environment.NewLine);

            Console.WriteLine(string.Format("Please enter a folder path to monitor.. {0}(Enter q to close){0}", Environment.NewLine));

            string strUserInput;
            do
            {
                strUserInput = Console.ReadLine();
                if (strUserInput != null)
                {
                    if (System.IO.Directory.Exists(strUserInput))
                    {
                        Console.WriteLine(string.Format("Monitoring folder ({0})...", strUserInput));
                        MonitorFolder(strUserInput);
                    }
                    else if (strUserInput.Equals("q"))
                    {
                        Environment.Exit(0);
                    }
                    else
                        Console.WriteLine(string.Format("Folder doesn't exist ({0})! Please try again..", strUserInput));
                }
            } while (strUserInput != null);

        }

        private static void MonitorFolder(string folderFullPath)
        {
            System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher(folderFullPath, "*.csv");
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            watcher.Created += new FileSystemEventHandler(OnChanged);
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                List<string> lines = File.ReadAllLines(e.FullPath, Encoding.UTF8).ToList();
                foreach (var line in lines)
                {
                    foreach (var item in line.Split(','))
                    {
                        Console.Write(lines.IndexOf(line) == 0 ? "[" + item + "]" : item);
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine + "Oops...... something went horribly wrong! " + Environment.NewLine);
                Console.WriteLine(ex.Message);
            }
        }
    }
}

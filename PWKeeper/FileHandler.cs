using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PWKeeper
{
    internal class FileHandler
    {
        public enum ALGO
        {
            DECODE = 0,
            ENCODE = 1
        }
        public ALGO TODO { get; set; }
        public string Input { get; set; }
        public string Path { get; set; }
        public FileHandler(ALGO TODO, string Path)
        {
            Input = "";
            this.TODO = TODO;
            this.Path = Path;
        }
        public void WriteAsync()
        {
            File.WriteAllText(@".\Output.txt", Input); //you will see file in debug folder, you can change launchSettings as needed.
        }
        public void Read()
        {
            try
            {
                Input = File.ReadAllText(Path);
                //Console.Write(Input);
                //Console.WriteLine();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

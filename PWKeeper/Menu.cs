using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWKeeper.Algorithms;

namespace PWKeeper
{
    internal class Menu
    {
        private Algorithms.IAlgorithm algorithm;
        private FileHandler fileHandler;
        public Menu(IDictionary<string,string> args)
        {
            Console.WriteLine("==================================");
            Console.WriteLine(" Welcome to Password-Keeper Tool. ");
            Console.WriteLine("==================================");
            if(args.Count > 0)
            {
                MenuNav(args);
            }
        }

        public async void MenuNav(IDictionary<string, string> args)
        {
            foreach ((string key, string value) in args)
            {

                switch (key)
                {
                    case "--h":
                    case "--help":
                        switch(value)
                        {
                            case "commands":
                                Console.WriteLine("you typed for help, please use only combination of commands below:");
                                Console.WriteLine("--encode <path> --algo <algorithm name>");
                                Console.WriteLine("--decode <path> --algo <algorithm name>");
                                Console.WriteLine("be aware if you use a path were program have no access, you need to run as administrator");
                                Console.WriteLine("please notice, you should use only files with text format like: *.txt, *.html, *.json, *.xml, etc.");
                                break;
                            default:
                                HelpNotification();
                                break;
                        }
                        break;
                    case "--e":
                    case "--encrypt":
                        fileHandler = new FileHandler(FileHandler.ALGO.ENCODE, value);
                        break;
                    case "--d":
                    case "--decrypt":
                        fileHandler = new FileHandler(FileHandler.ALGO.DECODE, value);
                        break;
                    case "--a":
                    case "--algo":
                        Console.WriteLine("Type your secret password:");
                        Console.WriteLine();
                        string secret = Console.ReadLine();
                        if((secret != null) && (secret != string.Empty))
                        switch(value)
                        {
                            case "lpwka":
                                algorithm = new LookyAlgo(secret);
                                break;
                            case "exampleName"://you can add any new algo over here
                                algorithm = new Example();
                                //your code here
                                break;
                            default:
                                HelpNotification();
                                return;
                        }
                        break;
                    default:
                        HelpNotification();
                        return;
                }
            }
            if((algorithm != null) && (fileHandler != null))
            {
                fileHandler.Read();
                if(fileHandler.TODO == FileHandler.ALGO.ENCODE)
                {
                    fileHandler.Input = await algorithm.Encode(fileHandler.Input);
                } else if(fileHandler.TODO == FileHandler.ALGO.DECODE)
                {
                    fileHandler.Input = await algorithm.Decode(fileHandler.Input);
                } else
                {
                    HelpNotification();
                }
                fileHandler.WriteAsync();
                Console.WriteLine("success");
                Console.WriteLine();
            } else
            {
                HelpNotification();
            }
        }
        private void HelpNotification()
        {
            Console.WriteLine("error occured");
            Console.WriteLine("=============================================================================");
            Console.WriteLine(" type \"--h commands\" or \"--help commands\" to see all available commands. ");
            Console.WriteLine("=============================================================================");
        }
    }
}

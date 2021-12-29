using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWKeeper
{
    internal class CommandHandler
    {
        public IDictionary<string, string> Commands { get; private set; }
        public CommandHandler()
        {
            Commands = new Dictionary<string, string>();
        }

        public void SetCommands(string[] args)
        {
            List<string> key = new();
            List<string> value = new();
            List<string> list = args.ToList();
            foreach (var arg in list.Select((value,i)=>(value,i)))
            {
                
                if(arg.i% 2 == 0)
                {
                    key.Add(arg.value);
                } else
                {
                    value.Add(arg.value);
                }
            }
            try
            {
                for (int i = 0; i < key.Count; i++)
                {
                    Commands.Add(key[i], value[i]);
                }
            } catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

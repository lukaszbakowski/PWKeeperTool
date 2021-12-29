
/*
* @author Lukasz Bakowski
*
* @date - 27.12.2021
*/

using PWKeeper;


if(args.Length < 2 || args == null)
{
    Console.WriteLine("no command defined, use \"--h commands\" to see all commands available");
    return;
}

CommandHandler CMDHandle = new();
CMDHandle.SetCommands(args);


foreach (var dict in CMDHandle.Commands)
{
    Console.WriteLine(dict.Key);
    Console.WriteLine(dict.Value);
}

Menu menu = new(CMDHandle.Commands);





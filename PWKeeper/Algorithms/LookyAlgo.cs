﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

/*
* @author Lukasz Bakowski
*
* @date - 27.12.2021
*/


namespace PWKeeper.Algorithms
{
    public class LookyAlgo : AlgorithmBase
    {

        
        private readonly string Chars = " 1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM\r\n!@#$%^&*()-_=+[]{}\\|;:\'\",<.>/?\t";
        public LookyAlgo(string password) : base(password)
        {
            AlgorithmName = "lpwka"; //Lukas (or Looky) PassWord Keeper Algorithm, use "--algo lpwka"
        }
        public override async Task<string> Decode(string input)
        {
            StringBuilder Output = new StringBuilder();
            string[] inputs = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string lastHash = "SomeSecretSaltOverHereYouCanChangeItAsNeededInYourPrivateRepo";
            int counter = 0;
            foreach (string s in inputs)
            {
                lastHash = Password + lastHash;
                lastHash = HashFunction(lastHash);
                Console.WriteLine("checking for: " + s);
                foreach (var c in Chars)
                {
                    string toHash = Password + lastHash + c;
                    string checkForHash = HashFunction(toHash);
                    //Console.WriteLine(checkForHash +" ==? "+ s);
                    if(checkForHash == s)
                    {
                        lastHash = checkForHash;
                        Output.Append(c);
                        //Console.WriteLine(lastHash +" dla: "+c);
                        break;
                    }
                }
                counter++;
            }
            Console.WriteLine(counter);
            if (Output.Length == 0) //make a fake output if password is uncorrect
            {
                Random random = new Random();
                for(int i = 0; i < inputs.Length; i++)
                {
                    Output.Append(new string(Enumerable.Repeat(Chars, 1)
                        .Select(s => s[random.Next(s.Length)]).ToArray()));
                }
            }
            return await Task.FromResult(Output.ToString());
        }
        
        public override async Task<string> Encode(string input)
        {
            StringBuilder Output = new StringBuilder();
            string lastHash = "SomeSecretSaltOverHereYouCanChangeItAsNeededInYourPrivateRepo";
            int counter = 0;
            foreach (var c in input)
            {
                 lastHash = Password + lastHash;
                 lastHash = HashFunction(lastHash);
                 string toHash = Password + lastHash + c;
                 //Console.WriteLine(toHash);
                 lastHash = HashFunction(toHash);
                 Output.Append(lastHash).Append("\n");
                counter++;
            }
            string output = Output.ToString();
            
            Console.WriteLine(output);
            Console.WriteLine(counter);
            return await Task.FromResult(output);
        }
        private string HashFunction(string toHash)
        {
            using (SHA512 mySHA512 = SHA512.Create())
            {
                byte[] charByte = Encoding.UTF8.GetBytes(toHash);
                byte[] buffer = mySHA512.ComputeHash(charByte);
                return BitConverter.ToString(buffer).Replace("-", String.Empty);
            }
        }
    }
}

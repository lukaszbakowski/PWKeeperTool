using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWKeeper.Algorithms
{
    internal class AlgorithmBase : IAlgorithm
    {
        public string AlgorithmName { get; protected set; }
        protected readonly StringBuilder Output;
        protected string Password;
        public AlgorithmBase(string password)
        {
            AlgorithmName = "exampleName";
            Output = new();
            Password = password;
        }
        public virtual string Decode(string input)
        {
            return Output.ToString();
        }
        public virtual string Encode(string input)
        {
            return Output.ToString();
        }
    }
}

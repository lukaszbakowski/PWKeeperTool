using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWKeeper.Algorithms
{
    internal class Example : IAlgorithm //or extended AlgorithmBase
    {
        public string AlgorithmName { get; private set; }
        public Example()
        {
            AlgorithmName = "exampleName"; //use with command "--alg exampleName" or you can skip this command so it will pickup a default algo automaticly (lpwka in current version)
        }
        public Task<string> Encode(string input)
        {
            //use your code here to encrypt your data
            return Task.FromResult(input);
        }
        public Task<string> Decode(string input)
        {
            //use your code here decrypt your data
            return Task.FromResult(input);
        }
    }
}

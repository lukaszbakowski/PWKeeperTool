using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWKeeper.Algorithms
{
    public class AlgorithmBase : IAlgorithm
    {
        public string AlgorithmName { get; protected set; }
        protected string Password;
        public AlgorithmBase(string password)
        {
            AlgorithmName = "exampleName";
            Password = password;
        }
        public virtual async Task<string> Decode(string input)
        {
            return await Task.FromResult(input.ToString());
        }
        public virtual async Task<string> Encode(string input)
        {
            return await Task.FromResult(input.ToString());
        }
    }
}

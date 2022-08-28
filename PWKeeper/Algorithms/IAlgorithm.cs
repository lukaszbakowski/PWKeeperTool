using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWKeeper.Algorithms
{
    public interface IAlgorithm
    {
        string AlgorithmName { get; } //use with command "--alg <AlgorithmName>" or you can skip this command so it will pickup a default algo automaticly (lpwka in current version)
        string Encode(string input); // "--encrypt <path>"
        string Decode(string input); // "--decrypt <path>"
    }
}

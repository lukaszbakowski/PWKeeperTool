using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWKeeper.Algorithms;
using PWKeeper.Core.Models;
using System.IO;
using System.Text.Json;

namespace PWKeeper.Core
{
    public class StorageBuilder
    {
        private IAlgorithm? algorithm;
        private string Path = @".\wwwroot\data\";
        private string? fileContent;
        public List<StorageItemModel> GetStorage { get; private set; }
        public StorageBuilder() {
            GetStorage = new();
        }

        public StorageBuilder SetStorage(string login)
        {
            Path = Path + login + ".txt";
            if(File.Exists(Path))
            {
                fileContent = File.ReadAllText(Path);
                Console.WriteLine("file exist");
            }
            return this;
        }
        public StorageBuilder SetAlgo(IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            if(fileContent != null)
            {
                fileContent = this.algorithm.Decode(fileContent);
            } else
            {
                string storageOutput = JsonSerializer.Serialize(GetStorage);
                fileContent = storageOutput;
                storageOutput = this.algorithm.Encode(storageOutput);
                File.WriteAllText(Path, storageOutput);
                Console.WriteLine("file not exist, creating new one");
            }
            return this;
        }
        public void Build()
        {
            if (fileContent != null)
            {
                GetStorage = JsonSerializer.Deserialize<List<StorageItemModel>>(fileContent);
            }
            Console.WriteLine("building storage");
        }
        public void AddItem(StorageItemModel item)
        {

        }
        public void RemoveItem(StorageItemModel item)
        {
            
        }
        public void UpdateItem(StorageItemModel item)
        {

        }
    }
}

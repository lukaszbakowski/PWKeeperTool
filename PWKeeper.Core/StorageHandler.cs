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
    public class StorageHandler
    {
        private IAlgorithm? algorithm;
        private string Path = @".\wwwroot\data\";
        public List<StorageItemModel> GetStorage { get; private set; }
        public StorageHandler() {
            GetStorage = new();
        }
        public async Task<List<StorageItemModel>> Build(string login, IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            Path = Path + login + ".txt";

            try
            {
                if (File.Exists(Path))
                {
                    string fileContent = await File.ReadAllTextAsync(Path);

                    if (fileContent != string.Empty && fileContent != null)
                    {
                        fileContent = await Decode(fileContent);
                        GetStorage = JsonSerializer.Deserialize<List<StorageItemModel>>(fileContent);
                    }
                }
                else
                {
                    UpdateStorageFile();
                }
            }
            catch (Exception ex) {
                return GetStorage;
            }
            return GetStorage;
        }
        private async Task<string> Decode(string input)
        {
            if(algorithm != null)
            {
                return await algorithm.Decode(input);
            }
            return string.Empty;
        }

        private async void UpdateStorageFile()
        {
            try
            {
                if (algorithm != null)
                {
                    string input = JsonSerializer.Serialize(GetStorage);
                    string output = await algorithm.Encode(input);
                    await File.WriteAllTextAsync(Path, output);
                }
            } catch(Exception ex) { }
        }

        public void AddItem(StorageItemModel item)
        {
            GetStorage.Add(item);
            UpdateStorageFile();
        }
        public void RemoveItem(StorageItemModel item)
        {
            
        }
        public void UpdateItem(StorageItemModel item)
        {

        }
    }
}

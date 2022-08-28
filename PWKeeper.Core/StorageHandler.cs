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
                    await UpdateStorageFile();
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

        private async Task<bool> UpdateStorageFile()
        {
            try
            {
                if (algorithm != null)
                {
                    string input = JsonSerializer.Serialize(GetStorage);
                    string output = await algorithm.Encode(input);
                    await File.WriteAllTextAsync(Path, output);
                }
            } catch(Exception ex) { return false; }
            return true;
        }

        public async Task<bool> AddItemAsync(StorageItemModel item)
        {
            GetStorage.Add(item);
            await UpdateStorageFile();
            return true;
        }
        public async Task<bool> RemoveItemAsync(StorageItemModel item)
        {
            GetStorage.Remove(item);
            await UpdateStorageFile();
            return true;
        }
        public async Task<bool> UpdateItemAsync(int index, StorageItemModel item)
        {
            GetStorage.RemoveAt(index);
            GetStorage.Add(item);
            await UpdateStorageFile();
            return true;
        }
    }
}

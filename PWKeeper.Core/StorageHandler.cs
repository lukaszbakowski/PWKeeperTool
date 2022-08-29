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
        private string Path { get
            {
                return _path;
            } set
            {
                var output = new StringBuilder();
                output.Append(@".\wwwroot\data\");
                output.Append(value);
                output.Append(".txt");
                _path = output.ToString();
            } 
        }
        private string _path = string.Empty;
        private string Backup { get
            {
                DateTime dateTime = DateTime.Now;
                var output = new StringBuilder();
                output.Append(@".\wwwroot\backup\");
                output.Append(dateTime.ToString("yyyy.MM.dd.HH.mm.ss"));
                output.Append(".txt");
                return output.ToString();
            }
        }
        public List<StorageItemModel> GetStorage { get; private set; }
        public string ExceptionMessage { get; private set; }
        public StorageHandler() {
            GetStorage = new();
        }
        public async Task<bool> Build(string login, IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            Path = login;

            try
            {
                if(login == string.Empty || login == null || algorithm == null)
                {
                    throw new ArgumentException("bad input login or algo");
                }
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
                ExceptionMessage = "singIn failed: " + ex.Message;
                return false;
            }
            ExceptionMessage = "sueccessfully loged in";
            return true;
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
                    await File.WriteAllTextAsync(Backup, output);
                }
            } catch(Exception ex) {
                ExceptionMessage = ex.Message;
                return false; 
            }
            return true;
        }

        public async Task<bool> AddItemAsync(StorageItemModel item)
        {
            try
            {
                GetStorage.Add(item);
                await UpdateStorageFile();
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
                return false;
            }
            return true;
        }
        public async Task<bool> RemoveItemAsync(StorageItemModel item)
        {
            try
            {
                GetStorage.Remove(item);
                await UpdateStorageFile();
            } catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateItemAsync(int index, StorageItemModel item)
        {
            try
            {
                GetStorage.RemoveAt(index);
                GetStorage.Add(item);
                await UpdateStorageFile();
            } catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
                return false;
            }
            return true;
        }
    }
}

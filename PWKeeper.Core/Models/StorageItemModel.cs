using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWKeeper.Core.Models
{
    /// <summary>
    /// <param name="Name">destination of where used login and password like example.com</param>
    /// <param name="Description">any extra info if needed</param>
    /// </summary>
    public class StorageItemModel
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
    }
}

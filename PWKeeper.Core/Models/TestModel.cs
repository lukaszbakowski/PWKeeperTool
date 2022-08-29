using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWKeeper.Core.Models
{
    /// <summary>
    /// model for testing appsettings 
    /// </summary>
    public class TestModel
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Secret { get; set; } = "SomeSecretSaltOverHereYouCanChangeItAsNeededInYourPrivateRepo";
    }
}

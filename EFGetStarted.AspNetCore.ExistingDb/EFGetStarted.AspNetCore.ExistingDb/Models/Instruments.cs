using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class Instruments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Qualifier { get; set; }
    }
}

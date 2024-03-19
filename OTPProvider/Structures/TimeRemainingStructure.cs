using OutSystems.ExternalLibraries.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPProvider.Structures
{
    [OSStructure]
    public struct TimeRemainingStructure
    {
        public int TimeRemaining { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}

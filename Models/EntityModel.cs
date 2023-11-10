using Microsoft.EntityFrameworkCore;

namespace BabyNI.Models
{
    [Keyless]

    public class EntityModel
    {
        public int DATETIME_KEY { get; set; }
        public int TIME { get; set; }
        public string? NETWORK_SID { get; set; }
        public string? NEALIAS { get; set; }
        public string? NETYPE { get; set; }
        public int RSL_INPUT_POWER { get; set; }
        public int MAX_RX_LEVEL { get; set; }
        public int RSL_DEVIATION { get; set; }


        // Add Optional Data Annotations or Fluent API
    }

}
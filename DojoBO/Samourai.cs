using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DojoBO
{
    public class Samourai : DojoEntity
    {
        public int Force { get; set; }
        public string Nom { get; set; }
        public virtual Arme Arme { get; set; }
        [DisplayName("Arts Martiaux maîtirisés")]
        public virtual List<ArtMartial> ArtMartiaux { get; set; }

        public int Potentiel { get; set; }

    }
}

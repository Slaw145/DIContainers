using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainers
{
    public class Thief : ICharacter
    {
        private int _strength = 10;
        public int Strength { get => _strength; set => _strength = value; }

        private int _stamina = 12;
        public int Stamina { get => _stamina; set => _stamina = value; }

        private int _additionalStrength = 10;
        public int AdditionalStrength { get => _additionalStrength; set => _additionalStrength = value; }

        private int _additionalStamina = 10;
        public int AdditionalStamina { get => _additionalStamina; set => AdditionalStrength = value; }
    }
}

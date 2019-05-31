using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainers
{
    public interface ICharacterSkillPoints
    {
        ICharacter CreateCharacter(ICharacter character);
        ICharacter GiveOutSkillPoints(ICharacter character);
        void CountNumberOfCalling();
    }

    public class CharacterSkillPoints : ICharacterSkillPoints
    {
        public int instances;

        public ICharacter CreateCharacter(ICharacter character)
        {
            return GiveOutSkillPoints(character);
        }

        public ICharacter GiveOutSkillPoints(ICharacter character)
        {
            character.Strength += character.AdditionalStrength;
            character.Stamina += character.AdditionalStamina;

            return character;
        }

        public void CountNumberOfCalling()
        {
            instances += 1;
            Console.WriteLine("Number of calling this method in object " + this.GetType().Name + " : " + instances);
        }
    }
}

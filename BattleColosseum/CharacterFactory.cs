using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public class CharacterFactory
    {
        public static Character createNewRandomCharacter()
        {
            return new Character(ClassData.getRandomClass());
        }

        public static Character createNewRandomCharacter(Class className)
        {
            return new Character(className);
        }
    }
}

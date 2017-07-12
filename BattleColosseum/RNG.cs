using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public class RNG
    {
        private static RNG instance;
        private Random randomGenerator;

        private RNG() {
            randomGenerator = new Random();
        }

        public static RNG getInstance()
        {
            if (instance == null)
            {
                instance = new RNG();
            }

            return instance;
        }

        public byte roll()
        {
            return (byte)randomGenerator.Next(100); //0-99
        }

        public byte[] rollStats()
        {
            byte[] rolls = new byte[7];

            for(int i=0; i< rolls.Length; i++)
            {
                rolls[i] = roll();
            }

            return rolls;
        }

    }
}

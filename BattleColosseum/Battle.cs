using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public class Battle
    {
        Character player1 { get; }
        Character player2 { get; }
        IBattleShow show;

        public Battle(Character player1, Character player2, ShowType show)
        {
            this.player1 = player1;
            this.player2 = player2;
            //this.show = BattleShowFactory.getInstance().makeShow(show, player1, player2); - todo
        }

        //Another constructor with the Interface


    }
}

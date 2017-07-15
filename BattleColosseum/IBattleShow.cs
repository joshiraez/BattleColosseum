using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public interface IBattleShow
    {
        void showMessage();
        void attack(AttackType attack, Battler num, byte damage);
        //void ability(Ability ability)
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    class Test
    {
        private enum STATS:int
        {
            HP = 0,
            POW = 1,
            SKL = 2,
            SPD = 3,
            LCK = 4,
            DEF = 5,
            RES = 6
        }

        private const byte STATS_SIZE = 7;
        private static readonly byte[] STATS_ROLLS_BASE = { 50, 30, 30, 20, 20, 30, 20 };
        private const int STATS_EXTRA_RANDOM_GROWTH = 200;
        private const int STATS_INITIAL_LEVELS_STATS = 10;

        private static Random rng = new Random();
        private static byte[] stats_rolls = new byte[STATS_SIZE];

        static void Main(string[] args)
        {

            byte[] stats_player1 = new byte[STATS_SIZE];
            byte[] stats_player2 = new byte[STATS_SIZE];

            byte[] stats_growths_player1 = new byte[STATS_SIZE];
            byte[] stats_growths_player2 = new byte[STATS_SIZE];
            float sum_aux;
            int growth_aux;

            STATS_ROLLS_BASE.CopyTo(stats_growths_player1, 0);
            STATS_ROLLS_BASE.CopyTo(stats_growths_player2, 0);

            sum_aux=rollStats();

            for (int stats_rolling = 0; stats_rolling < STATS_SIZE; stats_rolling++)
            {
                stats_growths_player1[stats_rolling] += (byte)((stats_rolls[stats_rolling] * STATS_EXTRA_RANDOM_GROWTH / sum_aux)+1); 
            }

            sum_aux = rollStats();
            for (int stats_rolling = 0; stats_rolling < STATS_SIZE; stats_rolling++)
            {
                stats_growths_player2[stats_rolling] += (byte)((stats_rolls[stats_rolling] * STATS_EXTRA_RANDOM_GROWTH / sum_aux) + 1);
            }


            for(int initial_levels=1; initial_levels<=STATS_INITIAL_LEVELS_STATS; initial_levels++) { 
                rollStats();

                for (int stats_rolling = 0; stats_rolling < STATS_SIZE; stats_rolling++)
                {
                    growth_aux = stats_growths_player1[stats_rolling];
                    growth_aux -= rng.Next(101) - 1;
                    while (growth_aux > 1)
                    {
                        stats_player1[stats_rolling]++;
                        growth_aux -= rng.Next(101) - 1;
                    }
                }

                rollStats();

                for (int stats_rolling = 0; stats_rolling < STATS_SIZE; stats_rolling++)
                {
                    growth_aux = stats_growths_player2[stats_rolling];
                    growth_aux -= rng.Next(101) - 1;
                    while (growth_aux > 1)
                    {
                        stats_player2[stats_rolling]++;
                        growth_aux -= rng.Next(101) - 1;
                    }
                }
            }

            stats_player1[(int)STATS.HP] += 10;
            stats_player2[(int)STATS.HP] += 10;

            stats_player1[(int)STATS.POW] += 5;
            stats_player2[(int)STATS.POW] += 5;

            ScreenDraw();
            PrintPlayer1Stats(stats_player1, stats_growths_player1);
            PrintPlayer2Stats(stats_player2, stats_growths_player2);

            PrintBattleScene(stats_player1, stats_player2);

            Console.Read();

            StartBattle(stats_player1, stats_player2);


        }

        public static int rollStats ()
        {
            int sum = 0;
            for (int stats_rolling = 0; stats_rolling < STATS_SIZE; stats_rolling++)
            {
                stats_rolls[stats_rolling] = (byte)rng.Next(101);
                sum += stats_rolls[stats_rolling];
            }

            return sum;
        }

        private static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        private static void ScreenDraw()
        {
            Console.Clear();
            WriteAt("PLAYER1", 1, 1);
            WriteAt("-o-", 1, 3);
            WriteAt("STR: ", 1, 5);
            WriteAt("SKL: ", 1, 6);
            WriteAt("SPD: ", 1, 7);
            WriteAt("LCK: ", 1, 8);
            WriteAt("DEF: ", 1, 9);
            WriteAt("RES: ", 1, 10);

            WriteAt("PLAYER2", Console.BufferWidth-("PLAYER2".Length)-1, 1);
            WriteAt("-o-", Console.BufferWidth - 4, 3);
            WriteAt(":STR ", Console.BufferWidth - 5, 5);
            WriteAt(":SKL ", Console.BufferWidth - 5, 6);
            WriteAt(":SPD ", Console.BufferWidth - 5, 7);
            WriteAt(":LCK ", Console.BufferWidth - 5, 8);
            WriteAt(":DEF ", Console.BufferWidth - 5, 9);
            WriteAt(":RES ", Console.BufferWidth - 5, 10);
        }

        private static void PrintPlayer1Stats(byte[] stats, byte[] growths)
        {
            for (int stats_reading = 1; stats_reading < STATS_SIZE; stats_reading++)
            {
                WriteAt(""+stats[stats_reading], 6, 5 - 1 + stats_reading);
                WriteAt("(" + growths[stats_reading]+")", 9, 5 - 1 + stats_reading);
            }
        }

        private static void PrintPlayer2Stats(byte[] stats, byte[] growths)
        {
            for (int stats_reading = 1; stats_reading < STATS_SIZE; stats_reading++)
            {
                WriteAt("" + stats[stats_reading], Console.BufferWidth - 7, 5 - 1 + stats_reading);
                WriteAt("(" + growths[stats_reading] + ")", Console.BufferWidth - 12, 5 - 1 + stats_reading);
            }
        }
        
        private static void PrintBattleScene(byte[] stats_player1, byte[] stats_player2)
        {
            char[,] pantalla = new char[9, 44];

            pantalla[0, 0] = '╔';
            pantalla[0, 43] = '╗';
            pantalla[8, 0] = '╚';
            pantalla[8, 43] = '╝';

            for(int col=1; col<=42; col++)
            {
                pantalla[0, col] = '═';
                pantalla[8, col] = '═';
            }

            for (int fil = 1; fil <= 7; fil++)
            {
                pantalla[fil, 0] = '║';
                pantalla[fil, 43] = '║';
            }

            char[] data_char_array = "HP:  /   DMG:    HIT:   CRT:   ".ToCharArray();

            for (int char_space = 0; char_space < data_char_array.Length; char_space++)
            {
                pantalla[6,2+char_space] = data_char_array[char_space];
            }

            int dmg;
            int hit;
            int crt;

            for (int hp_space=1; hp_space<=stats_player1[(int)STATS.HP]%40; hp_space++)
            {
                pantalla[2, hp_space + 2 - 1] = '▓';
            }
            for (int hp_space = 1; hp_space <= Math.Min(stats_player1[(int)STATS.HP] - 40, 40); hp_space++)
            {
                pantalla[4, hp_space + 2 - 1] = '▓';
            }

            dmg = Math.Max(stats_player1[(int)STATS.POW] - stats_player2[(int)STATS.DEF], 1);
            hit = Math.Min(Math.Max(80+stats_player1[(int)STATS.SKL]*2+stats_player1[(int)STATS.LCK]-stats_player2[(int)STATS.SPD]*2-stats_player2[(int)STATS.LCK],1),99);
            crt = Math.Min(Math.Max(stats_player1[(int)STATS.SKL]-stats_player2[(int)STATS.LCK],1),99);

            pantalla[6, 5] = (char)('0' + stats_player1[(int)STATS.HP] / 10);
            pantalla[6, 8] = (char)('0' + stats_player1[(int)STATS.HP] / 10);

            pantalla[6, 6] = (char)('0' + stats_player1[(int)STATS.HP] % 10);
            pantalla[6, 9] = (char)('0' + stats_player1[(int)STATS.HP] % 10);

            pantalla[6, 15] = (char)('0' + dmg / 10);
            pantalla[6, 16] = (char)('0' + dmg % 10);

            pantalla[6, 22] = (char)('0' + hit / 10);
            pantalla[6, 23] = (char)('0' + hit % 10);

            pantalla[6, 29] = (char)('0' + crt / 10);
            pantalla[6, 30] = (char)('0' + crt % 10);

            for(int fil=0; fil<9; fil++)
            {
                for(int col=0; col<44; col++)
                {
                    WriteAt(pantalla[fil, col].ToString(), 1 + col, 30 - 10 + fil);
                }
            }
            //Esto esta horrible :))) pero quiero terminar la frikada.
            pantalla = new char[9, 44];

            pantalla[0, 0] = '╔';
            pantalla[0, 43] = '╗';
            pantalla[8, 0] = '╚';
            pantalla[8, 43] = '╝';

            for (int col = 1; col <= 42; col++)
            {
                pantalla[0, col] = '═';
                pantalla[8, col] = '═';
            }

            for (int fil = 1; fil <= 7; fil++)
            {
                pantalla[fil, 0] = '║';
                pantalla[fil, 43] = '║';
            }

            data_char_array = "HP:  /   DMG:    HIT:   CRT:   ".ToCharArray();

            for (int char_space = 0; char_space < data_char_array.Length; char_space++)
            {
                pantalla[6, 2 + char_space] = data_char_array[char_space];
            }

            for (int hp_space = 1; hp_space <= stats_player2[(int)STATS.HP] % 40; hp_space++)
            {
                pantalla[2, hp_space + 2 - 1] = '▓';
            }
            for (int hp_space = 1; hp_space <= Math.Min(stats_player2[(int)STATS.HP] - 40, 40); hp_space++)
            {
                pantalla[4, hp_space + 2 - 1] = '▓';
            }

            dmg = Math.Max(stats_player2[(int)STATS.POW] - stats_player1[(int)STATS.DEF], 1);
            hit = Math.Min(Math.Max(80 + stats_player2[(int)STATS.SKL] * 2 + stats_player2[(int)STATS.LCK] - stats_player1[(int)STATS.SPD] * 2 - stats_player1[(int)STATS.LCK], 1), 99);
            crt = Math.Min(Math.Max(stats_player2[(int)STATS.SKL] - stats_player1[(int)STATS.LCK], 1), 99);

            pantalla[6, 5] = (char)('0' + stats_player2[(int)STATS.HP] / 10);
            pantalla[6, 8] = (char)('0' + stats_player2[(int)STATS.HP] / 10);

            pantalla[6, 6] = (char)('0' + stats_player2[(int)STATS.HP] % 10);
            pantalla[6, 9] = (char)('0' + stats_player2[(int)STATS.HP] % 10);

            pantalla[6, 15] = (char)('0' + dmg / 10);
            pantalla[6, 16] = (char)('0' + dmg % 10);

            pantalla[6, 22] = (char)('0' + hit / 10);
            pantalla[6, 23] = (char)('0' + hit % 10);

            pantalla[6, 29] = (char)('0' + crt / 10);
            pantalla[6, 30] = (char)('0' + crt % 10);

            for (int fil = 0; fil < 9; fil++)
            {
                for (int col = 0; col < 44; col++)
                {
                    WriteAt(pantalla[fil, col].ToString(), Console.BufferWidth - 45 + col, 30 - 10 + fil);
                }
            }
        }

        private static void StartBattle(byte[] stats_player1, byte[] stats_player2)
        {
            int dmgP1 = Math.Max(stats_player1[(int)STATS.POW] - stats_player2[(int)STATS.DEF], 1);
            int hitP1 = Math.Min(Math.Max(80 + stats_player1[(int)STATS.SKL] * 2 + stats_player1[(int)STATS.LCK] - stats_player2[(int)STATS.SPD] * 2 - stats_player2[(int)STATS.LCK], 1), 99);
            int crtP1 = Math.Min(Math.Max(stats_player1[(int)STATS.SKL] - stats_player2[(int)STATS.LCK], 1), 99);
            int hpP1 = stats_player1[(int)STATS.HP];
            int spdP1 = stats_player1[(int)STATS.SPD];

            int dmgP2 = Math.Max(stats_player2[(int)STATS.POW] - stats_player1[(int)STATS.DEF], 1);
            int hitP2 = Math.Min(Math.Max(80 + stats_player2[(int)STATS.SKL] * 2 + stats_player2[(int)STATS.LCK] - stats_player1[(int)STATS.SPD] * 2 - stats_player1[(int)STATS.LCK], 1), 99);
            int crtP2 = Math.Min(Math.Max(stats_player2[(int)STATS.SKL] - stats_player1[(int)STATS.LCK], 1), 99);
            int hpP2 = stats_player2[(int)STATS.HP];
            int spdP2 = stats_player2[(int)STATS.SPD];

            int roll;

            while (hpP1 > 0 && hpP2 > 0) {
                roll = rng.Next(101);
                if (spdP1 > spdP2 || rng.Next(101) < 50)
                {
                    //P1 ataca
                    roll = rng.Next(101);
                    if (crtP1 >= roll)
                    {
                        roll = rng.Next(101);
                        if (hitP1 >= roll)
                        {
                            WriteAt("CRITICAL! Player1 did " + dmgP1 * 3+" damage!", 20, 1);
                            QuitHPp2(hpP2, hpP2 - dmgP1 * 3);
                            hpP2 -= dmgP1 * 3;
                        }else
                        {
                            WriteAt("Player 1 missed a critical!", 20, 1);
                        }
                    }else
                    {
                        roll = rng.Next(101);
                        if (hitP1 >= roll)
                        {
                            WriteAt("Hit! Player1 did " + dmgP1 + " damage!", 20, 1);
                            QuitHPp2(hpP2, hpP2 - dmgP1);
                            hpP2 -= dmgP1;
                        }
                        else
                        {
                            WriteAt("Player1 missed!", 20, 1);
                        }
                    }

                    //P2 contraataca
                    roll = rng.Next(101);
                    if (crtP2 >= roll)
                    {
                        roll = rng.Next(101);
                        if (hitP2 >= roll)
                        {
                            WriteAt("CRITICAL! Player2 counters:  " + dmgP2 * 3 + " damage!", 20, 2);
                            QuitHPp1(hpP1, hpP1 - dmgP2 * 3);
                            hpP1 -= dmgP2 * 3;
                        }
                        else
                        {
                            WriteAt("Player2 missed a critical counter!", 20, 2);
                        }
                    }
                    else
                    {
                        roll = rng.Next(101);
                        if (hitP1 >= roll)
                        {
                            WriteAt("Hit! Player2 counters: " + dmgP2 + " damage!",20, 2);
                            QuitHPp1(hpP1, hpP1 - dmgP2);
                            hpP1 -= dmgP2;
                        }
                        else
                        {
                            WriteAt("Player2 missed the counter!", 20, 2 );
                        }
                    }

                    //Comprobamos si hay extra ataque
                    if(spdP1 -4 >= spdP2)
                    {
                        WriteAt("Player1 goes for an extra attack!", 20,3 );
                        roll = rng.Next(101);
                        if (crtP1 >= roll)
                        {
                            roll = rng.Next(101);
                            if (hitP1 >= roll)
                            {
                                WriteAt("CRITICAL! Player1 did " + dmgP1 * 3 + " damage!", 20,4);
                                QuitHPp2(hpP2, hpP2 - dmgP1 * 3);
                                hpP2 -= dmgP1 * 3;
                            }
                            else
                            {
                                WriteAt("Player 1 missed a critical!", 20, 4);
                            }
                        }
                        else
                        {
                            roll = rng.Next(101);
                            if (hitP1 >= roll)
                            {
                                WriteAt("Hit! Player1 did " + dmgP1 + " damage!", 20, 4);
                                QuitHPp2(hpP2, hpP2 - dmgP1);
                                hpP2 -= dmgP1;
                            }
                            else
                            {
                                WriteAt("Player1 missed!", 20, 4);
                            }
                        }
                    }

                    Console.Read();
                }else
                {
                    

                    //P2 ataca
                    roll = rng.Next(101);
                    if (crtP2 >= roll)
                    {
                        roll = rng.Next(101);
                        if (hitP2 >= roll)
                        {
                            WriteAt("CRITICAL! Player2 did  " + dmgP2 * 3 + " damage!", 20,1);
                            QuitHPp1(hpP1, hpP1 - dmgP2 * 3);
                            hpP1 -= dmgP2 * 3;
                        }
                        else
                        {
                            WriteAt("Player2 missed a critical!", 20,1);
                        }
                    }
                    else
                    {
                        roll = rng.Next(101);
                        if (hitP1 >= roll)
                        {
                            WriteAt("Hit! Player2 did " + dmgP2 + " damage!", 20,1);
                            QuitHPp1(hpP1, hpP1 - dmgP2);
                            hpP1 -= dmgP2;
                        }
                        else
                        {
                            WriteAt("Player2 missed!", 20, 1 );
                        }
                    }

                    //P1 contraataca
                    roll = rng.Next(101);
                    if (crtP1 >= roll)
                    {
                        roll = rng.Next(101);
                        if (hitP1 >= roll)
                        {
                            WriteAt("CRITICAL! Player1 counters: " + dmgP1 * 3 + " damage!", 20, 2);
                            QuitHPp2(hpP2, hpP2 - dmgP1 * 3);
                            hpP2 -= dmgP1 * 3;
                        }
                        else
                        {
                            WriteAt("Player 1 missed a critical counter!", 20, 2);
                        }
                    }
                    else
                    {
                        roll = rng.Next(101);
                        if (hitP1 >= roll)
                        {
                            WriteAt("Hit! Player1 counters: " + dmgP1 + " damage!", 20, 2);
                            QuitHPp2(hpP2, hpP2 - dmgP1);
                            hpP2 -= dmgP1;
                        }
                        else
                        {
                            WriteAt("Player1 missed!", 20, 2);
                        }
                    }

                    //Comprobamos si hay extra ataque
                    if (spdP2 - 4 >= spdP1)
                    {
                        WriteAt("Player2 goes for an extra attack!", 20, 3);
                        roll = rng.Next(101);
                        if (crtP2 >= roll)
                        {
                            roll = rng.Next(101);
                            if (hitP2 >= roll)
                            {
                                WriteAt("CRITICAL! Player2 did " + dmgP2 * 3 + " damage!", 20, 4);
                                QuitHPp1(hpP1, hpP1 - dmgP2 * 3);
                                hpP1 -= dmgP2 * 3;
                            }
                            else
                            {
                                WriteAt("Player2 missed a critical!", 20, 4);
                            }
                        }
                        else
                        {
                            roll = rng.Next(101);
                            if (hitP2 >= roll)
                            {
                                WriteAt("Hit! Player2 did " + dmgP2 + " damage!", 20, 4);
                                QuitHPp1(hpP1, hpP1 - dmgP2);
                                hpP1 -= dmgP2;
                            }
                            else
                            {
                                WriteAt("Player1 missed!", 20, 4);
                            }
                        }
                    }

                }
                Console.ReadLine();

                WriteAt("                                               ", 20, 1);
                WriteAt("                                               ", 20, 2);
                WriteAt("                                               ", 20, 3);
                WriteAt("                                               ", 20, 4);
            }
            if (hpP1 > 0)
            {
                WriteAt("Player1 wins!!!", 20, 1);
            }
            else
            {
                WriteAt("Player2 wins!!!", 20, 1);
            }

            Console.ReadLine();
        }

        public static void QuitHPp1(int initialHP, int finalHP)
        {
            finalHP = Math.Max(finalHP, 0);
            for (int hp_space = finalHP+1; hp_space <= initialHP; hp_space++)
            {
                if (hp_space <= 40)
                {
                    WriteAt("░", 3 + hp_space - 1, 30 - 8);
                }else
                {
                    if (hp_space> 40 && hp_space<=80)
                    {
                        WriteAt("░", 3 + hp_space - 41, 30 - 6);
                    }
                }
            }

            WriteAt(""+finalHP/10+""+finalHP%10, 6, 30 - 4);
        }

        public static void QuitHPp2(int initialHP, int finalHP)
        {
            finalHP = Math.Max(finalHP, 0);
            for (int hp_space = finalHP + 1; hp_space <= initialHP; hp_space++)
            {
                if (hp_space <= 40)
                {
                    WriteAt("░", Console.BufferWidth-43+ hp_space - 1, 30 - 8);
                }
                else
                {
                    if (hp_space > 40 && hp_space <= 80)
                    {
                        WriteAt("░", Console.BufferWidth - 43 + hp_space - 41, 30 - 6);
                    }
                }
            }

            WriteAt("" + finalHP / 10 + "" + finalHP % 10, Console.BufferWidth - 40, 30 - 4);
        }
    }
}


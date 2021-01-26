using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    //main
    public class PGNToArraycs
    {
        String[] Lines;
        public List<PGN> Game = new List<PGN>();
        public List<String> gamese = new List<String>();

        public PGNToArraycs(String filename)
        {
            Lines = System.IO.File.ReadAllLines(filename);
        }
        PGNToArraycs()
        {
        }
        public void PGNToArraycsMethod()
        {

            int j = 0;
            for (int i = 1; i < Lines.Length - 1; i++)
            {
                if (Lines[i - 1] == "" && Lines[i + 1] == "")
                {
                    Game.Add(new PGN());
                    Game[j].PGNToArraycsSplit(Lines[i]);
                    j++;
                }
            }

        }
    }
    public class PGN
    {
        public List<String> MoveText = new List<String>();

        public static int Index = 1;
        public static int Len = 0;

        public PGN()
        {

        }
        public void PGNToArraycsSplit(String Game)
        {


            try
            {
                Index = 1;
                do
                {
                    MoveText.Add(Game.Substring(Game.IndexOf(Index.ToString() + "."), Game.IndexOf((Index + 1).ToString() + ".") - Game.IndexOf(Index.ToString() + ".")));
                    Index++;
                } while (true);

            }catch(Exception t)
            {
              
                try
                {
                    MoveText.Add(Game.Substring(Game.IndexOf(Index.ToString() + "."), Game.IndexOf("1-0") - Game.IndexOf(Index.ToString() + ".")));

                }
                catch (Exception tt)
                {
                    MoveText.Add(Game.Substring(Game.IndexOf(Index.ToString() + "."), Game.IndexOf("0-1") - Game.IndexOf(Index.ToString() + ".")));

                }
            }
        }
    }
}

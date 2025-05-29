using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{
    internal class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
        public Player(int id, string name, double score)
        {
            Id = id;
            Name = name;
            Score = score;
        }
    }
}

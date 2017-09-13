using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Model
{
    public class ToyRobotInputModel
    {
        public ToyRobotInputModel()
        {
            Position = new ToyRobotOutputModel();
            InstructionSet = new List<string>();
        }

        public ToyRobotOutputModel Position { get; set; }
        public string Instructions { get; set; }
        public List<string> InstructionSet { get; set; }
    }
}

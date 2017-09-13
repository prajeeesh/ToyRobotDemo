using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Model
{
    public class ToyRobotControlModel
    {
        public ToyRobotControlModel()
        {
            RobotInputs = new List<ToyRobotInputModel>();
        }
        public List<ToyRobotInputModel> RobotInputs { get; set; }
    }
}

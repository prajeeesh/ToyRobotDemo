using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Model
{
    public class ToyRobotOutputViewModel
    {
        public ToyRobotOutputViewModel()
        {
            ToyRobotOutputs = new List<ToyRobotOutputModel>();
        }
        public List<ToyRobotOutputModel> ToyRobotOutputs { get; set; }
        public string ErrorMessage { get; set; }
    }
}

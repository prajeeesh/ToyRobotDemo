using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Model;
namespace ToyRobot.Services
{
    public class RobotMovementServices
    {
        private int GridTopX = 5;
        private int GridTopY = 5;
        private ToyRobotInputModel Move(ToyRobotInputModel inputLocation)
        {

            string direction = inputLocation.Position.Heading;
            string move_direction = "";
            bool can_move = true;
            var currentPositionX = inputLocation.Position.PositionX;
            var currentPositionY = inputLocation.Position.PositionY;

            if (direction == "EAST" || direction == "WEST")
                move_direction = "X";
            else if (direction == "SOUTH" || direction == "NORTH")
                move_direction = "Y";

            if (direction == "EAST" || direction == "NORTH")
            {
                if (direction == "EAST")
                    if ((currentPositionX < 0 || (currentPositionX) > GridTopX))
                        can_move = false;

                if (direction == "NORTH")
                    if (currentPositionY < 0 || (currentPositionY) > GridTopY)
                        can_move = false;

                if (can_move)
                {
                    if (move_direction == "X")
                        inputLocation.Position.PositionX += 1;
                    else
                        inputLocation.Position.PositionY += 1;
                }
            }
            else if (direction == "WEST" || direction == "SOUTH")
            {
                if (move_direction == "X" && currentPositionX > 0)
                    inputLocation.Position.PositionX -= 1;
                else if (move_direction == "Y" && currentPositionY > 0)
                    inputLocation.Position.PositionY -= 1;
            }

            return inputLocation;
        }

        private ToyRobotOutputModel ExecuteCommand(ToyRobotInputModel robotInputModel)
        {
            ToyRobotOutputModel finalCoordinates = new ToyRobotOutputModel();
            foreach (string command in robotInputModel.InstructionSet)
            {
                switch (command)
                {
                    case "LEFT":
                        switch (robotInputModel.Position.Heading)
                        {
                            case "EAST":
                                robotInputModel.Position.Heading = "NORTH";
                                break;
                            case "NORTH":
                                robotInputModel.Position.Heading = "WEST";
                                break;
                            case "WEST":
                                robotInputModel.Position.Heading = "SOUTH";
                                break;
                            case "SOUTH":
                                robotInputModel.Position.Heading = "EAST";
                                break;
                        }
                        break;
                    case "RIGHT":
                        switch (robotInputModel.Position.Heading)
                        {
                            case "EAST":
                                robotInputModel.Position.Heading = "SOUTH";
                                break;
                            case "NORTH":
                                robotInputModel.Position.Heading = "EAST";
                                break;
                            case "WEST":
                                robotInputModel.Position.Heading = "NORTH";
                                break;
                            case "SOUTH":
                                robotInputModel.Position.Heading = "WEST";
                                break;
                        }
                        break;
                    case "MOVE":
                        robotInputModel = Move(robotInputModel);
                        break;
                    case "REPORT":
                        finalCoordinates.PositionX = robotInputModel.Position.PositionX;
                        finalCoordinates.PositionY = robotInputModel.Position.PositionY;
                        finalCoordinates.Heading = robotInputModel.Position.Heading;
                        break;

                }

               // Console.WriteLine("OutPut : " + robotInputModel.Position.PositionX + " " + robotInputModel.Position.PositionY + " " + robotInputModel.Position.Heading);
            }
            
            return finalCoordinates;
        }
        public List<ToyRobotOutputModel> ExecuteRobotNavigation(ToyRobotControlModel toyRobotControlModel)
        {

            List<ToyRobotOutputModel> FinalCoordinates = new List<ToyRobotOutputModel>();

            foreach (ToyRobotInputModel input in toyRobotControlModel.RobotInputs)
            {
                FinalCoordinates.Add(ExecuteCommand(input));
            }
            return FinalCoordinates;
        }
    }
    
}

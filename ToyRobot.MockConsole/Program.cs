using Robot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot.MockConsole
{
    class Program
    {
        static int GridTopX = 5;
        static int GridTopY = 5;
        static void Main(string[] args)
        {
            ToyRobotInputModel inputLocation = new ToyRobotInputModel();

            inputLocation.Position.PositionX = 1;
            inputLocation.Position.PositionY = 2;
            inputLocation.Position.Heading = "EAST";


            //inputLocation.Position.PositionX = 0;
            //inputLocation.Position.PositionY = 0;
            //inputLocation.Position.Heading = "NORTH";
            //string[] instructionSet = { "MOVE", "REPORT" };
            string[] instructionSet = { "MOVE", "MOVE", "LEFT", "MOVE", "REPORT" };

            var output = ExecuteCommand(inputLocation, instructionSet);
            Console.Read();
        }
        public static ToyRobotInputModel Move(ToyRobotInputModel inputLocation)
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

        public static ToyRobotInputModel ExecuteCommand(ToyRobotInputModel inputLocation, string[] moveDirection)
        {
            foreach (string command in moveDirection)
            {
                switch (command)
                {
                    case "LEFT":
                        switch (inputLocation.Position.Heading)
                        {
                            case "EAST":
                                inputLocation.Position.Heading = "NORTH";
                                break;
                            case "NORTH":
                                inputLocation.Position.Heading = "WEST";
                                break;
                            case "WEST":
                                inputLocation.Position.Heading = "SOUTH";
                                break;
                            case "SOUTH":
                                inputLocation.Position.Heading = "EAST";
                                break;
                        }
                        break;
                    case "RIGHT":
                        switch (inputLocation.Position.Heading)
                        {
                            case "EAST":
                                inputLocation.Position.Heading = "SOUTH";
                                break;
                            case "NORTH":
                                inputLocation.Position.Heading = "EAST";
                                break;
                            case "WEST":
                                inputLocation.Position.Heading = "NORTH";
                                break;
                            case "SOUTH":
                                inputLocation.Position.Heading = "WEST";
                                break;
                        }
                        break;
                    case "MOVE":
                        inputLocation = Move(inputLocation);
                        break;
                }

                Console.WriteLine("OutPut : " + inputLocation.Position.PositionX + " " + inputLocation.Position.PositionY + " " + inputLocation.Position.Heading);
            }
            return inputLocation;
        }
    }
}

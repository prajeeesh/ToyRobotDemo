﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Robot.Model;
using ToyRobot.Services;
namespace ToyRobot.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly RobotMovementServices robotMovementServices;
        public HomeController()
        {
            //TODO -implement Dependancy injection
            robotMovementServices = new RobotMovementServices();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string robotInputModel)
        {
            ToyRobotOutputViewModel outputModel = new ToyRobotOutputViewModel();
            List<ToyRobotOutputModel> finalCoordinates = new List<ToyRobotOutputModel>();
            ToyRobotControlModel controlModel = new ToyRobotControlModel();
            string[] commandSeparator = new string[] { "PLACE" };
            string[] inputs = robotInputModel.Split(commandSeparator, StringSplitOptions.None);
            string[] stringSeparators = new string[] { "\r\n" };

            //Validates the inputs 
            outputModel.ErrorMessage = ValidateInput(robotInputModel);

            if (string.IsNullOrEmpty(outputModel.ErrorMessage))
            {
                foreach (string commands in inputs)
                {
                    ToyRobotInputModel inputModel = new ToyRobotInputModel();
                    if (!string.IsNullOrEmpty(commands))
                    {
                        var command = commands.Split(stringSeparators, StringSplitOptions.None);

                        if (command.Length > 1)
                        {
                            string[] startingCoordinate = command[0].Split(',');
                            if (startingCoordinate.Length > 2)
                            {
                                inputModel.Position.PositionX = Convert.ToInt32(startingCoordinate[0]);
                                inputModel.Position.PositionY = Convert.ToInt32(startingCoordinate[1]);
                                inputModel.Position.Heading = startingCoordinate[2];
                            }
                            for (int i = 1; i < command.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(command[i]))
                                    inputModel.InstructionSet.Add(command[i]);
                            }
                        }
                        controlModel.RobotInputs.Add(inputModel);
                    }
                }
            }
            finalCoordinates = robotMovementServices.ExecuteRobotNavigation(controlModel);
            outputModel.ToyRobotOutputs = finalCoordinates;
            //outputModel.ErrorMessage = robotInputModel;
            return View(outputModel);
        }

       private string ValidateInput(string inputCommands)
        {
            string validationError = string.Empty;
            if (!inputCommands.Contains("PLACE"))
                validationError = "At least one PLACE command should be present";
            if (!inputCommands.Contains("REPORT"))
                validationError += Environment.NewLine + "At least one REPORT command should be present";
            return validationError;
        }
    }
}
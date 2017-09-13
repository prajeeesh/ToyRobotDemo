using System;
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
            //var commands = inputs.Split(stringSeparators, StringSplitOptions.None);
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
            finalCoordinates = robotMovementServices.ExecuteRobotNavigation(controlModel);
            outputModel.ToyRobotOutputs = finalCoordinates;
            //outputModel.ErrorMessage = robotInputModel;
            return View(outputModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
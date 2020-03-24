using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private IParkDAO dao;

        private ISurveyDAO surveyDAO;

        public SurveyController(IParkDAO dao, ISurveyDAO surveyDAO)
        {
            this.dao = dao;
            this.surveyDAO = surveyDAO;
        }
        public IActionResult Index()
        {
            IList<Park> parks = dao.GetParks();
            
            ViewData["parks"] = parks;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Survey(SurveyModel submission)
        {
            surveyDAO.SaveNewSubmisssion(submission);
            return RedirectToAction("Results");
        }

        public IActionResult Results()
        {
            IList<SurveyModel> results = surveyDAO.GetSurveys();
            return View(results);

        }
    }
}
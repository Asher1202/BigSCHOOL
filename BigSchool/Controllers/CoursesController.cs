using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            Console.WriteLine(1);
            var viewModel = new CourseViewModel
            {
              Categories =_dbConText.Categories.ToList()
            };
            return View("Create", viewModel);
        }
        private readonly ApplicationDbContext  _dbConText;
        public CoursesController()
        {
            _dbConText = new ApplicationDbContext();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewmodel) 
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Categories = _dbConText.Categories.ToList();
                return View("Create",viewmodel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                Datetime = viewmodel.GetDateTime(),
                CategoryId = viewmodel.Category,
                    Place= viewmodel.Place
            };
            _dbConText.Courses.Add(course);
            _dbConText.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
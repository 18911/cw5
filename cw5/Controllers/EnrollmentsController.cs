using System;
using cw5.Models;
using cw5.Request;
using Microsoft.AspNetCore.Mvc;
using cw5.Services;

namespace cw5.Controllers{


    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase{
        private Student student;
        private IStudentDbService dbService;

        public EnrollmentsController(IStudentDbService dbService) {
            this.dbService = dbService;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest studentRequest) {
            MapStudent(studentRequest);

            if (dbService.CheckStudiesDb(studentRequest)){
                return Ok(dbService.GetEnrollment());//Created(Url,dbService.GetEnrollment());
            }
            return NotFound();
        }

        [Route("/api/enrollments/promotions")]
        [HttpPost]
        public IActionResult PromoteStudents(Promotions promotions) {
            if (dbService.PromoteStudents(promotions)){
                return Ok(dbService.GetEnrollment());//Created(Url,dbService.GetEnrollment());
            }
            else {
                return NotFound();
            }
        }

        public void MapStudent(EnrollStudentRequest enrollRequest) {
            student = new Student();

            student.IndexNumber = enrollRequest.IndexNumber;
            student.FirstName = enrollRequest.FirstName;
            student.LastName = enrollRequest.LastName;
            student.Birthdate = DateTime.Parse(enrollRequest.Birthdate);

        }

        
    }
}
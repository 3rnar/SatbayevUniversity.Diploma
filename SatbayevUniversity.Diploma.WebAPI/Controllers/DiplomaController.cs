
using Microsoft.AspNetCore.Mvc;
using SatbayevUniversity.Diploma.WebAPI.Models;
using SatbayevUniversity.Diploma.WebAPI.Models.RequestModels;
using SatbayevUniversity.Diploma.WebAPI.Services;
using System;


namespace SatbayevUniversity.Diploma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiplomaController : ControllerBase
    {
        IDiplomaWorkService _service;
        string UserId = "a8f8c3b8-89e6-447d-b6a1-173ee88d0d2d";
        string Language = "ru";
        public DiplomaController(IDiplomaWorkService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetDiplomaWorks")]
        public IActionResult GetDiplomaWorks([FromQuery]DiplomasFilter filter)
        {
            try
            {
                return Ok(_service.GetAllDiplomaWorks(filter, Language, UserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDiplomaWorksByID/{ID}")]
        public IActionResult GetDiplomaWorksByID(int ID)
        {
            try
            {
                return Ok(_service.GetDiplomaWorkByID(ID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertDiplomaWorks")]
        public IActionResult InsertDiplomaWorks([FromQuery]CreateDiplomaWorkModel model)
        {
            try
            {
                _service.InsertDiplomaWork(model, UserId, Language);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateDiplomaWorks/{ID}")]
        public IActionResult UpdateDiplomaWork(int ID, [FromQuery]CreateDiplomaWorkModel model)
        {
            try
            {
                _service.UpdateDiplomeWork(ID, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDiplomaTypes")]
        public IActionResult GetDiplomaTypes()
        {
            try
            {
                return Ok(_service.GetDiplomaTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AcceptStudent")]
        public IActionResult AcceptStudent([FromQuery]AcceptStudentModel request)
        {
            try
            {
                _service.TeacherAcceptStudent(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RejectStudent")]
        public IActionResult RejectStudent([FromQuery]RejectStudentModel model)
        {
            try
            {
                _service.TeacherRejectStudent(model, model.Notification, UserId, Language);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllDiplomaInstructors")]
        public IActionResult GetAllDiplomaInstructors()
        {
            try
            {
                return Ok(_service.GetAllInstructors());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

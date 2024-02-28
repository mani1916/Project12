using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Data.Repository;
using Project1.Models;
namespace Project1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;

        private readonly IStudentRepository _studentRepository;



        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }


        [HttpPost("create", Name = "CreateStudent")]
        // api/Student/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentDTO model)

        {
            _logger.LogInformation("CreateStudent Method is started");
            if (model == null)
            {
                return BadRequest("There is no student record given for adding");
            }

            // Student newStudent = new Student()
            // {
            //     StudentName = model.StudentName,
            //     Address = model.Address,
            //     Email = model.Email
            // };

            var newStudent = _mapper.Map<Student>(model);
            model.Id = await _studentRepository.create(newStudent);
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            // return model;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            _logger.LogError("GetStudents is created");
            if (_studentRepository.GetAll() == null)
                return NotFound($"The Records is Empty");
 
            // var students = await _collegeDbContext.Students.Select(stu => new StudentDTO()
            // {
            //     Id = stu.Id,
            //     StudentName = stu.StudentName,
            //     Email = stu.Email,
            //     Address = stu.Address,
            //     DOB = stu.DOB.ToShortDateString()
            // }).ToListAsync();

            var students = await _studentRepository.GetAll();
            var StudentDTOdata = _mapper.Map<List<StudentDTO>>(students);
            return Ok(StudentDTOdata);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Please check your id");
            }
            var res = await _studentRepository.GetByIdAsync(id);
            if (res == null)
                return NotFound($"There is no student with record {id}");
            // var student = new StudentDTO()
            // {
            //     Id = res.Id,
            //     StudentName = res.StudentName,
            //     Address = res.Address,
            //     Email = res.Email,
            //     DOB = res.DOB.ToShortDateString()
            // };

            var studentDTO = _mapper.Map<StudentDTO>(res);
            return Ok(studentDTO);
        }

        [HttpGet("{name}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest($"Please Check Your name");
            }
            var res = await _studentRepository.GetByNameAsync(name);
            if (res == null)
                return NotFound($"There is no student with record {name}");
            // var student = new StudentDTO()
            // {
            //     Id = res.Id,
            //     StudentName = res.StudentName,
            //     Address = res.Address,
            //     Email = res.Email
            // };
            var studentDTO = _mapper.Map<StudentDTO>(res);
            return Ok(studentDTO);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
            {
                return BadRequest("Please Enter the correct detials");
            }
            Student student = await _studentRepository.GetByIdAsync(model.Id, true);
            // var newStudent = new Student()
            // {
            //     Id = model.Id,
            //     StudentName = model.StudentName,
            //     Address = model.Address,
            //     Email = model.Email,
            //     DOB = Convert.ToDateTime(model.DOB),
            // };
            if (student == null)
                return NotFound("No Students record found based on this id");
            var newStudent = _mapper.Map<Student>(model);

            await _studentRepository.updateAsync(newStudent);
            return NoContent();
        }

        [HttpPatch("{id:int}/updatePatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentByPatch(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                return BadRequest("Please enter the correct details");
            }

            Student student = await _studentRepository.GetByIdAsync(id, true);
            if (student == null)
                return NotFound("There is no such record");

            // Map the Student entity to a StudentDTO for patching
            var studentDTO = _mapper.Map<StudentDTO>(student);

            // Apply patch operations to the StudentDTO
            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest("Enter the details with the correct format");
            student = _mapper.Map<Student>(studentDTO);

            _studentRepository.updateAsync(student);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudentById(int id)
        {
            if (id < 0)
            {
                return BadRequest("Please check your ID");
            }
            var res = await _studentRepository.GetByIdAsync(id);
            if (res == null)
                return NotFound($"There is no student with record {id}");
            _studentRepository.DeleteAsync(res);
            return Ok(true);
        }
    }
}
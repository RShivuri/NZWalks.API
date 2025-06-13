using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudents()
        {
            // This is a placeholder for the actual implementation
            return Ok(new List<string> { "Student1", "Student2", "Student3" });
        }
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            // This is a placeholder for the actual implementation
            return Ok($"Student with ID: {id}");
        }
        [HttpPost]
        public IActionResult AddStudent([FromBody] string studentName)
        {
            // This is a placeholder for the actual implementation
            return CreatedAtAction(nameof(GetStudentById), new { id = 1 }, studentName);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestAO1145Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        readonly Testao1145Context context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminController(Testao1145Context context)
        {
            this.context = context;
        }
        //изменения для всех добавить 

        [HttpPost("AddNewTeacher")] //создание учителей все ок 
        public async Task<ActionResult<Teacher>> AddNewTeacher(TeacherModel teacher)
        {
            var newTeacher = new Teacher
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Login = teacher.Login,
                Password = teacher.Password,
            };
            context.Teachers.Add(newTeacher);
            await context.SaveChangesAsync();
            return Ok("Новый учитель добавлен");
        }
        [HttpPost("AddNewStudent")] //ok
        public async Task<ActionResult> AddNewStudent(StModel student)
        {
            var newStudent = new Student { Id = student.Id, FirstName = student.FirstName, LastName = student.LastName, Login = student.Login, Password = student.Password, Age = student.Age,
            IdClass = student.IdClass};
            if (string.IsNullOrEmpty(newStudent.Login))
                return BadRequest("Вы не ввели логин");
            var check = await context.Students.FirstOrDefaultAsync(s => s.Login == newStudent.Login);
            if (check == null)
            {               
                context.Students.Add(newStudent);
                await context.SaveChangesAsync();
                return Ok("Успешно!");
            }
            else
                return BadRequest("Такой аккаунт уже существует");
        }
        [HttpPost("AddClass")] //создание класса все ок 
        public async Task<ActionResult<Class>> Addclass(ClModel clas)
        {
            var newClass = new Class
            {
                Id = clas.Id,
                Number = clas.Number,
                IdTeacher = clas.IdTeacher
            };
            context.Classes.Add(newClass);
            await context.SaveChangesAsync();
            return Ok("Новый класс добавлен");
        }
        [HttpPost("AddSubject")] //создание ПРЕДМЕТ А все ок 
        public async Task<ActionResult<Subject>> AddSubject(Subject subject)
        {
            var newSubject = new Subject
            {
                Id = subject.Id,
                Name = subject.Name
            };
            context.Subjects.Add(newSubject);
            await context.SaveChangesAsync();
            return Ok("Новый класс добавлен");
        }

        [HttpGet("GetAllTeacher")] //ok
        public async Task<List<TeacherModel>> GetAllTeacher()
        {
            var teachers = await context.Teachers.Select(s => (TeacherModel)s).ToListAsync();
            return teachers;
        }
        [HttpGet("GetAllStudent")] //ok
        public async Task<List<StModel>> GetAllStudent()
        {
            var st = await context.Students.Include(s => s.IdClassNavigation).Select(s => (StModel)s).ToListAsync();
            return st;
        }
        [HttpGet("GetAllSubject")] //ok
        public async Task<List<Subject>> GetAllSubject()
        {
            var subj = await context.Subjects.ToListAsync();
            return subj;
        }
        [HttpGet("GetAllClass")] //ok
        public async Task<List<ClModel>> GetAllClass()
        {
            var clas = await context.Classes.Select(s => (ClModel)s).ToListAsync();
            return clas;
        }

    }
}

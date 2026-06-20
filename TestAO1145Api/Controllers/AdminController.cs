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
            try
            {
                var newTeacher = (Teacher)teacher;
                context.Teachers.Add(newTeacher);
                foreach (var sub in newTeacher.IdSubjects)
                context.Entry(sub).State = EntityState.Unchanged;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
            return Ok("Новый учитель добавлен");
        }

        [HttpPut("SaveChangedByTeacher")]
        public async Task<ActionResult> SaveChangedByTeacher(TeacherModel teacher)
        {
            try
            {
                var origin = context.Teachers.Include(s=>s.IdSubjects).FirstOrDefault(s => s.Id == teacher.Id);
                if (origin == null)
                    return BadRequest("Учитель не найден");
                origin.IdSubjects.Clear();
                origin.IdSubjects = teacher.subject.Select(s => (Subject)s).ToList();
                context.Entry(origin).CurrentValues.SetValues((Teacher)teacher);
                await context.SaveChangesAsync();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
        [HttpPut("SaveChangedByStudent")]
        public async Task<ActionResult> SaveChangedByStudent(StModel student)
        {
            try
            {
                var origin = context.Students.FirstOrDefault(s => s.Id == student.Id);
                context.Entry(origin).CurrentValues.SetValues((Student)student);
                await context.SaveChangesAsync();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("AddClass")] //создание класса все ок 
        public async Task<ActionResult> Addclass(ClModel clas)
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
        [HttpPut("SaveChangedByClass")]
        public async Task<ActionResult> SaveChangedByClass(ClModel clas)
        {
            try
            {
                var origin = context.Students.FirstOrDefault(s => s.Id == clas.Id);
                context.Entry(origin).CurrentValues.SetValues((Class)clas);
                await context.SaveChangesAsync();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("AddSubject")] //создание ПРЕДМЕТ А все ок 
        public async Task<ActionResult> AddSubject(SubModel subject)
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
        [HttpPut("SaveChangedBySubject")]
        public async Task<ActionResult> SaveChangedBySubject(SubModel subject)
        {
            try
            {
                var origin = context.Students.FirstOrDefault(s => s.Id == subject.Id);
                context.Entry(origin).CurrentValues.SetValues((Subject)subject);
                await context.SaveChangesAsync();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAllTeacher")] //ok
        public async Task<List<TeacherModel>> GetAllTeacher()
        {
            var teachers = await context.Teachers.Include(s=>s.IdSubjects).Select(s => (TeacherModel)s).ToListAsync();
            return teachers;
        }
        [HttpGet("GetAllStudent")] //ok
        public async Task<List<StModel>> GetAllStudent()
        {
            var st = await context.Students.Include(s => s.IdClassNavigation).Select(s => (StModel)s).ToListAsync();
            return st;
        }
        [HttpGet("GetAllSubject")] //ok
        public async Task<List<SubModel>> GetAllSubject()
        {
            var subj = await context.Subjects.ToListAsync();
            return subj.Select(s => (SubModel)s).ToList(); ; 
        }
        [HttpGet("GetAllClass")] //ok
        public async Task<List<ClModel>> GetAllClass()
        {
            var clas = await context.Classes.Select(s => (ClModel)s).ToListAsync();
            return clas;
        }

    }
}

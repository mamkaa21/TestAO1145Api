using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static TestAO1145Api.Controllers.AuthController;
namespace TestAO1145Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly Testao1145Context context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudentController(Testao1145Context context)
        {
            this.context = context;
        }

        [HttpGet] //okо
        public async Task<ActionResult<StModel>> GetUserData()
        {
            var id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok((StModel)context.Students.Include(s => s.IdClassNavigation).FirstOrDefault(s => s.Id == id));
        }

       
        [HttpGet("GetAllTest")] //ok переделать под теты только для опреджеленного препода
        public async Task<List<TModel>> GetAllTest()
        {
            var tests = await context.Tests.Include(s=>s.IdSubjectNavigation).Include(s => s.IdTeacherNavigation).Select(s => (TModel)s).ToListAsync();
            return tests;
        }
        //подсчет оценки, вывод результатов, 
        [HttpGet("GetTestWithQ")] //РАБОТАЕТ
        public async Task<List<QModel>> GetTestWithQ(int id)
        {
            var Q = await context.Questions.Include(s => s.Answers).Where(s => s.IdTest == id).Select(s => (QModel)s).ToListAsync();
            var test = await context.Tests.FirstOrDefaultAsync(s => s.Id == id);
            if (Q.Count < test.CountQuestionTest)
                return Q;
            else
            {
                Random random = new Random();
                return Q.OrderBy(s => random.NextDouble() > 0.5).Take(test.CountQuestionTest.Value).ToList();
            }
        }

        [HttpPut("SaveChangedByStudentWin")]
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
        [HttpGet("GetResults")] //тесты на гл меню препода уловно последние 5 штук
        public async Task<List<StAModel>> GetResults()
        {
            var st = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var results = await context.Studentanswers.Include(s => s.IdTestNavigation.IdTeacherNavigation).Include(s => s.IdTestNavigation).Include(s => s.IdMarkNavigation).Where(s => s.IdStudent == st).ToListAsync();

            return results.Select(s => (StAModel)s).ToList();
        }

        [HttpPost("PostStAns")] //работает
        public async Task<ActionResult<StAModel>> PostStAns(StAModel sta)
        {
            var ast = await context.Students.FirstOrDefaultAsync(s => s.Id == sta.IdStudent); //наш студент в профиле
            var test = await context.Tests.FirstOrDefaultAsync(s => s.Id == sta.IdTest); // нахождение теста на который отвечает студент
            if (ast == null || test == null)
                return BadRequest();

            var studentanswer = (Studentanswer)sta;
            int rightCount = 0;
            var questions = studentanswer.Testcrossquestions.GroupBy(s => s.IdQuestion).Select(g => g.Key);
            foreach (var q in questions) // перебираем правильные ответы из ответов студента в правильных ответах теста
            {
                var right = context.Answers.Where(s => s.IdQuestion == q && s.RightAnswer.Value).Select(s => s.Id).ToList();
                var answers = studentanswer.Testcrossquestions.Where(s => s.IdQuestion == q).Select(s => s.IdAnswer).ToList();
                var type = context.Questions.FirstOrDefault(s => s.Id == q)?.Type;
                if (type != 3 && right.Count != answers.Count)
                    continue;
                if (type == 3 && right.Count == 0)
                    continue;
                answers.RemoveAll(s => right.Contains(s));
                if (answers.Count != 0)
                    continue;
                rightCount++;
            }
            var percent = ((float)rightCount / test.CountQuestionTest) * 100; //вычисляе м оценку
            var mark = context.Marks.First(s => s.CountQ >= percent);
            studentanswer.IdMark = mark.Id;
            context.Studentanswers.Add(studentanswer);
            await context.SaveChangesAsync();
            var model = (StAModel)studentanswer;
            model.Mark = mark.Number;// перобразовываем оценку в нормальный вид
            return Ok(model);
        }
    }
}


namespace TestAO1145Api
{
    public class TCrossQ
    {
        public int IdStudent { get; set; }

        public int IdQuestion { get; set; }

        public int IdAnswer { get; set; }

        public static explicit operator Testcrossquestion(TCrossQ tcq) 
        {
            return new Testcrossquestion
            {
                IdStudent = tcq.IdStudent,
                IdQuestion = tcq.IdQuestion,
                IdAnswer = tcq.IdAnswer
                 
            };
        }

        public static explicit operator TCrossQ(Testcrossquestion tcq) 
        {
            return new TCrossQ
            {
                IdStudent = tcq.IdStudent,
                IdQuestion = tcq.IdQuestion,
                IdAnswer = tcq.IdAnswer
                 
            };
        }

    }
}

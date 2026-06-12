namespace TestAO1145Api
{
    public class ClModel
    {
        public int? IdTeacher { get;  set; }
        public int? Number { get;  set; }
        public int Id { get;  set; }
        public static explicit operator Class(ClModel clas)
        {
            return new Class
            {
                Id = clas.Id,
                Number = clas.Number,
                IdTeacher = clas.IdTeacher

               
            };
        }
        public static explicit operator ClModel(Class clas)
        {
            return new ClModel
            {
                Id = clas.Id,
                Number = clas.Number,
                IdTeacher = clas.IdTeacher

               
            };
        }
    }
      
}
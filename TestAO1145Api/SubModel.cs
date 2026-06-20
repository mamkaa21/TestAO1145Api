namespace TestAO1145Api
{
    public class SubModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public static explicit operator Subject(SubModel subModel)
        { 
            return new Subject {  Id = subModel.Id, Name = subModel.Name };
        }

        public static explicit operator SubModel(Subject subModel)
        {
            return new SubModel { Id = subModel.Id, Name = subModel.Name };
        }
    }
}

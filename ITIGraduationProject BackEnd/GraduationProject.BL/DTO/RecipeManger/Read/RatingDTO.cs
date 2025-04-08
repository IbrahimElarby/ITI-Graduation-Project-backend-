namespace ITIGraduationProject.BL.DTO.RecipeManger.Read
{
    public class RatingDTO
    {
        public int Score { get; set; }
    }

    public class RatingDto
    {
        public int RatingID { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.RecipeManger.Read
{
    //public class RatingDTO
    //{
    //    public int Score { get; set; }
    //}
    public class CommentDto
    {
        public int CommentID { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
    }


}

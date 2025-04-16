using ITIGraduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class CommentManger : ICommentManger
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public CommentManger( IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult> AddCommentAsync(CommentDto dto)
        {
            var comment = new Comment
            {
                Text = dto.Content,
                RecipeID = dto.RecipeId,
                UserID = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult { Success = true };
        }

        public async Task<GeneralResult<List<CommentDto>>> GetCommentsForRecipeAsync(int recipeId)
        {
            var comments = await _unitOfWork.CommentRepository.GetByRecipeIdAsync(recipeId);

            var commentDtos = comments.Select(c => new CommentDto
            {
                Content = c.Text ?? string.Empty,
                RecipeId = c.RecipeID ?? 0,
                UserId = c.UserID
            }).ToList();

            return new GeneralResult<List<CommentDto>>
            {
                Success = true,
                Data = commentDtos
            };
        }

        public async Task<GeneralResult> UpdateCommentAsync(int commentId, string newText, int userId)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            if (comment == null || comment.UserID != userId)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError { Code = "NotAllowed", Message = "You cannot edit this comment." }]
                };
            }

            comment.Text = newText;
            await _unitOfWork.CommentRepository.UpdateAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult { Success = true };
        }

        public async Task<GeneralResult> DeleteCommentAsync(int commentId, int userId)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            if (comment == null || comment.UserID != userId)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError { Code = "NotAllowed", Message = "You cannot delete this comment." }]
                };
            }

            await _unitOfWork.CommentRepository.DeleteAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult { Success = true };
        }
    }

}

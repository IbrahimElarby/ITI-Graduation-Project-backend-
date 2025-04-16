namespace ITIGraduationProject.BL
{
    public interface ICommentManger
    {
        Task<GeneralResult> AddCommentAsync(CommentDto dto);
        Task<GeneralResult<List<CommentDto>>> GetCommentsForRecipeAsync(int recipeId);
        Task<GeneralResult> UpdateCommentAsync(int commentId, string newText, int userId);
        Task<GeneralResult> DeleteCommentAsync(int commentId, int userId);
    }
}
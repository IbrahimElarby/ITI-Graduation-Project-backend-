namespace ITIGraduationProject.BL
{
    public interface IBlogPostCategoryManger
    {
        public  Task<GeneralResult> AssignCategory(int blogPostId, int categoryId);

        public Task<GeneralResult> UnAssignCategory(int blogPostId, int categoryId);


    }
}
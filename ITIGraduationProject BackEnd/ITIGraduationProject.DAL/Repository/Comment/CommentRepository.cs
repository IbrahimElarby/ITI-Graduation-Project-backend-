using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class CommentRepository : ICommentRepository
    {
        
       
            private readonly ApplicationDbContext _context;

            public CommentRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task AddAsync(Comment comment)
            {
                await _context.Comments.AddAsync(comment);
            }

            public async Task<List<Comment>> GetByRecipeIdAsync(int recipeId)
            {
                return await _context.Comments
                    .Include(c => c.User)
                    .Include(c => c.Recipe)
                    .ThenInclude(r=>r.RecipeIngredients)
                    .ThenInclude(ri=>ri.Ingredient)
                    .Where(c => c.RecipeID == recipeId)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
            }

            public async Task<Comment?> GetByIdAsync(int commentId)
            {
                return await _context.Comments.FindAsync(commentId);
            }

            public Task UpdateAsync(Comment comment)
            {
                _context.Comments.Update(comment);
                return Task.CompletedTask;
            }

            public Task DeleteAsync(Comment comment)
            {
                _context.Comments.Remove(comment);
                return Task.CompletedTask;
            }
        }

    }


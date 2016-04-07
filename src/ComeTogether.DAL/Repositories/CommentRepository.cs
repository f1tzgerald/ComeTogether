using ComeTogether.DAL.Entities;
using ComeTogether.DAL.EntityFramework;
using ComeTogether.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Repositories
{
    public class CommentRepository : IComment
    {
        private ComeTogetherContext _context;

        public CommentRepository (ComeTogetherContext context)
        {
            _context = context;
        }
        
        public void AddComment(int toDoItemId, Comment comment)
        {
            var toDoItem = GetToDoItemById(toDoItemId);
            toDoItem.Comments.Add(comment);
            _context.Comments.Add(comment);
        }

        public IEnumerable<Comment> GetCommentsForToDoItem(int id)
        {
            var commentsInTasks = (from s in _context.ToDoItems
                                   where s.Id == id
                                   select s).Single().Comments.ToList();
            return commentsInTasks;
        }

        public Comment GetCommentById(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
            _context.Remove(comment);
        }
    }
}

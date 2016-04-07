using ComeTogether.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.DAL.Interfaces
{
    public interface IComment
    {
        void AddComment(int toDoItemId, Comment comment);
        //IEnumerable<Comment> GetCommentsForToDoItem(int id);
        Comment GetCommentById(int commentID);
        void DeleteComment(int commentId);
    }
}

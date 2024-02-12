using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class NotesBusiness:INotesBusiness                                               
    {
        private readonly INoteRepository repository;

        public NotesBusiness(INoteRepository repository)
        {
            this.repository = repository;
        }

        public string CreateNotes(NotesModel request, long userid)
        {
            return repository.CreateNotes(request, userid);
        }

        public bool DeleteNote(long userid, long noteid)
        {
            return repository.DeleteNote(userid, noteid);
        }

        public bool UpdateNote(long userid, long noteid, NotesModel request)
        {
            return repository.UpdateNote(userid, noteid, request);
        }

        public IEnumerable<Notes> GetAllNotes()
        {
            return repository.GetAllNotes();
        }

        public IEnumerable<Notes> GetByDate(long userid, DateTime date)
        {
            return repository.GetByDate(userid, date);
        }

        public Notes GetNoteById(long userid, long noteid)
        {
            return repository.GetNoteById(userid, noteid);
        }

        public Notes ToggelTrash(long userid, long noteid)
        {
            return repository.ToggelTrash(userid, noteid);
        }

        public Notes AddColor(long userid, long noteid, string color)
        {
            return repository.AddColor(userid, noteid, color);
        }

        public Notes ToggelPin(long userid, long noteid)
        {
            return repository.ToggelPin(userid, noteid);
        }

        public Notes ToggelArchive(long userid, long noteid)
        {
            return repository.ToggelArchive(userid, noteid);
        }










    }
}

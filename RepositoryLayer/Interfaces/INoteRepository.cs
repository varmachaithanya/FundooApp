using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRepository
    {
        public string CreateNotes(NotesModel request, long userid);

        public bool DeleteNote(long userid, long noteid);

        public bool UpdateNote(long userid, long noteid, NotesModel request);

        public IEnumerable<Notes> GetAllNotes();

        public IEnumerable<Notes> GetByDate(long userid, DateTime date);

        public Notes GetNoteById(long userid, long noteid);

        public Notes ToggelTrash(long userid, long noteid);

        public Notes AddColor(long userid, long noteid, string color);

        public Notes ToggelPin(long userid, long noteid);

        public Notes ToggelArchive(long userid, long noteid);











    }
}

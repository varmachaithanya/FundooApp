using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using RepositoryLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IGetBusiness
    {
        public string GetNote(long id, UserModel model);

        public IEnumerable<Notes> GetNoteByTitle(string title);


        public object getByChar(string character);


    }
}

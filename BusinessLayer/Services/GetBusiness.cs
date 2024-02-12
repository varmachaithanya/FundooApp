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
    public class GetBusiness:IGetBusiness
    {
        private readonly IGetRepository repository;

        public GetBusiness(IGetRepository repository)
        {
            this.repository = repository;
        }

        public string GetNote(long id, UserModel model)
        {
            return repository.GetNote(id,model);
        }

        public object getByChar(string character)
        {
            return repository.getByChar(character);
        }

        public IEnumerable<Notes> GetNoteByTitle(string title)
        {
            return repository.GetNoteByTitle(title);    
        }



    }
}

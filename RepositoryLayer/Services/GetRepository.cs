using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModelLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class GetRepository:IGetRepository
    {
        private readonly FundoContext context;
        private readonly IConfiguration config;
        private readonly IUserRepository userrepository;
        
       
        

        public GetRepository(FundoContext context,IConfiguration config, IUserRepository userrepository)
        {
            this.context = context;
            this.config = config;
            this.userrepository = userrepository;
            

        }

        public string GetNote(long id,UserModel model)
        {

            var user =context.Users.FirstOrDefault(x=>x.UsertId==id);
            if (user != null)
            {
                userrepository.UpdateUser(user.UsertId,model);
                return "User Found \nUser Updated Sucessfully...";

            }
            else
            {
                userrepository.InsertUser(model);
                return "User Not Found \nUser Inserted Sucessfully...";

            }

        }

        public IEnumerable<Notes> GetNoteByTitle(string title)
        {
            var user = context.Notes.Where(x => x.Title == title).ToList();

            if (user != null)
            {
                return user.ToList();
            }
            else
            {
                return null;
            }
        }

        public object getByChar(string character)
        {
            var user=context.Users.Where(x=>x.FirstName.StartsWith(character));
            if (user != null)
            {
                return user;
            }
            return null;
        }

       

    }
}

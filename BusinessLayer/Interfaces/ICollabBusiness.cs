using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBusiness
    {
        public bool AddCollobrator(long userid, long noteid, string collaboratorMail);

        public Collaborator Deletecolaborator(long userid, long noteid, long colaboratorid);

        public IEnumerable<Collaborator> GetCollaboratorById(long userid, long noteid);

        public IEnumerable<Collaborator> GetCollaborators(long userid);




    }
}

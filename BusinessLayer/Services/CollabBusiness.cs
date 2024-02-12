using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class CollabBusiness:ICollabBusiness
    {
        private readonly ICollabRepository _collabRepository;

        public CollabBusiness(ICollabRepository collabRepository)
        {
            _collabRepository = collabRepository;
        }

        public bool AddCollobrator(long userid, long noteid, string collaboratorMail)
        {
            return _collabRepository.AddCollobrator(userid, noteid, collaboratorMail);

        }
        public Collaborator Deletecolaborator(long userid, long noteid, long colaboratorid)
        {
            return _collabRepository.Deletecolaborator(userid, noteid, colaboratorid);
        }

        public IEnumerable<Collaborator> GetCollaboratorById(long userid, long noteid)
        {
            return _collabRepository.GetCollaboratorById(userid, noteid);
        }

        public IEnumerable<Collaborator> GetCollaborators(long userid)
        {
            return _collabRepository.GetCollaborators(userid);
        }




    }
}

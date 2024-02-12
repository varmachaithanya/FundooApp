using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class CollabRepository:ICollabRepository
    {
        private readonly FundoContext Context;

        public CollabRepository(FundoContext context)
        {
            this.Context = context;
        }

        public bool AddCollobrator(long userid,long noteid,string collaboratorMail)
        {
            var user=Context.Notes.Where(x=>x.UserId == userid&&x.NoteID==noteid).FirstOrDefault();
            if (user!=null)
            {
                Collaborator collab = new Collaborator();
                collab.UserId = userid;
                collab.NoteId = noteid;
                collab.CollaboratorEmail = collaboratorMail;

                Context.Add(collab);
                Context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public Collaborator Deletecolaborator(long userid,long noteid,long colaboratorid) 
        {
            var colab=Context.Collaborators.Where(x=>x.UserId==userid&&x.NoteId==noteid&&x.CollaboratorId==colaboratorid).FirstOrDefault();
            if (colab!=null)
            {
                Context.Remove(colab);
                Context.SaveChanges();
                return colab;
            }
            return null;

        }

        public IEnumerable<Collaborator> GetCollaboratorById(long userid,long noteid)
        {
            var colab=Context.Collaborators.Where(x=>x.UserId==userid&&x.NoteId==noteid).ToList();
            if(colab!=null)
            {
                return colab;
            }
            return null;
        }

        public IEnumerable<Collaborator> GetCollaborators(long userid)
        {
            var colab=Context.Collaborators.Where(x=> x.UserId==userid).ToList();
            if (colab != null)
            {
                return colab;
            }
            return null;
        }
    }
}

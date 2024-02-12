using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NoteRepository:INoteRepository
    {
        private readonly FundoContext context;
        private readonly IConfiguration config;

        public NoteRepository(FundoContext context,IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        public async Task<string> UploadImages(IFormFile formFile)
        {
            try
            {
                string orginalFileName = formFile.FileName;
                string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{Path.GetExtension(orginalFileName)}";

                string filePath = FileHelper.GetFilePath(uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string UploadImages(IFormFile formFile)
        //{
        //    try
        //    {
        //        string orginalFileName = formFile.FileName;
        //        string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{Path.GetExtension(orginalFileName)}";

        //        string filePath = FileHelper.GetFilePath(uniqueFileName);
        //        using (var filestream = new FileStream(filePath, FileMode.Create))
        //        {
        //            formFile.CopyTo(filestream);
        //        }
        //        return filePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public IEnumerable<Image> AddImages(long noteId, long userid, ICollection<IFormFile> files)
        {
            try
            {
                Notes resnotes = null;
                var user = context.Notes.FirstOrDefault(n => n.UserId == userid);
                if (user != null)
                {
                    resnotes = context.Notes.Where(n => n.UserId == userid && n.NoteID == noteId).FirstOrDefault();
                    if (resnotes != null)
                    {
                        IList<Image> images = new List<Image>();
                        foreach (var file in files)
                        {
                            Image image = new Image();
                            var UploadImagesRes = UploadImages(file);
                            image.Noteid = noteId;
                            image.ImageUrl = UploadImagesRes.ToString();
                            image.ImageName = file.FileName;
                            images.Add(image);
                            context.Image.Add(image);
                            resnotes.ModifiedAt = DateTime.Now;
                            context.Notes.Update(resnotes);
                            context.SaveChanges();
                        }
                        return images;
                    }
                    else
                    {
                        return null;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public IEnumerable<Image> UpdateImages(long noteId, long userId, ICollection<IFormFile> files)
        {
            try
            {
                var user = context.Users.FirstOrDefault(u => u.UsertId == userId);

                if (user != null)
                {
                    var existingNote = context.Notes.FirstOrDefault(n => n.UserId == userId && n.NoteID == noteId);

                    if (existingNote != null)
                    {
                        var existingImages = context.Image.Where(i => i.Noteid == noteId).FirstOrDefault();

                        context.Image.RemoveRange(existingImages);

                        IList<Image> newImages = new List<Image>();

                        foreach (var file in files)
                        {
                            Image image = new Image();

                            var uploadedImageUrl = UploadImages(file);

                            image.ImageId = existingImages.ImageId;
                            image.Noteid = noteId;
                            image.ImageUrl = uploadedImageUrl.ToString();
                            image.ImageName = file.FileName;
                            context.Image.Add(image);
                            newImages.Add(image);
                        }
                        existingNote.ModifiedAt = DateTime.Now;
                        context.Notes.Update(existingNote);
                        context.SaveChanges();

                        return newImages;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public string CreateNotes(NotesModel request, long userid)
        {
            if (userid != 0)
            {

                IEnumerable<Image> imagelist = null;
                User user = context.Users.FirstOrDefault(x => x.UsertId == userid);
                if (user != null)
                {
                    Notes note = new Notes();
                    note.Title = request.Title;
                    note.Description = request.Description;
                    note.Color = request.Color;
                    note.Reminder = request.Reminder;
                    note.IsArchive = request.IsArchive;
                    note.IsPinned = request.IsPinned;
                    note.IsTrash = request.IsTrash;
                    note.CreatedAt = request.CreatedAt;
                    note.ModifiedAt = request.ModifiedAt;
                    note.UserId = userid;


                    context.Notes.Add(note);
                    context.SaveChanges();
                    if (request.Imagepaths != null)
                    {
                        imagelist = AddImages(note.NoteID, userid, request.Imagepaths);
                    }
                    return "Note Created Sucessfully";

                }
            }
            else
            {
                return null;
            }
            return null;

        }

        public bool DeleteNote(long userid, long noteid)
        {
            var note=context.Notes.FirstOrDefault(x=>x.UserId==userid);
            if (note != null)
            {
                context.Notes.Remove(note);
                context.SaveChanges();
                return true;
            }
            return false;
            
        }

        public bool UpdateNote(long userid, long noteid,NotesModel request) 
        { 
            var update=context.Notes.FirstOrDefault(y=>y.UserId==userid&& y.NoteID==noteid);
            if (update != null)
            {
                update.Title = request.Title;
                update.Description = request.Description;
                update.Color = request.Color;
                update.Reminder = request.Reminder;
                update.IsArchive = request.IsArchive;
                update.IsPinned = request.IsPinned;
                update.IsTrash = request.IsTrash;
                update.CreatedAt = DateTime.Now;
                update.ModifiedAt = DateTime.Now;
                update.UserId = userid;


                context.SaveChanges();
                if (request.Imagepaths != null)
                {
                    UpdateImages(update.NoteID, userid, request.Imagepaths);
                }
                return true;
            }
            return false;
        }

        public IEnumerable<Notes> GetAllNotes()
        {
            var notes =context.Notes.ToList();

            if(notes.Count==0|| notes == null)
            {
                return null;
            }
            return notes.ToList();
        }
        public IEnumerable<Notes> GetByDate(long userid, DateTime date)
        {
            IEnumerable<Notes> user=context.Notes.Where(x=> x.UserId==userid&&x.Reminder==date).ToList();

            if (user.Any())
            {
                return user.ToList();
            }
            return null;
        }

        public Notes GetNoteById(long userid,long noteid) 
        { 
            var user=context.Notes.FirstOrDefault(x=>x.UserId==userid&&x.NoteID==noteid);
            if (user!=null)
            {
                return user;
            }
            return null;
        }

       public Notes ToggelTrash(long userid,long noteid)
        {
            var trash = context.Notes.FirstOrDefault(x=>x.UserId==userid&&x.NoteID==noteid);
            if (trash == null)
            {
                return null;
            }
            else
            {
                if (trash.IsTrash == false)
                {
                    trash.IsTrash = true;

                    if(trash.IsPinned == true)
                    {
                        trash.IsPinned = false;
                    }
                    if(trash.IsArchive == true)
                    {
                        trash.IsArchive= false;
                    }

                    context.Entry(trash).State=EntityState.Modified;
                    context.SaveChanges();
                }

                else
                {
                    trash.IsTrash=false;
                    if (trash.IsPinned == true)
                    {
                        trash.IsPinned=false;
                    }
                    if(trash.IsArchive == true)
                    {
                        trash.IsArchive=false;
                    }

                    context.Entry(trash).State=EntityState.Modified;
                    context.SaveChanges();
                }
                return trash;
            }
        }

        public Notes AddColor(long userid,long noteid,string color) 
        {
            var note=context.Notes.Where(x=>x.UserId==userid&&x.NoteID==noteid).FirstOrDefault();

            if(note == null)
            {
                return null;
            }
            else
            {
                note.Color=color;
                context.Entry(note).State=EntityState.Modified;
                context.SaveChanges();
                return note;
            }
        }

        public Notes ToggelPin(long userid,long noteid)
        {
            var pin=context.Notes.FirstOrDefault(x=>x.UserId==userid && x.NoteID==noteid);

            if(pin == null)
            {
                return null;
            }
            else
            {
                if(pin.IsPinned == true)
                {
                    pin.IsPinned=false;
                    context.Entry(pin).State=EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    pin.IsPinned = true;
                    context.Entry(pin).State = EntityState.Modified;
                    context.SaveChanges();

                }
                return pin;
            }

        }

        public Notes ToggelArchive(long userid,long noteid)
        {
            var archive=context.Notes.FirstOrDefault(x=>x.UserId==userid&&x.NoteID==noteid);
            if(archive == null)
            {
                return null;
            }
            else
            {
                if (archive.IsArchive == true)
                {
                    archive.IsArchive = false;

                    if (archive.IsPinned == true)
                    {
                        archive.IsPinned = false;
                    }
                    if (archive.IsTrash == true)
                    {
                        archive.IsTrash = false;
                    }

                    context.Entry(archive).State = EntityState.Modified;
                    context.SaveChanges();

                    return archive;
                }
                else
                {
                    archive.IsArchive= true;
                    if(archive.IsPinned == true)
                    {
                        archive.IsPinned = false;
                    }
                    if(archive.IsTrash == true)
                    {
                        archive.IsTrash = false;
                    }

                    context.Entry(archive).State= EntityState.Modified;
                    context.SaveChanges();

                    return archive;
                }


                }
            }

            
        }
    }


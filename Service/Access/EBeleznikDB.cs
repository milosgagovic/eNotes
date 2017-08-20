using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using System.Data.SqlClient;

namespace Service.Access
{
    public class EBeleznikDB : IEBeleznikDB
    {
        private static IEBeleznikDB ieBeleznik;

        public static IEBeleznikDB Instance
        {
            get
            {
                if (IEBeleznikDB == null)
                {
                    IEBeleznikDB = new EBeleznikDB();
                }
                return IEBeleznikDB;
            }
            set
            {
                if (IEBeleznikDB == null)
                {
                    IEBeleznikDB = value;
                }
            }
        }

        public static IEBeleznikDB IEBeleznikDB
        {
            get
            {
                return ieBeleznik;
            }

            set
            {
                ieBeleznik = value;
            }
        }
        public bool AddGroup(Group group)
        {
            using (var db = new AccessDB())
            {
                var uList = db.Groups.ToList();
                if (uList.Exists(x => x.Name == group.Name))
                {
                    //grupa sa tim imenom vec postoji
                    return false;
                }
                db.Groups.Add(group);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool AddUser(User user)
        {
            using (var db = new AccessDB())
            {
                var uList = db.Users.ToList();
                if (uList.Exists(x => x.Username == user.Username))
                {
                    //user sa tim username vec postoji
                    return false;
                }
                List<Group> groups = new List<Group>();
                if (user.Group.Count > 0)
                {
                    groups = new List<Group>();
                    foreach (Group item in user.Group)
                    {
                        groups.Add(GetGroup(item.Name, db));
                    }
                }
                user.Group = groups;
                db.Users.Add(user);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public List<Group> GetAllGruops()
        {
            using (AccessDB context = new AccessDB())
            {
                List<Group> groups = context.Groups.ToList();
                return groups;
            }
        }

        public List<User> GetAllUsers()
        {
            using (AccessDB context = new AccessDB())
            {
                List<User> users = context.Users.ToList();
                return users;
            }
        }

        public Group GetGroup(string groupName)
        {
            using (AccessDB context = new AccessDB())
            {

                Group group = context.Groups.FirstOrDefault((x) => x.Name == groupName);
                if (group != null)
                {
                    return group;
                }
            }

            return null;
        }



        public User GetUser(string username)
        {
            using (AccessDB context = new AccessDB())
            {

                User user = context.Users.FirstOrDefault((x) => x.Username == username);
                if (user != null)
                {
                    return user;
                }
            }

            return null;
        }

        public bool LogIn(string username, string password)
        {
            using (AccessDB context = new AccessDB())
            {

                User user = context.Users.FirstOrDefault((x) => x.Username == username);
                if (user != null)
                {
                    if (user.Password.Equals(password))
                    {
                        user.IsAuthenticated = true;
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            return false;
        }

        public bool LogOut(string username)
        {
            using (AccessDB context = new AccessDB())
            {
                var result = from b in context.Users
                             where b.Username.Equals(username)
                             select b;
                User user = result.ToList().FirstOrDefault();
                if (user != null)
                {
                    if (user.IsAuthenticated)
                    {
                        user.IsAuthenticated = false;
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveGroup(Group group)
        {
            using (AccessDB context = new AccessDB())
            {
                Group gr = context.Groups.FirstOrDefault<Group>((x) => x.Id == group.Id);

                if (gr != null)
                {
                    context.Entry(gr).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool RemoveUser(User user)
        {
            using (AccessDB context = new AccessDB())
            {
                User us = context.Users.FirstOrDefault<User>((x) => x.Id == user.Id);

                if (us != null)
                {
                    context.Entry(us).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool UpdateGroup(Group group)
        {
            using (AccessDB context = new AccessDB())
            {

                Group gr = context.Groups.FirstOrDefault((x) => x.Id == group.Id);
                if (gr != null)
                {
                    gr.Name = group.Name;
                    context.Entry(gr).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            using (AccessDB context = new AccessDB())
            {
                User u = context.Users.FirstOrDefault((x) => x.Id == user.Id);
                if (u != null)
                {
                    u.Name = user.Name;
                    u.Surname = user.Surname;
                    u.Username = user.Username;
                    u.Password = user.Password;

                    List<Group> groups = new List<Group>();
                    if (user.Group.Count > 0)
                    {
                        groups = new List<Group>();
                        foreach (Group item in user.Group)
                        {
                            groups.Add(GetGroup(item.Name, context));
                        }
                    }
                    u.Group = groups;

                    var result = context.Database.ExecuteSqlCommand("DELETE  FROM dbo.UserGroups WHERE User_Id = @user ;", new SqlParameter("@user", user.Id));
                    if (result > 0)
                    {
                        context.SaveChanges();
                    }
                    context.Entry(u).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public List<Group> GetGroupsForUser(User user)
        {
            using (AccessDB context = new AccessDB())
            {
                if (user != null)
                {
                    List<Group> result = context.Users.Where(x => x.Id == user.Id).SelectMany(c => c.Group).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                }

            }
            return null;
        }

        public static Group GetGroup(string groupName, AccessDB db)
        {
            var query = db.Groups.SingleOrDefault(p => p.Name == groupName);
            return query;
        }

        public List<Note> GetAllNotes()
        {
            using (AccessDB context = new AccessDB())
            {
                List<Note> notes = context.Notes.ToList();
                return notes;
            }
        }

        public bool AddNote(Note note)
        {
            using (var db = new AccessDB())
            {
                List<Group> groups = new List<Group>();
                if (note.Groups.Count > 0)
                {
                    groups = new List<Group>();
                    foreach (Group item in note.Groups)
                    {
                        groups.Add(GetGroup(item.Name, db));
                    }
                }
                note.Groups = groups;
                db.Notes.Add(note);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool UpdateNote(Note note)
        {
            using (AccessDB context = new AccessDB())
            {
                Note n = context.Notes.FirstOrDefault((x) => x.Id == note.Id);
                if (n != null)
                {
                    n.Title = note.Title;
                    n.RTFText = note.RTFText;

                    List<Group> groups = new List<Group>();
                    if (note.Groups.Count > 0)
                    {
                        groups = new List<Group>();
                        foreach (Group item in note.Groups)
                        {
                            groups.Add(GetGroup(item.Name, context));
                        }
                    }
                    n.Groups = groups;

                    var result = context.Database.ExecuteSqlCommand("DELETE  FROM dbo.NoteGroups WHERE Note_Id = @note ;", new SqlParameter("@note", note.Id));
                    if (result > 0)
                    {
                        context.SaveChanges();
                    }
                    context.Entry(n).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteNote(Note note)
        {
            using (AccessDB context = new AccessDB())
            {
                Note n = context.Notes.FirstOrDefault<Note>((x) => x.Id == note.Id);

                if (n != null)
                {
                    context.Entry(n).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public List<Note> GetNotesForUser(User user)
        {
            using (AccessDB context = new AccessDB())
            {
                User u = context.Users.FirstOrDefault<User>((x) => x.Id == user.Id);
                if (u != null)
                {
                    if (u.Username == "admin" && u.Password == "admin")
                        return context.Notes.ToList();

                    List<Group> groups = context.Users.Where(x => x.Id == u.Id).SelectMany(c => c.Group).ToList();
                    if (groups != null)
                    {
                        List<Note> result = new List<Note>();
                        foreach (Group group in groups)
                        {
                            List<Note> notes = context.Groups.Where(x => x.Id == group.Id).SelectMany(c => c.Notes).ToList();
                            foreach (Note note in notes)
                            {
                                if (!result.Contains(note))
                                    result.Add(note);
                            }
                        }
                        return result;
                    }



                }


                return null;
            }
        }

        public Note GetNote(int id)
        {
            using (AccessDB context = new AccessDB())
            {

                Note n = context.Notes.FirstOrDefault((x) => x.Id == id);
                if (n != null)
                {
                    return n;
                }
            }
            return null;
        }
    }
}

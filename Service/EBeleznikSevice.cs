using Common;
using System.Collections.Generic;
using Common.Entities;
using Service.Access;

namespace Service
{
    public class EBeleznikSevice : IServiceConract
    {
        public bool AddGroup(Group group)
        {
            return EBeleznikDB.Instance.AddGroup(group);
        }

       

        public bool AddUser(User user)
        {
            return EBeleznikDB.Instance.AddUser(user);
        }

        

        public List<Group> GetAllGruops()
        {
            return EBeleznikDB.Instance.GetAllGruops();
        }

        public List<Note> GetAllNotes()
        {
            return EBeleznikDB.Instance.GetAllNotes();
        }

        public List<User> GetAllUsers()
        {
            return EBeleznikDB.Instance.GetAllUsers();
        }

        public Group GetGroup(string groupName)
        {
            return EBeleznikDB.Instance.GetGroup(groupName);
        }

        public List<Group> GetGroupsForUser(User user)
        {
            return EBeleznikDB.Instance.GetGroupsForUser(user);
        }

        public User GetUser(string username)
        {
            return EBeleznikDB.Instance.GetUser(username);
        }

        public bool LogIn(string username, string password)
        {
            return EBeleznikDB.Instance.LogIn(username, password);
        }

        public bool LogOut(string username)
        {
            return EBeleznikDB.Instance.LogOut(username);
        }

        public bool RemoveGroup(Group group)
        {
            return EBeleznikDB.Instance.RemoveGroup(group);
        }

        public bool RemoveUser(User user)
        {
            return EBeleznikDB.Instance.RemoveUser(user);
        }

        public bool UpdateGroup(Group group)
        {
            return EBeleznikDB.Instance.UpdateGroup(group);
        }

        public bool AddNote(Note note)
        {
            return EBeleznikDB.Instance.AddNote(note);
        }

        public bool UpdateNote(Note note)
        {
            return EBeleznikDB.Instance.UpdateNote(note);
        }
        public bool DeleteNote(Note note)
        {
            return EBeleznikDB.Instance.DeleteNote(note);
        }

        public bool UpdateUser(User user)
        {
            return EBeleznikDB.Instance.UpdateUser(user);

        }

        public List<Note> GetNotesForUser(User user)
        {
            return EBeleznikDB.Instance.GetNotesForUser(user);
        }

        public Note GetNote(int id)
        {
            return EBeleznikDB.Instance.GetNote(id);
        }
    }
}

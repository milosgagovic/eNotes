using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Access
{
    public interface IEBeleznikDB
    {
        bool AddUser(User user);
        bool LogIn(string username, string password);
        bool LogOut(string username);
        bool UpdateUser(User user);
        bool RemoveUser(User user);
        User GetUser(string username);
        List<User> GetAllUsers();

        bool AddGroup(Group group);
        bool UpdateGroup(Group group);
        bool RemoveGroup(Group group);
        Group GetGroup(string groupName);
        List<Group> GetAllGruops();

        List<Group> GetGroupsForUser(User user);

        List<Note> GetAllNotes();

        bool AddNote(Note note);
        bool UpdateNote(Note note);

        bool DeleteNote(Note note);
        
        List<Note> GetNotesForUser(User user);

        Note GetNote(int id);
    }
}

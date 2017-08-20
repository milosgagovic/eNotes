using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{ 
    [ServiceContract]
    public interface IServiceConract
    {
        [OperationContract]
        bool AddUser(User user);
        [OperationContract]
        bool LogIn(string username, string password);
        [OperationContract]
        bool LogOut(string username);
        [OperationContract]
        bool UpdateUser(User user);
        [OperationContract]
        bool RemoveUser(User user);
        [OperationContract]
        User GetUser(string username);
        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        bool AddGroup(Group group);
        [OperationContract]
        bool UpdateGroup(Group group);
        [OperationContract]
        bool RemoveGroup(Group group);
        [OperationContract]
        Group GetGroup(string groupName);
        [OperationContract]
        List<Group> GetAllGruops();
        [OperationContract]
        List<Group> GetGroupsForUser(User user);
        [OperationContract]
        List<Note> GetAllNotes();
        [OperationContract]
        bool AddNote(Note note);
        [OperationContract]
        bool UpdateNote(Note note);
        [OperationContract]
        bool DeleteNote(Note note);
        [OperationContract]
        List<Note> GetNotesForUser(User user);
        [OperationContract]
        Note GetNote(int id);
    }
}

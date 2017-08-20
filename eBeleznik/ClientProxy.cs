using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using Common.Entities;
using System.Windows;

namespace eBeleznik
{
    public class ClientProxy : ChannelFactory<IServiceConract>, IServiceConract
    {
        private IServiceConract factory;

        public IServiceConract Factory
        {
            get
            {
                return factory;
            }

            set
            {
                factory = value;
            }

        }


        public ClientProxy(NetTcpBinding binding, string address)
            : base(binding, address)
        {
            try
            {
                this.Factory = this.CreateChannel();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in network");
            }

        }
        public ClientProxy() { }
        public bool AddGroup(Group group)
        {
            bool result = false;

            try
            {
                result = this.Factory.AddGroup(group);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool AddUser(User user)
        {
            bool result = false;

            try
            {
                result = this.Factory.AddUser(user);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public List<Group> GetAllGruops()
        {
            List<Group> result = new List<Group>();
            try
            {
                result = this.factory.GetAllGruops();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public List<User> GetAllUsers()
        {
            List<User> result = new List<User>();
            try
            {
                result = this.factory.GetAllUsers();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public Group GetGroup(string groupName)
        {
            Group result = null;

            try
            {
                result = this.Factory.GetGroup(groupName);
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public User GetUser(string username)
        {
            User result = null;

            try
            {
                result = this.Factory.GetUser(username);
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public bool LogIn(string username, string password)
        {
            bool result = false;

            try
            {
                result = this.Factory.LogIn(username, password);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool LogOut(string username)
        {
            bool result = false;

            try
            {
                result = this.Factory.LogOut(username);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool RemoveGroup(Group group)
        {
            bool result = false;

            try
            {
                result = this.Factory.RemoveGroup(group);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool RemoveUser(User user)
        {
            bool result = false;

            try
            {
                result = this.Factory.RemoveUser(user);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool UpdateGroup(Group group)
        {
            bool result = false;

            try
            {
                result = this.Factory.UpdateGroup(group);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool UpdateUser(User user)
        {
            bool result = false;

            try
            {
                result = this.Factory.UpdateUser(user);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public List<Group> GetGroupsForUser(User user)
        {
            List<Group> result = new List<Group>();
            try
            {
                result = this.factory.GetGroupsForUser(user);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public List<Note> GetAllNotes()
        {
            List<Note> result = new List<Note>();
            try
            {
                result = this.factory.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool AddNote(Note note)
        {
            bool result = false;

            try
            {
                result = this.Factory.AddNote(note);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool UpdateNote(Note note)
        {
            bool result = false;

            try
            {
                result = this.Factory.UpdateNote(note);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public bool DeleteNote(Note note)
        {
            bool result = false;

            try
            {
                result = this.Factory.DeleteNote(note);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            return result;
        }

        public List<Note> GetNotesForUser(User user)
        {
            List<Note> result = new List<Note>();
            try
            {
                result = this.factory.GetNotesForUser(user);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public Note GetNote(int id)
        {
            Note result = null;

            try
            {
                result = this.Factory.GetNote(id);
            }
            catch (Exception e)
            {
            }

            return result;
        }
    }
}

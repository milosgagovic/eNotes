using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Common.Entities
{
    [DataContract]
    public class Note
    {
        public Note()
        {
            Groups = new List<Group>(); 
        }
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string RTFText { get; set; }
        [DataMember]
        public List<Group> Groups { get; set; }
    }
}

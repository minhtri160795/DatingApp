using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models
{
    public class Photo
    {
        [Key]
        public int Id{get;set;}
        public string Url{get;set;}
        public string Description{get;set;}
        public DateTime DateAdded{get;set;}
        public bool IsMain{get;set;}
        public Users Users{get;set;}
        public int UsersId{get;set;}
        
    }
}
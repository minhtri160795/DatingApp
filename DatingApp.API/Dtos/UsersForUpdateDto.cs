using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class UsersForUpdateDto
    {
        public string Introduction {get;set;}
        public string LookingFor {get;set;}
        public string Interests {get;set;}
        public string City {get;set;}
        public string Country {get;set;}
    }
}
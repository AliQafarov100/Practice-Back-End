using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _6hours_Back_End.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        [NotMapped]

        public IFormFile Img { get; set; }
    }
}

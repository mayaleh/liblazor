using MyLibraryOverview.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyLibraryOverview.Server.Models
{
    public class UserBooksContext : BaseModel
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

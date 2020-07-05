using FMS_Web_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.DAL
{   
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        public DbSet<FeedbackOption> FeedbackOptions { get; set; }
        public DbSet<EventParticipatedUser> EventParticipatedUsers { get; set; }
        public DbSet<EventNotParticipatedUser> EventNotParticipatedUsers { get; set; }
        public DbSet<EventUnregisteredUser> EventUnregisteredUsers { get; set; }
        public DbSet<EventPocDetail> EventPocDetails { get; set; }
        public DbSet<ParticipantFeedback> ParticipantFeedbacks { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace TaskApplication.DataAccess.Entities
{
    public enum Statuses : int
    {
        Open = 1,
        Resolved = 2
    }

    public class Status
    {
        public int StatusId { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }
    }
}
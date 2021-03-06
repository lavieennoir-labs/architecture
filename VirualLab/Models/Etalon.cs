﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirualLab.Models
{
    public class Etalon
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        [ForeignKey("Id")]
        public Task Task { get; set; }
        public string[] Statements { get; set; } 
    }
}
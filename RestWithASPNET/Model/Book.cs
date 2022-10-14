using System;
using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNET.Model.Base;

namespace RestWithASPNET.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
        // Agora o Id vem de BaseEntity
        // public int Id { get; set; }
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime launchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public bool Enabled { get; set; }
    }
}
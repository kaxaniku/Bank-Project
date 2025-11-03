using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain
{
    public enum BlockType
    {
        User,
        Bank,
        System
    }
    public class CardBlock
    {
        [Key]
        public int CardBlockId { get; set; }

        [Required]
        public int CardId { get; set; }

        public Card Card { get; set; } = null!;

        [Required]
        public BlockType BlockType { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ReasonCode { get; set; } = null!;

        [Required]
        public bool IsHard { get; set; }

        [Required]
        public DateTimeOffset StartedAt { get; set; }

        [Required]
        public DateTimeOffset? EndedAt { get; set; }

        [Required]
        [MaxLength(100)]
        public string CreatedBy { get; set; } = null!;
    }
}

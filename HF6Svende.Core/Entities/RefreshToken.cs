using HF6SvendeAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Core.Entities
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Login))]
        public int LoginId { get; set; }

        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public virtual Login Login { get; set; } = null!;
    }
}

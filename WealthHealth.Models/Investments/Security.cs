using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace WealthHealth.Models.Investments
{
    public abstract class Security
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Symbol { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [NotMapped]
        public virtual string SecurityWithSymbol {
            get { return String.Format("{0} ({1})", Title, Symbol); }
        }
    }
}
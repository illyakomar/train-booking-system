using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class TwoFactorUser
    {
        public int TwoFactorUserId { get; set; }
        public string UserId { get; set; }
        public int Code { get; set; }

        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared.Services.DateTimeServices
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetTurkeyToday()
        {
            return DateTime.UtcNow.AddHours(3);
        }
    }
}

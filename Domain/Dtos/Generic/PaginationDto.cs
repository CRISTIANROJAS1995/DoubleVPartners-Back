using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Generic
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        private int recordPerPage = 50;
        private readonly int maximumAmountPerPage = 50;

        public int RecordPerPage
        {
            get { return recordPerPage; }
            set
            {
                recordPerPage = value > maximumAmountPerPage ? maximumAmountPerPage : value;
            }
        }
    }
}

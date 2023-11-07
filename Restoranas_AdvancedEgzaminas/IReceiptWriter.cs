using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoranas_AdvancedEgzaminas
{
    public interface IReceiptWriter
    {
        void WriteReceipt(List<Table> tables);
    }
}

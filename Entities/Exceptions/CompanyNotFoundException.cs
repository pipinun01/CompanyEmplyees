using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CompanyNotFoundException: NotFoundException
    {
        public CompanyNotFoundException(Guid id):
            base($"This company with id: {id} doesn't exist in the database")
        {

        }
    }
}

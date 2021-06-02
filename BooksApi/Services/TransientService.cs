using BooksApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class TransientService : ITransientService
    {
        private string guid;

        public TransientService()
        {
            guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return guid;
        }
    }
}

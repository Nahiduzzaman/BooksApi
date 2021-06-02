using BooksApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class ScopedService : IScopedService
    {
        private string guid;

        public ScopedService()
        {
            guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return guid;
        }
    }
}

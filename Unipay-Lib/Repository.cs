using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipay_Lib
{
    public sealed class Repository
    {
        static readonly Repository repository = new Repository();
        //
        // Instantiate any lists stored in this repository
        //
        private Repository()
        {
            
        }
        //
        // Return a version of the repository to whereever it is called.
        // Remembers any data that is put in to the repository.
        //
        public static Repository GetRepository()
        {
            return repository;
        }
        //
        // should take one argument per list that is to be stored in the repository
        //
        public void GetLists()
        {

        }
    }
}

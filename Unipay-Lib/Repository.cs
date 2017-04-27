using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unipay_Lib.Building_Blocks;

namespace Unipay_Lib
{
    public sealed class Repository
    {
        List<Cardsystem> Cardsystems;
        List<Mobilsystem> Mobilsystems;
        List<Merchant> Merchants;
        static readonly Repository repository = new Repository();
        //
        // Instantiate any lists stored in this repository
        //
        private Repository()
        {
            Cardsystems = new List<Cardsystem>();
            Mobilsystems = new List<Mobilsystem>();
            Merchants = new List<Merchant>();
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
        public void GetLists(List<Cardsystem> card = null, List<Mobilsystem> mobil = null, List<Merchant> merc = null)
        {
            if (card != null)
            {
                Cardsystems = card;
            }
            if (mobil != null)
            {
                Mobilsystems = mobil;
            }
            if (merc != null)
            {
                Merchants = merc;
            }
        }
    }
}

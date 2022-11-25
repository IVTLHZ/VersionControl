using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Entities;

namespace UnitTestExample.Abstractions
{
    public interface IAccountManager
    {
        BindingList<Account> Accounts { get; }

        Account CreateAccount(Account account);
    }
}

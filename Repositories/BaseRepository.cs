using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Repositories
{
    /// <summary>
    /// This class is meant to be a base repository, each repository should inherit from this class.
    /// Each repository should have a connectionstring to the database.
    /// </summary>
    public abstract class BaseRepository
    {
        protected string connectionString;
    }
}

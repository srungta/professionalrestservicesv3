using Microsoft.EntityFrameworkCore;
using Microsoft.Libraries.ORM.Interfaces;
using System;

namespace Microsoft.Libraries.ORM.Impl
{
    public abstract class BaseSystemContext : DbContext, ISystemContext
    {
        private const int MIN_ROWS = 1;

        public BaseSystemContext(DbContextOptions<BaseSystemContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public bool CommitChanges()
        {
            var noOfRowsAffected = this.SaveChanges();

            return noOfRowsAffected >= MIN_ROWS;
        }
    }
}

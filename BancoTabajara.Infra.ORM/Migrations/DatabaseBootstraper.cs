using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.ORM.Migrations
{
    public class DatabaseBootstrapper
    {
        private readonly DbContext _context;

        public DatabaseBootstrapper(DbContext context)
        {
            _context = context;
        }

        public void Configure()
        {
            if (_context.Database.Exists())
                return;

            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }
    }
}

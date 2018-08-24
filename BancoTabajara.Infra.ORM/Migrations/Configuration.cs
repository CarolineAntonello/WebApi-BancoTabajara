namespace BancoTabajara.Infra.ORM.Migrations
{
    using BancoTabajara.Common.Tests.Features.Contas;
    using BancoTabajara.Infra.ORM.Context;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BancoTabajaraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BancoTabajaraContext context)
        {
            var _seed = new DatabaseBootstrapper(context);
            _seed.Configure();
            context.Contas.Add(ContaObjectMother.GetContaComMovimentacao());
            context.SaveChanges();
        }
    }
}

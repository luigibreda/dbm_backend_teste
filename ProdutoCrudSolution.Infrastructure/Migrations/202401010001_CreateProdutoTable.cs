using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations;
using Migration = FluentMigrator.Migration;
using MigrationAttribute = FluentMigrator.MigrationAttribute;

namespace ProdutoCrudSolution.Infrastructure.Migrations
{
    [Migration(202401010001)]
    public class CreateProdutoTable : Migration
    {
        public override void Up()
        {
            Create.Table("Produtos")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(100).NotNullable()
                // Se estiver dando erro, troque por .AsString(2147483647)
                .WithColumn("Descricao").AsString(int.MaxValue).Nullable()
                .WithColumn("Preco").AsDecimal().NotNullable()
                .WithColumn("DataCadastro").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Produtos");
        }
    }
}

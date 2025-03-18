using FluentMigrator;

namespace EasyFinance.Infraestructure.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_EXPENSES, "Create table to save the user's expenses")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("expenses")
            .WithColumn("Title").AsString(255).NotNullable()
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("StartMonth").AsInt32().NotNullable()
            .WithColumn("StartYear").AsInt32().NotNullable()
            .WithColumn("Type").AsInt32().NotNullable()
            .WithColumn("Category").AsInt32().NotNullable()
            .WithColumn("PaymentMethod").AsInt32().NotNullable()
            .WithColumn("Months").AsInt32().Nullable()
            .WithColumn("Value").AsDecimal(15,2).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_expenses_users_id", "users", "id");
    }
}


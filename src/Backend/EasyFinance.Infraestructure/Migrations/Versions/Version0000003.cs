using FluentMigrator;

namespace EasyFinance.Infraestructure.Migrations.Versions;
[Migration(DatabaseVersions.UPDATE_TABLE_EXPENSES, "Update table expenses to allow adding recurrent bill")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        Alter.Table("expenses")
            .AddColumn("IsRecurrent").AsBoolean().WithDefaultValue(false)
            .AddColumn("IsRecurrentActive").AsBoolean().WithDefaultValue(false);
    }
}


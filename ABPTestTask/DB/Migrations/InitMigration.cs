using ABPTestTask.Models;
using FluentMigrator;

namespace ABPTestTask.DB.Migrations;

[Migration(1)]
public class InitMigration : Migration
{
    public override void Up()
    {
        
        Create.Table(nameof(Device))
            .WithColumn(nameof(Device.Id)).AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Device.DeviceToken)).AsString(15);

        Create.Table(nameof(ColorExperiment))
            .WithColumn(nameof(ColorExperiment.Id)).AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(ColorExperiment.Color)).AsString(15)
            .WithColumn(nameof(ColorExperiment.DeviceId)).AsInt64()
            .ForeignKey(nameof(Device), nameof(Device.Id));

        Create.Table(nameof(PriceExperiment))
            .WithColumn(nameof(PriceExperiment.Id)).AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(PriceExperiment.Price)).AsDecimal()
            .WithColumn(nameof(PriceExperiment.DeviceId)).AsInt64()
            .ForeignKey(nameof(Device), nameof(Device.Id));
    }

    public override void Down()
    {
        Delete.Table(nameof(Device));
        Delete.Table(nameof(ColorExperiment));
        Delete.Table(nameof(PriceExperiment));
    }
}
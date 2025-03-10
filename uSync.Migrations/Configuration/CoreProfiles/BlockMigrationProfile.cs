﻿using System.Text.RegularExpressions;

using Umbraco.Cms.Core.Composing;

using uSync.Migrations.Composing;
using uSync.Migrations.Configuration.Models;
using uSync.Migrations.Migrators.BlockGrid;
using uSync.Migrations.Migrators.Optional;

namespace uSync.Migrations.Configuration.CoreProfiles;

internal class BlockMigrationProfile : ISyncMigrationProfile
{
    private readonly SyncMigrationHandlerCollection _migrationHandlers;

    public BlockMigrationProfile(SyncMigrationHandlerCollection migrationHandlers)
    {
        _migrationHandlers = migrationHandlers;
    }

    public int Order => 200;

	public string Name => "Convert to BlockLists and BlockGrid";

    public string Icon => "icon-brick color-green";

    public string Description => "Convert Nested content and Grid to BlockList and BlockGrid  (Experimental!)";

    public MigrationOptions Options => new MigrationOptions
    {
        Group = "Convert",
        Source = "uSync/v9",
		Target = $"{uSyncMigrations.MigrationFolder}/{DateTime.Now:yyyyMMdd_HHmmss}",
		Handlers = _migrationHandlers.SelectGroup(8, string.Empty),
        SourceVersion = 8,
        PreferredMigrators = new Dictionary<string, string>
        {
			{ UmbConstants.PropertyEditors.Aliases.NestedContent, nameof(NestedToBlockListMigrator) },
			{ UmbConstants.PropertyEditors.Aliases.Grid, nameof(GridToBlockGridMigrator) }
        }
    };
}

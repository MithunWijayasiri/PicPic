using Xunit;

// PluginMan keeps mutable statics (pluginsFolder, inputType) that tests overwrite.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

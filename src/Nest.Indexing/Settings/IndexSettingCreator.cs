namespace Nest.Indexing.Settings
{
    public class IndexSettingCreator: IIndexSettingCreator
    {
        public virtual IIndexState Create()
        {

            var indexState = new IndexState();

            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            indexSettings.Analysis = new AnalysisDescriptor();
            indexSettings.Analysis.Analyzers = new Analyzers();

            indexSettings.Analysis.Analyzers.Add("indexy_english", new LanguageAnalyzer() { Language = Language.English });

            indexState.Settings = indexSettings;

            return indexState;
        }
    }
}
